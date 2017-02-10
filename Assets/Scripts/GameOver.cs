using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public Text gameOverText;
	public Text congratsText;

	public bool levelComplete = false;


	private string[] randomString = new string[] {"Congratulations!", "You are a genius!", "Incredible!", "You made it!", "You are a winner!", "Amazing!", "You are on fire!", "Outstanding!", "Wow! Very skills!"};

	private GameObject[] targets;
	private bool allEnabled = false;

	private Animator animator;



	void Start (){
		animator = GetComponent<Animator>();

		int randNum = Random.Range (0, randomString.Length);
		congratsText.text = randomString[randNum];

		targets = GameObject.FindGameObjectsWithTag("Target");
	}

	void FixedUpdate () {

		foreach (GameObject target in targets) {
			Behaviour halo = target.GetComponent("Halo") as Behaviour;
			if (halo.enabled){
				allEnabled = true;
			}else{
				allEnabled = false;
				break;
			}
		}

		if (!levelComplete) {
			if (allEnabled) {
				//gameOverText.text = "YOU WON!";
				animator.SetTrigger ("GameOver");
				MusicManager.mInstance.PlayWinSound();
				levelComplete = true;

				int currLevel = SceneManager.GetActiveScene ().buildIndex; //Nivell actual
				int lastLevel = PlayerPrefs.GetInt("CurrentLevel"); //Ultim nivell guardat

				//Nomes guardem si el nivell superat es mes alt que l'ultim nivell guardat
				if(currLevel > lastLevel){
					Manager.GetInstance ().SetLevelFinished (SceneManager.GetActiveScene ().buildIndex);
				} //else print ("No cal guardar");


			} else {
				//gameOverText.text = "Keep trying...";
				levelComplete = false;
			}
		}

	}
}
