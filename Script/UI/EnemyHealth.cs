using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int maxHealth = 100;
	public int curHealth = 100;
	public Texture2D t;
	
	public float healthBarLength;
	
	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width / 3;
	}
	
	// Update is called once per frame
	void Update () {
		//AdjustCurrentHealth (1);
	}
	
	// called for creating element in GUI
	void OnGUI() 
	{
		var Rcur = new Rect (10, 40, healthBarLength, 20);
		var Rtotal = new Rect (10, 40, Screen.width / 3, 20);
		
		GUI.Box (Rcur, "");
		GUI.Box ( Rtotal, curHealth + "/" + maxHealth );
	}
	
	/* int adj with adjust, positive for healing, negative for damage*/
	public void AdjustCurrentHealth( int adj ) 
	{
		curHealth += adj;
		curHealth = curHealth > maxHealth ? maxHealth : curHealth;
		
		if (curHealth < 0) {
			curHealth = 0;
		}
		
		healthBarLength = Screen.width / 3 * ( curHealth / (float)maxHealth ) ;
	}
}
