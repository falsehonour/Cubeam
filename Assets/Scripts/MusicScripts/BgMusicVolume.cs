using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BgMusicVolume : MonoBehaviour {

	private Slider slider;
	private float firstVolum;
	private float secondVolum;

	void Awake () {		
		slider = GetComponent<Slider> ();

		firstVolum = slider.value;

		if (MusicManager.musicVolum != slider.value) {
			slider.value = MusicManager.musicVolum;
		}
	}
	
	// Update is called once per frame
	void Update () {
		secondVolum = slider.value;

		if (firstVolum != secondVolum) {
			MusicManager.mInstance.SetBgVolume(secondVolum);
			firstVolum = secondVolum;
			Manager.GetInstance().SaveVolume("Musica", secondVolum);
		}
	}

}
