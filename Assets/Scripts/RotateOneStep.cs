using UnityEngine;
using System.Collections;


public class RotateOneStep : MonoBehaviour {
	
	public int nowFacing = 0;
	public Transform targetObject;

	private float targetAngle = 0;

	const float rotationAmount = 5f;
	

	void FixedUpdate() {

		if (targetAngle != 0) {
			StartCoroutine (Rotate ());
		}
	}

	//Si es clica el boto esquerre
	public void LeftButtonPressed(){
		MusicManager.mInstance.PlayTick ();

		if (targetAngle != 0) {
			return;
		}

		targetAngle -= 90.0f;

		//Indiquem quina cara esta enfocat a la camera
		if (nowFacing <= 0) {
			nowFacing = 3;
		} else {
			nowFacing -= 1;
		}
	}

	//Si es clica el boto dret
	public void RightButtonPressed(){
		MusicManager.mInstance.PlayTick ();

		if (targetAngle != 0) {
			return;
		}

		targetAngle += 90.0f;

		//Indiquem quina cara esta enfocant a la camera
		if (nowFacing >= 3) {
			nowFacing = 0;
		} else {
			nowFacing += 1;
		}
	}

	//S'executa la rotacio de la camera
	//protected void Rotate() {
	IEnumerator Rotate(){

		if (targetAngle > 0) {
			transform.RotateAround (targetObject.position, Vector3.up, -rotationAmount);
			targetAngle -= rotationAmount;
			yield return null;
		} else if (targetAngle < 0) {
			transform.RotateAround (targetObject.position, Vector3.up, rotationAmount);
			targetAngle += rotationAmount;
			yield return null;
		}
	}
}
