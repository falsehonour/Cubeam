using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ButtonsController : MonoBehaviour {

	public bool UnlockAllLevels = false;
	
	private GameObject mainMenu;
	private GameObject levelsMenu;
	private GameObject settings;

	public static bool main = true;
	public static bool levels = false;
	public static bool setts = false;


	void Awake() {
		if (SceneManager.GetActiveScene ().name == "MainMenu") {
			mainMenu = GameObject.Find ("MainMenu");
			levelsMenu = GameObject.Find ("LevelsMenu");
			settings = GameObject.Find("Settings");
		
			mainMenu.SetActive (main);
			levelsMenu.SetActive (levels);
			settings.SetActive (setts);

			//Manager.GetInstance().LoadGame(); //--!!!!!!!!!!!!!!!!!!
		}
	}

	////////// MAIN MENU BUTTONS //////////
	public void LoadLastUnlocked(){
		buttonTick ();

		int savedLevel = PlayerPrefs.GetInt ("CurrentLevel");
		
		//Load last unlocked level.
		if (Application.CanStreamedLevelBeLoaded(savedLevel + 1) && savedLevel + 2 < SceneManager.sceneCountInBuildSettings) {
			SceneManager.LoadScene(savedLevel + 1);
		} else {
			SceneManager.LoadScene("Level_1");
		}
	}

	////////// LEVELS MENU BUTTONS //////////
	public void LoadLevel(int number){
		buttonTick ();

		int savedLevel = PlayerPrefs.GetInt ("CurrentLevel"); 
		//if (UnlockAllLevels || (number == 1 || Manager.GetInstance().GetLevelFinished(number - 1))){
		if (UnlockAllLevels || number == 1 || number <= savedLevel + 1){
			SceneManager.LoadScene("Level_" + number);
		}
	}

	////////// IN LEVEL BUTTONS //////////
	public void GoBack(){
		buttonTick ();

		if (SceneManager.GetActiveScene ().name == "EndCredits") {
			SceneManager.LoadScene("MainMenu");
			main = true;
			levels = false;
			setts = false;
		} else if (SceneManager.GetActiveScene ().name == "MainMenu") {
			main = true;
			levels = false;
			setts = false;
		} else {
			SceneManager.LoadScene("MainMenu");
			main = false;
			levels = true;
		}
	}

	public void NextLevel(){
		buttonTick ();

		int currentLevel = SceneManager.GetActiveScene ().buildIndex;
		if (Application.CanStreamedLevelBeLoaded (currentLevel + 1)) {
			SceneManager.LoadScene(currentLevel + 1);
		} else {
			//Debug.Log("No more levels! Loading Credits");
			SceneManager.LoadScene("EndCredits");
		}

	}

	//----- Android Back Button
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			
			switch(SceneManager.GetActiveScene ().name) {		
			case "MainMenu":
				if(main){
					Application.Quit();
				} else {
					main = true;
					levels = false;
					setts = false;
				}
				break;

			case "EndCredits":
				SceneManager.LoadScene("MainMenu");
				main = true;
				levels = false;
				setts = false;
				break;
			
			default:
				SceneManager.LoadScene("MainMenu");
				main = false;
				levels = true;
				setts = false;
				break;
			}

			mainMenu.SetActive (main);
			levelsMenu.SetActive (levels);
			settings.SetActive (setts);
		}

		//Debug.Log (main);
	}


	public void buttonTick(){
		MusicManager.mInstance.PlayTick ();
	}

	public void setMainFalse(){
		main = false;
	}

	public void setMainTrue(){
		main = true;
	}

}
