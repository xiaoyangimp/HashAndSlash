using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {

	public const string PLAYER_SPAWN_POINT = "Player Spawn Point";	//this is the name of the game object that the player will start
	public 

	void Awake() {
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveCharacterToData() {
		GameObject pc = GameObject.Find("pc");
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();

		PlayerPrefs.GetString ("Player Name", pcClass.Name);

		for( int i = 0; i < Enum.GetValues (typeof(AttributeName)).Length; i++ ) {
			PlayerPrefs.SetInt ( ((AttributeName)i).ToString() + " - Base Value" , pcClass.GetAttribute(i).BaseValue );
			PlayerPrefs.SetInt ( ((AttributeName)i).ToString() + " - EXP To Level" , pcClass.GetAttribute(i).ExpToLevel );
		}

		for( int i = 0; i < Enum.GetValues (typeof(VitalName)).Length; i++ ) {
			PlayerPrefs.SetInt ( ((VitalName)i).ToString() + " - Base Value" , pcClass.GetVital(i).BaseValue );
			PlayerPrefs.SetInt ( ((VitalName)i).ToString() + " - Current Value" , pcClass.GetVital(i).CurValue );
			PlayerPrefs.SetInt ( ((VitalName)i).ToString() + " - EXP To Level" , pcClass.GetVital(i).ExpToLevel );

			//PlayerPrefs.SetString ( ((VitalName)i) + " - Mods", pcClass.GetVital(i).GetModifyingStatString() );

		}

		for( int i = 0; i < Enum.GetValues (typeof(SkillName)).Length; i++ ) {
			PlayerPrefs.SetInt ( ((SkillName)i).ToString() + " - Base Value" , pcClass.GetSkill(i).BaseValue );
			PlayerPrefs.SetInt ( ((SkillName)i).ToString() + " - EXP To Level" , pcClass.GetSkill(i).ExpToLevel );

			//PlayerPrefs.SetString ( ((SkillName)i) + " - Mods", pcClass.GetSkill(i).GetModifyingStatString() );

		}
	}

	public void LoadCharacterData() {
		GameObject pc = GameObject.Find("pc");
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
		pcClass.Name = PlayerPrefs.GetString ("Player Name", "Name_Me");
		
		for( int i = 0; i < Enum.GetValues (typeof(AttributeName)).Length; i++ ) {
			pcClass.GetAttribute(i).BaseValue = PlayerPrefs.GetInt ( ((AttributeName)i).ToString() + " - Base Value" , 0);
			pcClass.GetAttribute(i).ExpToLevel = PlayerPrefs.GetInt ( ((AttributeName)i).ToString() + " - EXP To Level" , 0);
		}
		
		for( int i = 0; i < Enum.GetValues (typeof(VitalName)).Length; i++ ) {


			pcClass.GetVital(i).BaseValue  = PlayerPrefs.GetInt ( ((VitalName)i).ToString() + " - Base Value" , 0);
			pcClass.GetVital(i).ExpToLevel = PlayerPrefs.GetInt ( ((VitalName)i).ToString() + " - EXP To Level" , 0);

			pcClass.GetVital(i).Update ();

			pcClass.GetVital(i).CurValue = PlayerPrefs.GetInt ( ((VitalName)i).ToString() + " - Current Value" , 1);

		}
			
		for( int i = 0; i < Enum.GetValues (typeof(SkillName)).Length; i++ ) {
			pcClass.GetSkill(i).BaseValue = PlayerPrefs.GetInt ( ((SkillName)i).ToString() + " - Base Value" , 0);
			pcClass.GetSkill(i).ExpToLevel  = PlayerPrefs.GetInt ( ((SkillName)i).ToString() + " - EXP To Level" , 0);
			
			pcClass.GetSkill (i).Update ();
		}
	}
}
