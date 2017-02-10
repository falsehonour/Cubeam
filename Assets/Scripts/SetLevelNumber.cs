using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetLevelNumber : MonoBehaviour {
	
	void Start () {
		Text levelNum =  gameObject.GetComponent<Text>();
		levelNum.text = "Level " + SceneManager.GetActiveScene ().buildIndex;
	}

}
