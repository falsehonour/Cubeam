using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectsVolume : MonoBehaviour {
	
	private Slider slider;
	private float firstVolum;
	private float secondVolum;
	
	void Start () {
		slider = GetComponent<Slider> ();

		firstVolum = slider.value;

		if (MusicManager.effectsVolum != slider.value) {
			slider.value = MusicManager.effectsVolum;
		}
	}
	
	// Update is called once per frame
	void Update () {
		secondVolum = slider.value;
		
		if (firstVolum != secondVolum) {
			MusicManager.mInstance.SetSoundsVolume(secondVolum);
			firstVolum = secondVolum;
			Manager.GetInstance().SaveVolume("Efectes", secondVolum);
		}
	}
}
