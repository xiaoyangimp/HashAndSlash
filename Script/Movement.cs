using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour {
	public float rotateSpeed = 60f;
	public float walkSpeed = 5f;
	public float strafeSpeed = 2.5f;
	public float runSpeed = 10f;
	public float backMultiplier = 0.5f;

	private Transform _myTransform;
	private CharacterController _charcon;

	void Awake() {
		_myTransform = transform;
		_charcon = gameObject.GetComponent<CharacterController>();
	}

	// Use this for initialization
	void Start () {
		animation.wrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {

		if( !_charcon.isGrounded ) {
			_charcon.Move (Vector3.down * Time.deltaTime * 10f);
		}

		Turn();
		Walk();
		Strafe();

	}

	private void Turn() {
		if( Mathf.Abs( Input.GetAxis("Rotate Player") ) > 0 )  {
			_myTransform.Rotate ( 0, Input.GetAxis("Rotate Player") * Time.deltaTime * rotateSpeed, 0 );
			
		}
	}

	private void Walk() {
		if( Mathf.Abs( Input.GetAxis("Move Forward") ) > 0 ) {


			if( Input.GetButton("Run") && Input.GetAxis("Move Forward") > 0 ) {
				animation.CrossFade("run");
				_charcon.SimpleMove ( _myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Move Forward") *  runSpeed );
			}
			else {
				animation.CrossFade("walk"); 

				if( Input.GetAxis("Move Forward") > 0 ) {
					_charcon.SimpleMove ( _myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Move Forward") *  walkSpeed );
				}
				else {
					_charcon.SimpleMove ( _myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Move Forward") *  walkSpeed * backMultiplier );
				}
			}

		}
		else {
			animation.CrossFade("idle");
		}
	}

	private void Strafe() {
		if( Mathf.Abs( Input.GetAxis("Strafe") ) > 0 ) {


			_charcon.SimpleMove ( _myTransform.TransformDirection(Vector3.right) * Input.GetAxis("Strafe") *  strafeSpeed );

			if( Input.GetAxis("Strafe") > 0 ) {
				animation.CrossFade("strafeRight");
			}
			else {
				animation.CrossFade("strafeLeft");
			}
		}
	}
	
}
