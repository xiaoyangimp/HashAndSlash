/// <summary>
/// Game time management 
/// </summary>
using UnityEngine;
using System.Collections;


public class GameTime : MonoBehaviour {

	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;

	public Transform[] sun;								// the array to store the suns (stars)
	private Stars[] _sunScript;							// the array that store the script for suns

	public float morningLight;							// parameters defines the time from 0-1 that all outdoor lights turns off
	public float nightLight;							// parameters defines the time from 0-1 that all outdoor lights turns on

	public float dayCycleInMinitus = 1;					// the definition of a day in real minitus
	private float _dayCycleInSeconds;					// the defination of a dya in real seconds
	
	public Color ambLightMax;							// the color of the maximum ( brightest ) ambient light )
	public Color ambLightMin;							// the color of the minimum ( lightest ) ambient light 

	public float starttime = 0;

	private const float DEGREE_PER_SECOND = 360 / DAY;	// the degree that the sun will move per second

	private float _sunRotation;							// the degree tha the sun will move per second
	private float _timeOfDay;							// the current time of day, calculated by adding delta time

	public float sunRise = 0.2f;						// sun rise point
	public float sunSet = 0.8f;							// sun set point
	public float Noon = 0.5f;							// Noon point 
	

	// Use this for initialization
	void Start () {

		// initialize the definition of day cycle in second and sunrise, sunset points
		_dayCycleInSeconds = dayCycleInMinitus * MINUTE;

		// initially set the skybox shader _Blend parameter to 0
		RenderSettings.skybox.SetFloat( "_Blend", 0 );


		_sunScript = new Stars[sun.Length];

		/* initializing scripts*/
		for ( int cnt = 0; cnt < _sunScript.Length; cnt++ ) {
			Stars s = sun[cnt].GetComponent<Stars>();

			if( s == null) {

				Debug.Log ("sun script is not yet set up");
				sun[cnt].gameObject.AddComponent<Stars>();
				s = sun[cnt].GetComponent<Stars>();

			}

			_sunScript[cnt] = s;
		}


		_timeOfDay = starttime * _dayCycleInSeconds;	// initialize the time 
		_sunRotation = DEGREE_PER_SECOND * DAY / ( _dayCycleInSeconds );	// initialize the degree the the sun will rotate for one second

		/* Rotate all stored suns*/
		for(int i = 0; i < sun.Length; i++ ) 
			sun[i].Rotate ( new Vector3 ( 360 * starttime, 0, 0 ) );

		//setup the lighting for all suns
		SetupLighting();
	}


	// Update is called once per frame
	void Update () {

		/* Rotate all stored suns*/
		for(int i = 0; i < sun.Length; i++ ) 
			sun[i].Rotate ( new Vector3 ( _sunRotation, 0, 0 ) * Time.deltaTime );

		//update time
		_timeOfDay += Time.deltaTime;

		// change sky box parameters
		BlendSkybox( );

		// obtain the time that passed in a day, from 0 - 1
		float _timeInDay = ( _timeOfDay / _dayCycleInSeconds ) % 1;

		//Debug.Log( _timeInDay * 24 );

		// change the lighting according to the time in a day
		if( _timeInDay  > sunRise && _timeInDay < Noon ) {
			AdjustLighting( true );
		}
		else if ( _timeInDay > Noon && _timeInDay < sunSet) {
			AdjustLighting( false );
		}

		//change the ourdoor lighting according to the time in a day by Messenger broadcasting
		if(  _timeInDay > morningLight && _timeInDay < nightLight ) {
			Messenger<bool>.Broadcast ("Morning Time", true);
		}
		else {
			Messenger<bool>.Broadcast ("Morning Time", false);
		}
		
	}

	/// <summary>
	/// Blends the skybox.
	/// </summary>
	private void BlendSkybox() {


		float temp = _timeOfDay / _dayCycleInSeconds;

		// calculate the blend of the sky box by a cos function
		// which mimics the sun behavior as a cos wave
		temp = Mathf.Cos(temp * 2 * Mathf.PI) * 0.5f + 0.5f;


		RenderSettings.skybox.SetFloat( "_Blend", temp );
	}

	/// <summary>
	/// Setups the lighting atstart.
	/// </summary>
	private void SetupLighting() {
		for( int i = 0; i < _sunScript.Length; i++ ) {


			if( _sunScript[i].giveLight ) {

				float _timeInDay = ( _timeOfDay / _dayCycleInSeconds ) % 1;
				
				// change the lighting according to the time in a day
				if( _timeInDay  > sunRise && _timeInDay < Noon ) {
					AdjustLighting( true );
				}
				else if ( _timeInDay > Noon && _timeInDay < sunSet) {
					AdjustLighting( false );
				}

			}
			else {
				sun[i].GetComponent<Light>().intensity = 0;
			}
		}
	}

	/// <summary>
	/// Adjusts the lighting during each update.
	/// </summary>
	/// <param name="brighten">If set to <c>true</c> brighten.</param>
	private void AdjustLighting( bool brighten ) {

		float temp = ( _timeOfDay / _dayCycleInSeconds ) % 1;


		for( int i = 0; i < _sunScript.Length; i++ ) {

			float minL =  _sunScript[i].minBrightness; 	// get the the minimum light for each sun
			float maxL =  _sunScript[i].maxBrightness;	// get the the maximum light for each sun

			if( brighten == true ) {	// function that is called during the sun rise time to noon
			
				if( _sunScript[i].giveLight ) {

					//update sun light intensity
					sun[i].GetComponent<Light>().intensity = minL + (temp - sunRise) / (Noon - sunRise ) * ( maxL - minL );

					//update render ambientlight
					RenderSettings.ambientLight = ambLightMin + (temp - sunRise ) / ( Noon - sunRise ) * (ambLightMax - ambLightMin );
					
				}
			}

			else {

				if( _sunScript[i].giveLight ) { // function that is called during the noon to sun set

					//update sun light intensity
					sun[i].GetComponent<Light>().intensity = minL + (sunSet - temp) / ( sunSet - Noon ) * ( maxL - minL );

					
					//update render ambientlight
					RenderSettings.ambientLight = ambLightMin + (sunSet - temp) / ( sunSet - Noon ) * (ambLightMax - ambLightMin );
				}
			}
		}


	}
}
