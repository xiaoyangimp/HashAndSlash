using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AdvancedMove))]
public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetButton("Move Forward") ) {

			if( Input.GetAxis ("Move Forward") > 0 ) {
				SendMessage ( "MoveMeForward", AdvancedMove.Forward.forward);
			}
			else {
				SendMessage ( "MoveMeForward", AdvancedMove.Forward.back);
			}
		}

		if( Input.GetButtonUp("Move Forward") ) {
			SendMessage ( "MoveMeForward", AdvancedMove.Forward.none);
		}

		if( Input.GetButtonDown ("Run") ) {
			SendMessage ( "ToggleRun" );
		}

		if( Input.GetButtonUp ("Run") ) {
			SendMessage ( "ToggleRun" );
		}

		if( Input.GetButton("Rotate Player") ) {
			
			if( Input.GetAxis ("Rotate Player") > 0 ) {
				SendMessage ( "RotateMe", AdvancedMove.Turn.right);
			}
			else {
				SendMessage ( "RotateMe", AdvancedMove.Turn.left);
			}
		}

		if( Input.GetButtonUp("Rotate Player") ) 
			SendMessage ( "RotateMe", AdvancedMove.Turn.none);

		if( Input.GetButton("Strafe") ) {
			
			if( Input.GetAxis ("Strafe") > 0 ) {
				SendMessage ( "Strafe", AdvancedMove.Turn.right);
			}
			else {
				SendMessage ( "Strafe", AdvancedMove.Turn.left);
			}
		}

		if( Input.GetButtonUp("Strafe") )
			SendMessage ( "Strafe", AdvancedMove.Turn.none);


		if( Input.GetButton ("Jump") ) {
			SendMessage( "JumpUp" );
		}
	}
}
