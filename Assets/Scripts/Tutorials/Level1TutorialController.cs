using UnityEngine;
using System.Collections;

public class Level1TutorialController : MonoBehaviour {

	//Elements Fase 0
	public GameObject panel0;

	//Elements Fase 1
	public GameObject panel1;
	public GameObject arrow1;

	//Elements Fase 2
	public GameObject panel2;
	public GameObject arrow2;
	public GameObject target;

	//Elements Fase 3	
	public GameObject panel3;
	public GameObject arrow3;
	public GameObject mirror1;

	//Elements Fase 4
	public GameObject panel4;
	public GameObject arrow4;
	public GameObject mirror2;
	
	private bool isFase4 = false;


	void Start () {
		panel0.SetActive (true);
		target.SetActive (true);

		panel1.SetActive (false);
		arrow1.SetActive (false);

		panel2.SetActive (false);
		arrow2.SetActive (false);

		panel3.SetActive (false);
		arrow3.SetActive (false);
		mirror1.SetActive (false);

		panel4.SetActive (false);
		arrow4.SetActive (false);
		mirror2.SetActive (false);
	}

	public void startFase1(){
		panel0.SetActive (false);

		panel1.SetActive (true);
		arrow1.SetActive (true);
		target.SetActive (true);
	}

	public void startFase2(){
		panel1.SetActive (false);
		arrow1.SetActive (false);
		
		panel2.SetActive (true);
		arrow2.SetActive (true);
	}

	public void startFase3(){
		panel2.SetActive (false);
		arrow2.SetActive (false);

		panel3.SetActive (true);
		arrow3.SetActive (true);
		mirror1.SetActive (true);
	}

	public void startFase4(){
		panel3.SetActive (false);
		arrow3.SetActive (false);

		panel4.SetActive (true);
		arrow4.SetActive (true);
		mirror2.SetActive (true);

		isFase4 = true;
	}

	void Update(){
		if (isFase4 && Input.GetMouseButtonDown (0)) {			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			//Si cliquem un objecte "movible"...
			if (Physics.Raycast (ray, out hit, 100.0f) && hit.collider.tag == "Movable") {  				
				arrow4.SetActive (false);	
				panel4.SetActive (false);
			}
		}
	}
}
