using UnityEngine;
using System.Collections;

public enum ObjectType {
	Duodiv,
	ColorSwitch,
}

public enum LaserColor {
	Inherit, 
	Verd, 
	Vermell
}

public class LaserBehavior : MonoBehaviour {
	
	public ObjectType objectType;
	public LaserColor laserColor = LaserColor.Inherit;
	public Transform newArc;

	private bool mirrorHit;
	private bool alreadyCloned = false;

	private Transform clone1;
	private Transform clone2;

	private Vector3 arcPos;
	private Quaternion arcRot1;
	private Quaternion arcRot2;

	private ArcReactor_Launcher arcSettings1;
	private ArcReactor_Launcher arcSettings2;

	private GameObject hitArcPrefab;
	//private Vector3 hitPoint;
	private Vector3 hitNormal;
	private RaycastHit myHit;
	private ArcReactor_Launcher myLauncher;
	

	//Si el laser toca l'objecte, n'obtenim la informacio
	public void ArcReactorHit(ArcReactorHitInfo hit) {

		myHit = hit.raycastHit;
		myLauncher = hit.launcher;

		//hitPoint = myHit.point;
		hitNormal = myHit.normal;
		hitNormal = myHit.transform.TransformDirection (hitNormal);

		mirrorHit = true;
	}
	
	void Update () {

		Vector3 goPos = gameObject.transform.parent.position;
		Vector3 goRot = gameObject.transform.parent.eulerAngles;

		switch (objectType) {
		/*-----------------------------------------------------------------------------------------*/	
		case ObjectType.Duodiv:

			arcPos = new Vector3 (goPos.x, goPos.y, goPos.z);

			arcRot1 = Quaternion.Euler (goRot.x, goRot.y, goRot.z);
			arcRot2 = Quaternion.Euler (goRot.x + 180, goRot.y, goRot.z);

			if (mirrorHit && !alreadyCloned) {
				
				clone1 = (Transform) Instantiate(newArc, arcPos, arcRot1);
				clone2 = (Transform) Instantiate(newArc, arcPos, arcRot2);

				arcSettings1 = clone1.Find("Launcher").GetComponent<ArcReactor_Launcher>();
				arcSettings2 = clone2.Find("Launcher").GetComponent<ArcReactor_Launcher>();
				
				//Establim el color del laser (inherit)
				hitArcPrefab = myLauncher.arcPrefab;
				arcSettings1.arcPrefab = hitArcPrefab;
				arcSettings2.arcPrefab = hitArcPrefab;

				//Convertim els clons en fills de l'objecte
				clone1.transform.parent = transform.parent;
				clone2.transform.parent = transform.parent;
				
				alreadyCloned = true;
			}
			
			if (!mirrorHit && alreadyCloned) {
				Destroy(clone1.gameObject);
				Destroy(clone2.gameObject);
				alreadyCloned = false;
			}

			break;
		/*-----------------------------------------------------------------------------------------*/
		case ObjectType.ColorSwitch:	
			
			arcPos = transform.position; //hitPoint;
			//arcPos = hitPoint;//
			//particules.transform.LookAt(hit.raycastHit.point + hit.raycastHit.normal);

			if (mirrorHit && !alreadyCloned) {

				hitArcPrefab = myLauncher.arcPrefab;
				if (hitArcPrefab.name != "LaserBlanc"){
					return;
				}

				if (hitNormal == myHit.transform.right) {
					arcRot1 = Quaternion.LookRotation(-Vector3.right);
				}
				if (hitNormal == -myHit.transform.right){;
					arcRot1 = Quaternion.LookRotation(Vector3.right);
				}
				if (hitNormal == myHit.transform.forward){
					arcRot1 = Quaternion.LookRotation(-Vector3.forward);
				}
				if (hitNormal == -myHit.transform.forward){
					arcRot1 = Quaternion.LookRotation(Vector3.forward);
				}
				if (hitNormal == myHit.transform.up){
					arcRot1 = Quaternion.LookRotation(-Vector3.up);
				}
				if (hitNormal == -myHit.transform.up){
					arcRot1 = Quaternion.LookRotation(Vector3.up);
				}

				clone1 = (Transform) Instantiate (newArc, arcPos, arcRot1);

				arcSettings1 = clone1.Find("Launcher").GetComponent<ArcReactor_Launcher>();


				if (laserColor == LaserColor.Verd){
					arcSettings1.arcPrefab = Resources.Load("LaserVerd") as GameObject;
				} else if (laserColor == LaserColor.Vermell){
					arcSettings1.arcPrefab = Resources.Load("LaserVermell") as GameObject;
				}

				//Convertim el clon en fill de l'objecte
				clone1.transform.parent = transform.parent;
				
				alreadyCloned = true;
			}
			
			if (!mirrorHit && alreadyCloned) {
				Destroy(clone1.gameObject);
				alreadyCloned = false;
			}

			break;
		/*-----------------------------------------------------------------------------------------*/
		}

		mirrorHit = false;
	}
}


