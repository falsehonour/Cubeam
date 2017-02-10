using UnityEngine;
using System.Collections;

public class Level4TutorialController : MonoBehaviour {

	public GameObject panel1;

	void Start() {
		panel1.SetActive (true);
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			//Si cliquem un objecte "movible"...
			if (Physics.Raycast (ray, out hit, 100.0f) && hit.collider.tag == "Movable") {  				
				panel1.SetActive (false);	
			}
		}
	}

	public void endFase1(){
		panel1.SetActive (false);
	}
}
