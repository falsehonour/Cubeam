using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class Manager {

	private static Manager instance;
	private Dictionary<int, bool> levelState;

	//private string filePath = Application.persistentDataPath + "/settings.txt";
	private const string fileName = "PlayerSettings.txt";
	private StreamWriter sr;

	private Manager() {
		// Este es el constructor, aquí reservas la memoria para las estructuras que necesites tipo diccionario, etc.	
		levelState = new Dictionary<int, bool>();
		//sr = File.CreateText (fileName);
		//Input.multiTouchEnabled = false;
	}

	public static Manager GetInstance() {
		if (instance == null) {
			instance = new Manager();
		}
		return instance;
	}
	

	////////////// MANAGE FINISHED LEVELS ///////////////
	public void SetLevelFinished(int level)	{
		if (!levelState.ContainsKey(level)) {
			levelState.Add(level, true);
			SaveGame (level);
		}
	}

	public bool GetLevelFinished(int level) {
		if (levelState.ContainsKey(level)) {
			return levelState[level];
		}
		return false;
	}

	////////////// MANAGE SAVES ///////////////
	//Manager.GetInstance().SaveGame();
	public void SaveGame(int num) { 
		Debug.Log ("Saving Game");
		PlayerPrefs.SetInt ("CurrentLevel", num);
		/*
		if (!File.Exists (fileName)) {
			Debug.Log (fileName + " not found");
		} 
		Debug.Log ("Writing on file...");


		//sr.WriteLine("This is my file.");
		//sr.WriteLine ("I can write ints {0} or floats {1}, and so on.", 1, 4.2);

		sr.WriteLine ("Sound volume: " + MusicController.soVolum);
		sr.Close();
		*/



		/*	
		GameObject[] cubes = GameObject.FindGameObjectsWithTag("");

		for (int i = 0 ; i < cubes.Length ; ++i)
		{
			Escribir(cubes[i].transform.position)
		}
		*/
	}

	public void LoadGame() { //public void LoadGame(int level) {
		/*
		Debug.Log ("Loading Game");
		Debug.Log (PlayerPrefs.GetInt ("CurrentLevel"));

		int currentLevel = PlayerPrefs.GetInt ("CurrentLevel");
		return currentLevel;
		*/

		// Lees un archivo, y lo vuelcas en el diccionario levelState.
		
		/*try
		{
			using (StreamReader sr = new StreamReader("level" + level.ToString() + ".txt"))
			{
				String line = sr.ReadToEnd();
				Console.WriteLine(line);


			}
		}
		catch (Exception e)
		{
			Console.WriteLine("The file could not be read:");
			Console.WriteLine(e.Message);
		}*/
	}

	//Application.persistentDataPath

	//L'String source es "Musica" o "Efectes"
	public void SaveVolume(String source, float num){
		PlayerPrefs.SetFloat (source, num);
	}
}



