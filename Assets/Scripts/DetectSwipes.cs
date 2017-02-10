using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Swipe {
	None,
	Up,
	Down,
	UpLeft,
	UpRight,
	DownLeft,
	DownRight
};

public class DetectSwipes : MonoBehaviour {

	//Variables publiques
	public static Swipe SwipeDirection;
	public float speed;
	public float AndroidSpeed;

	public RotateOneStep script; //Accedim a l'script de rotacio de camera per ajustar la direccio dels vectors

	//Variables privades		
	private Vector2 _firstClickPos;
	private Vector2 _secondClickPos;
	private Vector2 _currentSwipe;

	private Rigidbody objecte;
	private bool objectSelected = false;
	
	private float distanceTraveled = 0f;

	private bool mHasToStop = false;	
	private List<Rigidbody> mMoguts;	
	private LineRenderer lineRenderer;

	private TouchDistance touchDistance;

	private float amortiguacion;
	public float factorAmortiguacion = 10000;

	private Vector3 objPosition;
	
	void Awake() {
		mMoguts = new List<Rigidbody> ();

		lineRenderer = gameObject.GetComponent<LineRenderer>(); //La Fletxa de direccio

		if (Application.platform == RuntimePlatform.Android) {
			speed = AndroidSpeed;
		}
	}

	void Start() {

		GameObject[] moguts = GameObject.FindGameObjectsWithTag ("Movable");
		foreach (GameObject mogut in moguts) {
			Rigidbody pare = mogut.transform.parent.gameObject.GetComponent<Rigidbody>();
			mMoguts.Add(pare);
		}

		touchDistance = gameObject.GetComponent<TouchDistance> ();
	}

	private void FixedUpdate() {

		//Si s'ha seleccionat un objecte, el movem
		if (objectSelected) {
			if (SwipeDirection != Swipe.None) {
				MoveObject(SwipeDirection, script.nowFacing);
			}
		}
		//Si s'ha des-seleccionat l'objecte, el posicionem segons la cuadricula (i tots els objectes per si se n'ha mogut algun amb el contacte)
		else if (mHasToStop) {
			mHasToStop = false;
			
			for (int i = 0 ; i < mMoguts.Count ; ++i) {
				StartCoroutine (snapToGrid(mMoguts[i]));
			}			
		}	
	}

	private void Update() {

		//Emetem un Raycast per detectar on estem clicant
		if (Input.GetMouseButtonDown(0)) {			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			//Si cliquem un objecte "movible"...
			if (Physics.Raycast (ray, out hit, 100.0f) && hit.collider.tag == "Movable") {  				
				//objecte = hit.collider.gameObject.GetComponent<Rigidbody>(); //Si no te parent
				objecte = hit.collider.transform.parent.gameObject.GetComponent<Rigidbody>(); //Si te parent (tots tenen parent)

				//_firstClickPos = Camera.main.WorldToScreenPoint(objecte.position);
				//Debug.Log("Primer: "+Camera.main.WorldToScreenPoint(objecte.position));
				//Debug.Log("Segon: "+Input.mousePosition);
				_firstClickPos = Input.mousePosition;
				objectSelected = true;
			}
		}	
		
		if (objectSelected) {
			if (Input.GetMouseButton(0)) {

				objPosition = objecte.transform.position;

				Vector2 centerO = Camera.main.WorldToViewportPoint(objPosition);

				distanceTraveled = Vector2.Distance(centerO, Camera.main.ScreenToViewportPoint(Input.mousePosition));
				distanceTraveled = distanceTraveled * 1000;

				ReadSwipe(_firstClickPos, script.nowFacing);
			}	

			if (Input.GetMouseButtonUp(0)) {
				mHasToStop = true;
				objectSelected = false;

				lineRenderer.enabled = false;

			}
		}
		
		//--------------------------- AMORTIGUACIO ---------------------------//
		if (touchDistance.distancia == 1) amortiguacion = 1;
		else {
			float separacion = Vector2.Distance (Camera.main.WorldToViewportPoint(objPosition),Camera.main.ScreenToViewportPoint(Input.mousePosition));
			float factorLerp = ( 1 / ( ( 1 + separacion ) * factorAmortiguacion ) ) ;
			amortiguacion = Mathf.Lerp (amortiguacion , 0 , factorLerp);
			if (amortiguacion < 0.001f) amortiguacion = 0;
		}
		//--------------------------------------------------------------------//
	}

