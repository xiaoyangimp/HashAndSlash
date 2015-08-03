using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour {

	private PlayerCharacter _toon;
	private const int START_POINTS = 350;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_VALUE = 50;
	private int pointsLeft;

	private bool controlbool = false;


	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 20;
	private const int STAT_LABEL_WIDTH = 100;
	private const int BASEVALUE_WIDTH = 30;

	private int statStartingPos = 40;


	public GUISkin mySkin;

	public GameObject PlayerPrefab;

	// Use this for initialization
	void Start () {
		GameObject pc = Instantiate ( PlayerPrefab , Vector3.zero, Quaternion.identity ) as GameObject;

		pc.name = "pc";

//		_toon = new PlayerCharacter();
		_toon = pc.GetComponent<PlayerCharacter>();
		_toon.Awake ();

		pointsLeft = START_POINTS;


		for( int i = 0; i < Enum.GetValues (typeof(AttributeName)).Length; i++ ) {
			_toon.GetAttribute(i).BaseValue = STARTING_VALUE;
			pointsLeft -= ( STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE );
		}

		_toon.StateUpdate();
	}
	

	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		GUI.skin = mySkin;

		DisplayName ();
		DisplayPointLeft();
		DisplayAttributes ();
		DisplayVitals();
		DisplaySkills();

		if(_toon.Name.Equals ("") || pointsLeft > 0 )
			DisplayCreateLabel();
		else
			DisplayCreateButton();
	}

	private void DisplayName() {
		GUI.Label( new Rect(10, 10, 50, 25), "Name" );
		_toon.Name = GUI.TextField ( new Rect ( 65, 10, 100, 25) , _toon.Name );
	}

	private void DisplayAttributes() {
		for( int i = 0; i < Enum.GetValues (typeof(AttributeName)).Length; i++ ) {
			GUI.Label ( new Rect( OFFSET,
			                     statStartingPos + LINE_HEIGHT * i,
			                     STAT_LABEL_WIDTH,
			                     LINE_HEIGHT ),
			           ((AttributeName)i).ToString() );

			GUI.Label ( new Rect( STAT_LABEL_WIDTH + OFFSET,
			                     statStartingPos + LINE_HEIGHT * i,
			                     BASEVALUE_WIDTH,
			                     LINE_HEIGHT ),
			           _toon.GetAttribute(i).AdjustBaseValue.ToString()) ;

			if( GUI.RepeatButton ( new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_WIDTH,
			                          statStartingPos + LINE_HEIGHT * i,
			                          LINE_HEIGHT,
			                          LINE_HEIGHT),
			                "-" ) ) {
				if(!controlbool) {

					if( _toon.GetAttribute( i ).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE ) {
						_toon.GetAttribute (i).BaseValue--;
						pointsLeft++;
					}

					_toon.StateUpdate();

					controlbool = true;
					StartCoroutine ("Wait");
				}

			}

			if ( GUI.RepeatButton ( new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_WIDTH + LINE_HEIGHT,
			                           statStartingPos + LINE_HEIGHT * i,
			                           LINE_HEIGHT,
			                           LINE_HEIGHT),
			                 "+" ) ) {
				if(!controlbool) {

					if( pointsLeft > 0 ) {
						_toon.GetAttribute ( i ).BaseValue++;
						pointsLeft--;
					}

					_toon.StateUpdate();

					controlbool = true;
					StartCoroutine ("Wait");
				}
			}
		}
	}

	private void DisplayVitals() {
		for( int i = 0; i < Enum.GetValues (typeof(VitalName)).Length; i++ ) {
			GUI.Label ( new Rect( OFFSET,
			                     statStartingPos + LINE_HEIGHT * (i + 7),
			                     STAT_LABEL_WIDTH,
			                     LINE_HEIGHT ),
			           ((VitalName)i).ToString() );

			GUI.Label ( new Rect( STAT_LABEL_WIDTH + OFFSET,
			                     statStartingPos + LINE_HEIGHT * (i + 7),
			                     BASEVALUE_WIDTH,
			                     LINE_HEIGHT ),
			           _toon.GetVital(i).AdjustedBaseValue.ToString()) ;
		}
	}

	private void DisplaySkills() {
		for( int i = 0; i < Enum.GetValues (typeof(SkillName)).Length; i++ ) {
			GUI.Label ( new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_WIDTH + LINE_HEIGHT * 2 + OFFSET * 3,
			                     statStartingPos + LINE_HEIGHT * i,
			                     STAT_LABEL_WIDTH,
			                     LINE_HEIGHT ),
			           ((SkillName)i).ToString() );

			GUI.Label ( new Rect( OFFSET + STAT_LABEL_WIDTH + BASEVALUE_WIDTH + LINE_HEIGHT * 2 + OFFSET * 4 + STAT_LABEL_WIDTH,
			                     statStartingPos + LINE_HEIGHT * i,
			                     BASEVALUE_WIDTH,
			                     LINE_HEIGHT ),
			           _toon.GetSkill(i).AdjustedBaseValue.ToString()) ;
		}
	}

	private void DisplayPointLeft() {
		GUI.Label( new Rect(250, 10, STAT_LABEL_WIDTH, LINE_HEIGHT), "Points Left: " + pointsLeft.ToString () );
	}

	private void DisplayCreateLabel() {
		GUI.Label ( new Rect(OFFSET,
		                      statStartingPos + LINE_HEIGHT * 10,
		                      STAT_LABEL_WIDTH,
		                      LINE_HEIGHT)
		           , "Creating Character", "Button" );
	}

	private void DisplayCreateButton() {
		if ( GUI.Button ( new Rect(OFFSET,
		                      statStartingPos + LINE_HEIGHT * 10,
		                      STAT_LABEL_WIDTH,
		                      LINE_HEIGHT)
		            , "Create" ) ) {

			GameSettings gsScript = GameObject.Find ("__GameSettings").GetComponent<GameSettings>();

			//change the initial value
			UpdateCurVitalValues();

			gsScript.SaveCharacterToData();

			Application.LoadLevel ("Level1");

		}
	}

	private void UpdateCurVitalValues() {

		for( int i = 0; i < Enum.GetValues (typeof(VitalName)).Length; i++ ) {
			_toon.GetVital(i).CurValue = _toon.GetVital(i).AdjustedBaseValue; 

		}

	}

	private IEnumerator Wait() {
		yield return new WaitForSeconds(0.1f);
		controlbool = false;
	}
}
