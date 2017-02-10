using UnityEngine;
using System.Collections;

public class Level2i3TutorialController : MonoBehaviour {

	public GameOver gameOver;

	//Elements Fase 1
	public GameObject panel1;
	public GameObject arrow1;
	private bool isFase1 = true;

	//Elements Fase 2
	public GameObject panel2;
	private bool isGameOver;


	void Start () {
		panel1.SetActive (true);
		arrow1.SetActive (true);

		panel2.SetActive (false);
	}

	void FixedUpdate(){
		if (isFase1 && Input.GetMouseButtonDown (0)) {			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			//Si cliquem un objecte "movible"...
			if (Physics.Raycast (ray, out hit, 100.0f) && hit.collider.tag == "Movable") {  				
				arrow1.SetActive (false);	
				panel1.SetActive (false);

				isFase1 = false;
				StartCoroutine(Fase2());
			}
		}

		isGameOver = gameOver.levelComplete;

		if (isGameOver) {
			panel2.SetActive (false);
		}
	}

	public IEnumerator Fase2(){
		yield return new WaitForSeconds(8);
		if (!isGameOver) {
			panel2.SetActive (true);
		}
		yield return null;
	}
}
