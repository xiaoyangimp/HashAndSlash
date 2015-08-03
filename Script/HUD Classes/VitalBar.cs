/// <summary>
/// Vital bar.
/// Jiyu Xiao
/// Jul 20, 2015
/// 
/// This class is responsible to display the vital bars of a player or mobs
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VitalBar : MonoBehaviour {

	private bool _isPlayer;		//bool value that indicate a player vital bar or mob vital bar

	private float _maxBarLength;	// this is the length of the vital bar
	private float _curBarLength;
	private float _barHeight;
	
	private Image _maxBar;
	private Image _curBar;
	private Text _healthValue;

	void Awake() {
		_isPlayer = gameObject.name == "PlayerHealthSet" ? true : false;
		
		_maxBar = gameObject.transform.GetChild(0).GetComponent<Image>() as Image;
		_curBar = gameObject.transform.GetChild(1).GetComponent<Image>() as Image;
		_healthValue = gameObject.transform.GetChild(2).GetComponent<Text>() as Text;
		
		_maxBarLength = _maxBar.rectTransform.rect.width;
		_curBarLength = _curBar.rectTransform.rect.width;
		_barHeight = _curBar.rectTransform.rect.height;
		_healthValue.text = _curBarLength + "/" + _maxBarLength;
	}

	// Use this for initialization
	void Start () {

		OnEnable ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Called when gameObject enabled
	public void OnEnable() {
		if( _isPlayer ) {
			Messenger<int, int>.AddListener("Player Health Update", OnChangeHealthBarSize);
		}
		else {
			ToggleDisplay ( false );
			Messenger<int, int>.AddListener("Mob Health Update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener ("Show Mob Vitalbars", ToggleDisplay);
		}
	}

	// Called when gameObject disabled
	public void OnDisable() {
		if( _isPlayer ) {
			Messenger<int, int>.RemoveListener("Player Health Update", OnChangeHealthBarSize);
		}
		else {
			Messenger<int, int>.RemoveListener("Mob Health Update", OnChangeHealthBarSize);
		}
	}


	// this method will calculate the length of vital bar presented in the screen
	public void OnChangeHealthBarSize ( int curh, int maxh ) {

		_curBarLength =  ( (float) curh / maxh ) * _maxBarLength ;	//this calculated the current bar length based on the vital percentage

		_curBar.rectTransform.sizeDelta = new Vector2( _curBarLength , _barHeight);	// update the vital bar image size
		_healthValue.text = curh + "/" + maxh;										// update the text on the vital bar
	
	}

	// Set the type of the vital bar
	public void SetPlayerHealth( bool b ) {
		_isPlayer = b;
	}

	public void ToggleDisplay( bool show) {

		_maxBar.enabled = show;
		_curBar.enabled = show;
		_healthValue.enabled = show;


	}
}
