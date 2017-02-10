using UnityEngine;
using System.Collections;

public class TouchDistance : MonoBehaviour {

	public float distancia;
	public float rango = 0.5f;

	void Update () 
	{
		if (Input.GetMouseButtonDown(0)) 	StartCoroutine (chequeaAnterior());	
		if (!Input.GetMouseButton(0)) 		distancia = 0;
	}

	IEnumerator chequeaAnterior()
	{
		while (Input.GetMouseButton(0))
		{
			Vector2 primerTouch = Input.mousePosition;
			yield return new WaitForSeconds (0.05f);
			distancia = Vector2.Distance(primerTouch, Input.mousePosition);
			distancia = (distancia > rango) ?  1 : 0;
		}
	}
}
