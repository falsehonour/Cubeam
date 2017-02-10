using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LevelsController : MonoBehaviour {

	public Sprite lockSprite;
	public Sprite unlockSprite;

	public ButtonsController script; //Llegim ButtonsController per saber si s'ha activat la opcio de desbloquejar tots els nivells.

	private Image[] buttons;

	void Start () {

		int savedLevel = PlayerPrefs.GetInt ("CurrentLevel"); 


		buttons = gameObject.GetComponentsInChildren<Image>();
		foreach (Image button in buttons) {
			
			string readNumber = Regex.Match(button.name, @"\d+").Value;
			int number; 
			int.TryParse(readNumber, out number);

			Text text = button.gameObject.GetComponentInChildren<Text>();

			// Desbloquejat
			if (number == 1 || number <= savedLevel + 1 || script.UnlockAllLevels){
				button.sprite = unlockSprite;
				text.enabled = true;

				if(number == savedLevel + 1){
					Button b = button.GetComponent<Button>();
					ColorBlock cb = b.colors;
					cb.normalColor = new Color32(23, 210, 227, 255);
					b.colors = cb;
				}

			} 
			// Bloquejat
			else {
				button.sprite = lockSprite;
				text.enabled = false;
			}
		
		}
	}	
}
