using UnityEngine;
using System.Collections;

public class Level15TutorialController : MonoBehaviour {

	public GameObject panel1;
	public GameObject panel2;
	public GameObject panel3;

	void Start() {

	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			//Si cliquem un objecte "movible"...
			if (Physics.Raycast (ray, out hit, 100.0f) && hit.collider.tag == "Movable") {  				
					
			}
		}
	}

	public void endFase1(){

	}
}
