using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class MusicManager : MonoBehaviour {

	public AudioSource musicSource;
	public AudioSource tickSource;
	public AudioSource winSource;

	public static MusicManager mInstance = null;

	public static float musicVolum = 1f;
	public static float effectsVolum = 1f;

	void Awake () {

		if (mInstance == null) {
			mInstance = this;
		} else if (mInstance != this) {
			Destroy (gameObject);
			return;
		}
			
		DontDestroyOnLoad(this.gameObject);

		if (PlayerPrefs.HasKey("Musica")) {
			float savedVolume = PlayerPrefs.GetFloat ("Musica");
			musicVolum = savedVolume;
			SetBgVolume(musicVolum);
		}
		if (PlayerPrefs.HasKey("Efectes")) {
			float savedVolume = PlayerPrefs.GetFloat ("Efectes");
			effectsVolum = savedVolume;
			SetSoundsVolume(effectsVolum);
		}
	}

	/*
	public static MusicManager GetMusicInstance() {
		return mInstance;
	}
	*/

	public void PlayTick() {
		tickSource.Play ();
	}

	public void PlayWinSound() {
		winSource.Play ();
	}

	////////////// MANAGE MUSIC VOLUME ///////////////
	public void SetBgVolume(float value) {
		musicVolum = value; 
		musicSource.volume = musicVolum;
	}
	
	public void SetSoundsVolume(float value) {
		effectsVolum = value;
		tickSource.volume = effectsVolum;
		winSource.volume = effectsVolum;
	}
}