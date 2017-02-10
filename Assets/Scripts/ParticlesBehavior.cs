using UnityEngine;
using System.Collections;

public class ParticlesBehavior : MonoBehaviour {

	public GameObject particules;
	private ParticleSystem partSystem;
	private ParticleSystem.EmissionModule em;

	private bool mirrorHit;
	private bool oldMirrorHit;

	private GameObject hitArcPrefab;

	
	//public void ArcReactorReflection(ArcReactorHitInfo hit) {
	public void ArcReactorHit(ArcReactorHitInfo hit) {
		//Debug.Log(hit.raycastHit.transform.gameObject.name);
		mirrorHit = true;

		particules.transform.position = hit.raycastHit.point;
		particules.transform.LookAt(hit.raycastHit.point + hit.raycastHit.normal);

		//Si el GameObject particules esta desactivat, activa'l
		if (!particules.activeSelf) {
			particules.SetActive (true);
		}

		//Si el Sistema de Particules esta parat, engega'l
		if (!em.enabled || partSystem.isStopped) {
		
			if (hitArcPrefab != hit.launcher.arcPrefab){
				hitArcPrefab = hit.launcher.arcPrefab; //Assignem el tipus de laser
				colorParticles(); //Generem les particules del color que toca
			}

			em.enabled = true;
		}
	}
	

	void Start () {
		partSystem = particules.GetComponent<ParticleSystem>();
		em = partSystem.emission;
	}


	void FixedUpdate () {
		//If ray stopped hitting mirror
		if (!mirrorHit && !oldMirrorHit && em.enabled) {
			em.enabled = false;
		}
		oldMirrorHit = mirrorHit;
		mirrorHit = false;
	}

	IEnumerator WaitAndStop() {
		yield return new WaitForSeconds(2);
		partSystem.Stop();
		particules.SetActive (false);
	}


	public void colorParticles(){
		
		switch(hitArcPrefab.name){
		case("LaserBlanc"):
			partSystem.startColor = new Color32 (255, 255, 255, 255);
			break;
			
		case("LaserVerd"):
			partSystem.startColor = new Color32 (50, 255, 50, 255);
			break;
			
		case("LaserVermell"):
			partSystem.startColor = new Color32 (255, 50, 50, 255);
			break;
		}
		
	}
}
