using UnityEngine;
using System.Collections;

public class Mob : BaseCharacter {

	public int curHealth;
	public int maxHealth;

	// Use this for initialization
	void Start () {
		GetAttribute((int) AttributeName.Constitution).BaseValue = 100;
		GetVital( (int) VitalName.Health ).Update ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void DisplayHealth () {
		Messenger<int, int>.Broadcast( "Mob Health Update", curHealth, maxHealth);
	}
}
