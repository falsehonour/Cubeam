using UnityEngine;
using System.Collections;

public class SpawnWaves : MonoBehaviour {
	
	public GameObject[] hazards; ////
	public Vector3 spawnValues;

	public int hazardCount;
	public float spawnWait;

	public Vector3 gravity;

	void Start(){
		StartCoroutine (spawnHazard ());
		Physics.gravity = gravity;
	}

	IEnumerator spawnHazard () {
		while(true){
			for (int i = 0; i < hazardCount; i++){

				int random = Random.Range (0, hazards.Length);

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				GameObject myObject = Instantiate (hazards[random], spawnPosition, spawnRotation) as GameObject;
				myObject.transform.parent = transform;

				yield return new WaitForSeconds(spawnWait);
			}
		}
	}
	

}
