using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AdvancedMove))]
[RequireComponent (typeof (SphereCollider))]
public class AI : MonoBehaviour {

	private enum State {
		Idle,							// doing nothing
		Init,							// initialize the transform information
		Setup,							// setup up the parameters required
		Search,							// find the player
		Attack,							// attack the player
		Retreat,						// run back to it spawn point
		Flee,							// run to the nearest spawn point
		Taunting						// mobs are taunting
	}


	public float perceptionRadius = 10f;
	public float baseMeleeRange = 3f;
	public float wanderRange = 10f;

	public Transform target;			// generally the player's transform as a target
	private Transform _myTransform;
	private Transform _home;
	private SphereCollider _sphereCollider;

	private const float ROTATION_DAMP = 0.3f;
	private const float FORWARD_DAMP = 0.5f;
	
	private State _state;
	private bool _alive = true;
	private bool _taunting = false;


	// Use this for initialization
	void Start () {

		_state = AI.State.Init;


		StartCoroutine( "FSM" );

//		_sphereCollider = GetComponent<SphereCollider>();
//		CharacterController cc = GetComponent<CharacterController>();
//		
//		if( _sphereCollider == null ) {
//			Debug.Log ("There's no sphere collider on this mob.");
//			return;
//		}
//		else {
//			_sphereCollider.isTrigger = true;
//		}
//
//		if( cc == null ) {
//			Debug.Log(" There's no character controller on this mob");
//		}
//		else {
//			_sphereCollider.center = cc.center;
//			_sphereCollider.radius = perceptionRadius;
//		}
//
//		_myTransform = transform;

	}

	private IEnumerator FSM() {
		while( _alive) {

			if( _taunting ) {
				_taunting = false;
				_state = AI.State.Search;
			}

			switch ( _state ) {
			case State.Init:
				Init ();
				break;
			case State.Setup:
				Setup();
				break;
			case State.Search:
				Search();
				break;
			case State.Attack:
				Attack();
				break;
			case State.Retreat:
				Retreat();
				break;
			case State.Flee:
				Flee();
				break;
			case State.Taunting:
				Taunt();
				break;
			}

			if( _taunting )
				yield return new WaitForSeconds(_myTransform.animation["taunt"].length + 1);
			else if ( !target )
				yield return new WaitForSeconds( Random.Range (1f, 1.2f) );

			yield return null;
		}
	}

	private void Init() {
		_myTransform = transform;
		_home = transform.parent.transform;
		_myTransform.animation["taunt"].speed = 2f;

		_sphereCollider = GetComponent<SphereCollider>();
		
		if( _sphereCollider == null ) {
			Debug.Log ("There's no sphere collider on this mob.");
			return;
		}

		_state = AI.State.Setup; 
	}

	private void Setup() {

		CharacterController cc = GetComponent<CharacterController>();

		_sphereCollider.center = cc.center;
		_sphereCollider.radius = perceptionRadius;
		_sphereCollider.isTrigger = true;

		_state = AI.State.Search;
	}

	private void Search() {

		Move ();

		if( !target ) {
			_state = AI.State.Taunting;
		}
	}

	private void Attack() {

		Move ();
		_state = AI.State.Retreat;
	}

	private void Retreat() {

		//_myTransform.LookAt ( _myTransform );
		Move ();
		_state = AI.State.Search;
	}

	private void Flee() {

		Move ();
		_state = AI.State.Search;
	}

	private void Taunt() {

		_taunting = true;
		animation.Play ( "taunt" );

	}

/*	// Update is called once per frame
	void Update () {
		if( target ) {

			Vector3 dir = (target.position - _myTransform.position).normalized;
			float direction = Vector3.Dot ( dir, transform.forward );

			float dist = Vector3.Distance( target.position, _myTransform.position );

			if( direction > FORWARD_DAMP && dist > baseMeleeRange ) {
				SendMessage ( "MoveMeForward", AdvancedMove.Forward.forward);
			}
			else {
				SendMessage ( "MoveMeForward", AdvancedMove.Forward.none);
			}


			dir = (target.position - _myTransform.position).normalized;
			direction = Vector3.Dot ( dir, transform.right );

			if ( direction > ROTATION_DAMP ) {
				SendMessage( "RotateMe", AdvancedMove.Turn.right);
			}
			else if (direction < -ROTATION_DAMP ) {
				SendMessage( "RotateMe", AdvancedMove.Turn.left);
			}
			else {
				SendMessage( "RotateMe", AdvancedMove.Turn.none);
			}

		}
		else {
			SendMessage ( "MoveMeForward", AdvancedMove.Forward.none);
			SendMessage( "RotateMe", AdvancedMove.Turn.none);
		}

	}
*/


	// function that is called for the mob's motion logic
	private void Move() {
		if( target ) {
			
			Vector3 dir = (target.position - _myTransform.position).normalized;
			float direction = Vector3.Dot ( dir, transform.forward );
			
			float dist = Vector3.Distance( target.position, _myTransform.position );
			
			if( direction > FORWARD_DAMP && dist > baseMeleeRange ) {
				SendMessage ( "MoveMeForward", AdvancedMove.Forward.forward);
			}
			else {
				SendMessage ( "MoveMeForward", AdvancedMove.Forward.none);
				_state = AI.State.Attack;
			}
			
			
			dir = (target.position - _myTransform.position).normalized;
			direction = Vector3.Dot ( dir, transform.right );
			
			if ( direction > ROTATION_DAMP ) {
				SendMessage( "RotateMe", AdvancedMove.Turn.right);
			}
			else if (direction < -ROTATION_DAMP ) {
				SendMessage( "RotateMe", AdvancedMove.Turn.left);
			}
			else {
				SendMessage( "RotateMe", AdvancedMove.Turn.none);
			}
			
		}
		else {
			SendMessage ( "MoveMeForward", AdvancedMove.Forward.none);
			SendMessage( "RotateMe", AdvancedMove.Turn.none);


		}
	}
	

	//	function called when player enters the perception range
	public void OnTriggerEnter(Collider other) {

		if( other.CompareTag("Player") ) {
			target = other.transform;
			_alive = true;
			StartCoroutine( "FSM" );
		}
	}

	// function called when plaer leave the perception range
	public void OnTriggerExit( Collider other) {

		if( other.CompareTag("Player") ) {
			target = _home;


		//	_alive = false;
		}
	}
}