	public void ReadSwipe(Vector2 _secondClickPos, int facingSide){
	
		_secondClickPos = new Vector2( Input.mousePosition.x, Input.mousePosition.y );
		_currentSwipe = new Vector2( _secondClickPos.x - _firstClickPos.x, _secondClickPos.y - _firstClickPos.y );
				
		_currentSwipe.Normalize();
		
		// COMPROVEM LA DIRECCIO DEL SWIPE
		float deadZone = 0.5f; 

		SwipeDirection = Swipe.None;

		if (_currentSwipe.y > 0f) {
			SwipeDirection = Swipe.Up;
			
			if (_currentSwipe.x <= -deadZone) {
				SwipeDirection = Swipe.UpLeft;
			}
			else if (_currentSwipe.x >= deadZone) {
				SwipeDirection = Swipe.UpRight;
			}
		}	
		else if (_currentSwipe.y < 0f) {
			SwipeDirection = Swipe.Down;
			
			if (_currentSwipe.x <= -deadZone) {
				SwipeDirection = Swipe.DownLeft;
			}
			else if (_currentSwipe.x >= deadZone) {
				SwipeDirection = Swipe.DownRight;
			}
		}	
	}	
	
	//Assignem la direccio de cada vector segons la rotacio de la camera i apliquem la força a l'objecte.
	public void MoveObject(Swipe swipeDir, int facingSide){

		Vector3 plusX = transform.right;
		Vector3 plusZ = transform.forward;
		Vector3 minusX = -transform.right;
		Vector3 minusZ = -transform.forward;

		Vector3[][] arr = new Vector3[4][];
		arr[0] = new Vector3[4]{plusX, plusZ, minusX, minusZ};
		arr[1] = new Vector3[4]{plusZ, minusX, minusZ, plusX};
		arr[2] = new Vector3[4]{minusX, minusZ, plusX, plusZ};
		arr[3] = new Vector3[4]{minusZ, plusX, plusZ, minusX};


		float factor = distanceTraveled * speed * Time.fixedDeltaTime * amortiguacion;

		if(distanceTraveled < 30f){
			factor = 0;
		}

		float arrowLength = 1.3f;
		Vector3 arrowDirection = Vector3.zero;
		Vector3 centerOffset = Vector3.zero;
		float offset = 0.6f;

		switch (swipeDir) {
		case Swipe.Up:
			objecte.AddForce (transform.up * factor);
			//Variables de la fletxa:
			centerOffset = new Vector3 (0f, offset, 0f);
			arrowDirection = new Vector3 (0f, arrowLength, 0f);
			break;
		
		case Swipe.Down:
			objecte.AddForce (-transform.up * factor);
			//Variables de la fletxa:
			centerOffset = new Vector3 (0f, -offset, 0f);
			arrowDirection = new Vector3 (0f, -arrowLength, 0f);
			break;
		
		case Swipe.UpLeft:
			objecte.AddForce(arr[facingSide][3] * factor);
			//Variables de la fletxa:
			centerOffset = arr[facingSide][3] * offset;
			arrowDirection = arr[facingSide][3] * arrowLength;
			break;
		
		case Swipe.UpRight:
			objecte.AddForce(arr[facingSide][2] * factor);
			//Variables de la fletxa:
			centerOffset = arr[facingSide][2] * offset;
			arrowDirection = arr[facingSide][2] * arrowLength;
			break;
		
		case Swipe.DownLeft:
			objecte.AddForce(arr[facingSide][0] * factor);
			//Variables de la fletxa:
			centerOffset = arr[facingSide][0] * offset;
			arrowDirection = arr[facingSide][0] * arrowLength;
			break;
		
		case Swipe.DownRight:
			objecte.AddForce(arr[facingSide][1] * factor);
			//Variables de la fletxa:
			centerOffset = arr[facingSide][1] * offset;
			arrowDirection = arr[facingSide][1] * arrowLength;
			break;
		}

		createArrow (centerOffset, arrowDirection);
	}


	public void createArrow(Vector3 centerOffset, Vector3 newDirection) {

		Vector3 startPosition = objecte.position + centerOffset;
		Vector3 targetPosition = objecte.position + newDirection;
		
		lineRenderer.enabled = true;
		lineRenderer.SetPosition(0, startPosition);
		lineRenderer.SetPosition(1, targetPosition);
	}


	public IEnumerator snapToGrid(Rigidbody objecte) {
		objecte.velocity = Vector3.zero;
		objecte.angularVelocity = Vector3.zero;

		float elapsedTime = 0f;
		Vector3 startPosition = objecte.transform.position;
		float time = 0.1f;

		Vector3 newPosition = new Vector3(Mathf.Round(objecte.position.x), 
		                                  Mathf.Round(objecte.position.y), 
		                                  Mathf.Round(objecte.position.z));

		//Animem l'objecte des de la posicio inicial fins a la posicio final.
		while (elapsedTime <= time){
			objecte.transform.position = Vector3.Lerp(startPosition, newPosition, elapsedTime/time);
			elapsedTime += Time.fixedDeltaTime;
			yield return null;
		}
	}
}