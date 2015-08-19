using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class AdvancedMove : MonoBehaviour {	// class which only care about the move logic, input are included in another class

	public enum State {						//used to represent the current state of the player, using FSM
		Idle,
		Init,
		Setup,
		Action
	}

	public enum Turn {						//parameters used in the rotation and strafe
		left = -1,
		none = 0,
		right = 1
	}
	public enum Forward {					//parameters used in advancing
		back = -1,
		none = 0,
		forward = 1
	}



	public float rotateSpeed = 60f;
	public float walkSpeed = 5f;
	public float strafeSpeed = 2.5f;
	public float runMultiplier = 2f;
	public float backMultiplier = 0.5f;
	public float gravity = 20f;
	public float airTime = 0;				// How long have the character been in the air since the last leaving ground
	public float fallTime = 0.5f;			//the length of time we have to be falling
	public float jumpHeight = 5;
	public float jumpTime = 1.5f;

	private CollisionFlags _collisionFlags;	// collision flags we have from the previous frame
	private Vector3 _moveDirection;			//the direction the character is moving
	private Transform _myTransform;			//cached transform
	private CharacterController _charcon;	//cached CharacterController

	private Turn _turn;
	private Forward _forward;
	private Turn _strafe;
	private bool _run;
	private bool _jump;

	private State _state;

	// called before the script starts
	void Awake() {
		_myTransform = transform;
		_charcon = gameObject.GetComponent<CharacterController>();
		_state = AdvancedMove.State.Init;
	}

	// Use this for initialization
	IEnumerator Start () {

		while(true) {

			switch(_state) {
			case State.Init:
				Init ();
				break;
			case State.Setup:
				Setup ();
				break;
			case State.Action:
				ActionPicker ();
				break;
			}

			yield return null;
		}

	}

	private void Init() {
		if( !GetComponent<CharacterController>() ) return;
		if( !GetComponent<Animation>() ) return;

		_state = AdvancedMove.State.Setup;
	}

	private void Setup() {
		animation.Stop ();   				// stop all the pre-defined animation
		
		animation.wrapMode = WrapMode.Loop;
		
		animation["jump"].layer = 1;		// set the priority (weight) of the animations
		animation["jump"].wrapMode = WrapMode.Once;
		
		Idle ();

		_turn = AdvancedMove.Turn.none;
		_forward = AdvancedMove.Forward.none;
		_strafe = AdvancedMove.Turn.none;
		_run = false;
		_jump = false;

		_state = AdvancedMove.State.Idle;
	}

	void Update() {
		ActionPicker();
	}

	private void ActionPicker() {

		_myTransform.Rotate ( 0, (int)_turn * Time.deltaTime * rotateSpeed, 0 );

		if( _charcon.isGrounded ) {

			// Debug.Log("on ground");

			_moveDirection = new Vector3( (int)_strafe, 0, (int)_forward);
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;

			if( _forward != AdvancedMove.Forward.none )  {

				if( _run && (int)_forward > 0 ) {
					Run ();
					_moveDirection *= runMultiplier;
				}
				else if ( (int)_forward < 0) {
					_moveDirection *= backMultiplier;
					Walk();
				}
				else {
					Walk ();
				}

			}
			else if ( _strafe != AdvancedMove.Turn.none ) {

				if( (int) _strafe < 0 ) {
					strafeLeft();
				}
				else {
					strafeRight ();
				}

			}
			else {
				Idle();
			}

			if( _jump ) {
				if( airTime < jumpTime ) {
					_moveDirection.y += jumpHeight;
					Jump ();
					_jump = false;
				}
			}

		}
		else {

			// Debug.Log("Not grounded");

			if( ( _collisionFlags & CollisionFlags.CollidedBelow ) == 0 ) {
				airTime += Time.deltaTime;
				if( airTime > fallTime ) {
					//Fall ();
					airTime = 0;
				}
			}
		}

		_moveDirection.y -= gravity * Time.deltaTime;  // used for smooth falling
		_collisionFlags = _charcon.Move ( _moveDirection * Time.deltaTime);
	}

	// Input Management

	public void MoveMeForward( Forward z ) {
		_forward = z;
	}

	public void ToggleRun() {
		_run = !_run;
	}

	public void RotateMe( Turn y) {
		_turn = y;
	}

	public void Strafe( Turn x ) {
		_strafe = x;
	}

	public void JumpUp() {
		_jump = true;
	}

	// animation functions

	private void Idle() {
		animation.CrossFade("idle");
	}

	private void Walk() {
		animation.CrossFade("walk");
	}

	private void Run() {
		animation.CrossFade("run");
	}

	private void strafeLeft() {
		animation.CrossFade("strafeLeft");
	}

	private void strafeRight() {
		animation.CrossFade("strafeRight");
	}

	private void Jump() {
		animation.CrossFade("jump");
	}

	private void Fall() {
		animation.CrossFade("idle"); // temporarily no fall animation 
	}
}
