using UnityEngine;
using System.Collections;

public enum TargetColor{
	Blanc,
	Verd,
	Vermell
}

public class TargetBehavior : MonoBehaviour {

	public TargetColor targetColor;

	private bool targetHit;
	private Behaviour halo;	
	private ArcReactor_Launcher myLauncher;
	private GameObject rightColor;


	//Si el laser toca l'objecte, n'obtenim la informacio
	public void ArcReactorHit(ArcReactorHitInfo hit) {

		myLauncher = hit.launcher;
		targetHit = true;
	}

	void Start (){
		halo = (Behaviour)GetComponent("Halo");

		switch (targetColor) {

		case TargetColor.Blanc:
			rightColor = Resources.Load("LaserBlanc") as GameObject;
			break;

		case TargetColor.Verd:
			rightColor = Resources.Load("LaserVerd") as GameObject;
			break;

		case TargetColor.Vermell:
			rightColor = Resources.Load("LaserVermell") as GameObject;
			break;
		}
	}
	
	void Update () {

		if (targetHit && myLauncher.arcPrefab == rightColor) {
			halo.enabled = true;
		} else {
			halo.enabled = false;
		}

		targetHit = false;
	}
}


