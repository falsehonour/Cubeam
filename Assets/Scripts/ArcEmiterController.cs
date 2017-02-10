using UnityEngine;
using System.Collections;

public class ArcEmiterController : MonoBehaviour {

	public ArcReactor_Launcher[] launchers;
	public float rechargeRate = 1;

//	private bool rayLaunched = false;
	private float recharge;

	void Start () {
		launchers [0].LaunchRay ();
	}
	/*
	void Update () 
	{
		if (!rayLaunched) {
			launchers [0].LaunchRay ();
			rayLaunched = true;
		} else {
			rayLaunched = false;
		}
	}
	*/
}
