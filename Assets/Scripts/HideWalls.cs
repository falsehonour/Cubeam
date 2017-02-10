using UnityEngine;
using System.Collections;

public class HideWalls : MonoBehaviour {

	public float speed = 5f;

	private GameObject[] walls;

	void Start(){
		walls = GameObject.FindGameObjectsWithTag ("Wall");
	}

	void Update (){

		foreach (GameObject wall in walls) {

			GameObject pare = wall.transform.parent.gameObject;

			//Variables de distancia
			GameObject cameraTarget = GameObject.Find("CameraTarget");
			float cameraDistance = Vector3.Distance(transform.position, cameraTarget.transform.position);
			float wallDistance = Vector3.Distance (transform.position, wall.transform.position);

			//Variables de rotacio
			Vector3 startRotation = pare.transform.eulerAngles;//wall.transform.eulerAngles;
			Vector3 newRotation;

			if (cameraDistance < wallDistance){ //Estan lluny
				newRotation = new Vector3(pare.transform.eulerAngles.x, pare.transform.eulerAngles.y, 0f);				
			} else{ //Estan a prop
				newRotation = new Vector3(pare.transform.eulerAngles.x, pare.transform.eulerAngles.y, 90f);				
			}

			pare.transform.eulerAngles = Vector3.Lerp(startRotation, newRotation, speed * Time.fixedDeltaTime);
		}
	}
}