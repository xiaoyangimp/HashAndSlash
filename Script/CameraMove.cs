using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
		
	public Transform target;
	public float walkDistance;
	public float runDistance;
	public float height;

	private float _x;						//used in camera rotation
	private float _y;						//used in camera rotation
	private float _xSpeed = 250.0f;			//rotation speed of camera in x-axis
	private float _ySpeed = 120.0f;			//rotation speed of camera in y-axis
	private bool _camButtonDown = false;
	private bool _rotateCameraKeyPressed = false;

	private float heightDamping = 2.0f;
	private float rotationDamping = 3.0f;

	private Transform _myTransform;

	void Awake() {
		_myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		if( target == null ) {
			Debug.Log("No target attached!");
			return;
		}
		else {
			CameraSetUp();
		}

	}

	void Update() {

		// handle the input logics and translate the button and up into a bool value
		// so that a single click event can be translated into a period of time
		if( Input.GetButtonDown( "Rotate Camera Button" )  ) {	// use the Imput Manager to make this user selectable button
			_camButtonDown = true;
		}
		else if ( Input.GetButtonUp ( "Rotate Camera Button" ) )  {
			_camButtonDown = false;
		}

		if( Input.GetButtonDown( "Rotate Camera Horizontal Button" ) || Input.GetButtonDown( "Rotate Camera Vertical Button" ) ) {
			_rotateCameraKeyPressed = true;
		}
		else if ( Input.GetButtonUp( "Rotate Camera Horizontal Button" ) || Input.GetButtonDown( "Rotate Camera Vertical Button" ) ) {
			_rotateCameraKeyPressed = false;
		}
	}
		
	// function called after all thje Update function are done
	void LateUpdate () {

		if ( target == null ) {
			Debug.Log("No target attached!");
			return;
		}

		if ( _rotateCameraKeyPressed ) {
			_x += Input.GetAxis("Rotate Camera Horizontal Button") * _xSpeed * 0.02f;
			_y += Input.GetAxis("Rotate Camera Vertical Button") * _ySpeed * 0.02f;

			RotateCamera();
		}
		else if( _camButtonDown )  {	// use the Imput Manager to make this user selectable button
			_x += Input.GetAxis("Mouse X") * _xSpeed * 0.02f;
			_y -= Input.GetAxis("Mouse Y") * _ySpeed * 0.02f;

			
			RotateCamera();
		}
		else {
			// CameraSetUp();

			_x = _myTransform.eulerAngles.y;
			_y = _myTransform.eulerAngles.x;
			
			// Calculate the current rotation angles
			float wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y + height;
			
			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;
			
			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
			
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			_myTransform.position = target.position;
			_myTransform.position -= currentRotation * Vector3.forward * walkDistance;
			
			// Set the height of the camera
			_myTransform.position = new Vector3( _myTransform.position.x, currentHeight, _myTransform.position.z);
			
			// Always look at the target
			_myTransform.LookAt (target);
		}
	}

	public void CameraSetUp() {
		_myTransform.position = new Vector3(target.position.x , target.position.y + height, target.position.z - walkDistance);
		_myTransform.LookAt(target);
	}

	private void RotateCamera() {

		Quaternion rotation = Quaternion.Euler(_y, _x, 0);
		Vector3 position = rotation * new Vector3(0.0f, 0.0f , -walkDistance) + target.position;
		
		_myTransform.rotation = rotation;
		_myTransform.position = position;

	}
		
}
