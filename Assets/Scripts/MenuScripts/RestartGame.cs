using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

	public void DeleteAllPrefs(){
		PlayerPrefs.DeleteAll ();
	}
}
