using UnityEngine;
using System.Collections;
using System;

public class BaseCharacter : MonoBehaviour {

	private string _name;
	private int _level;
	private uint _freeExp;

	private Attribute[] _attrs;
	private Vital[] _vitals;
	private Skill[] _skills;

	public void Awake() {
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;

		_attrs = new Attribute[ Enum.GetValues (typeof(AttributeName)).Length ];
		_vitals = new Vital[ Enum.GetValues (typeof(VitalName)).Length ];
		_skills = new Skill[ Enum.GetValues (typeof(SkillName)).Length ];

		SetupAttributes();
		SetupVitals();
		SetupSkills();
	}


	public String Name {
		get { return _name; }
		set { _name = value; }
	}

	public int Level {
		get { return _level; }
		set { _level = value; }
	}

	public uint FreeExp {
		get { return _freeExp; }
		set { _freeExp = value; }
	}

	public void AddExp ( uint exp ) {
		_freeExp += exp;

		CalculateLevel ();
	}

	// take avg of all the players skills and assign that as the player level
	public void CalculateLevel() {

	}

	private void SetupAttributes() {
		for( int i = 0; i < _attrs.Length; i++ ) {
			_attrs[i] = new Attribute();
			_attrs[i].Name = ( (AttributeName) i ).ToString();
		}
	}

	private void SetupVitals() {
		for( int i = 0; i < _vitals.Length; i++ ) {
			_vitals[i] = new Vital();
		}

		SetupVitalModifier();
	}

	private void SetupSkills() {
		for( int i = 0; i < _skills.Length; i++ ) {
			_skills[i] = new Skill();
		}

		SetupSkillModifier();
	}

	public Attribute GetAttribute(int index) {
		return _attrs[index];
	}

	public Vital GetVital(int index) {
		return _vitals[index];
	}

	public Skill GetSkill(int index) {
		return _skills[index];
	}

	private void SetupVitalModifier() {
		//health
		GetVital((int) VitalName.Health).AddModifier(new ModifyingAttribute( GetAttribute((int)AttributeName.Constitution), 0.5f));

		//energy
		GetVital((int) VitalName.Energy).AddModifier(new ModifyingAttribute( GetAttribute((int)AttributeName.Constitution), 1f));


		//mana
		GetVital((int) VitalName.Mana).AddModifier(new ModifyingAttribute( GetAttribute((int)AttributeName.Willpower), 1f));
	}

	private void SetupSkillModifier() {

		//melee offence
		GetSkill ( (int) SkillName.Melee_Offence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Might), .33f) );
		GetSkill ( (int) SkillName.Melee_Offence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Nimbleness), .33f) );

		//melee defence
		GetSkill ( (int) SkillName.Melee_Defence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Speed), .33f) );
		GetSkill ( (int) SkillName.Melee_Defence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Constitution), .33f) );

		//magic offence
		GetSkill ( (int) SkillName.Magic_Offence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Concentration), .33f) );
		GetSkill ( (int) SkillName.Magic_Offence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Willpower), .33f) );

		//magic defence
		GetSkill ( (int) SkillName.Magic_Defence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Concentration), .33f) );
		GetSkill ( (int) SkillName.Magic_Defence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Willpower), .33f) );

		//ranged offence
		GetSkill ( (int) SkillName.Ranged_Offence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Concentration), .33f) );
		GetSkill ( (int) SkillName.Ranged_Offence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Speed), .33f) );

		//ranged defence
		GetSkill ( (int) SkillName.Ranged_Defence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Speed), .33f) );
		GetSkill ( (int) SkillName.Ranged_Defence ).AddModifier ( new ModifyingAttribute( GetAttribute((int)AttributeName.Nimbleness), .33f) );
	}

	public void StateUpdate() {

		foreach( Vital v in _vitals ) {
			v.Update ();
		}

		foreach( Skill s in _skills ) {
			s.Update ();
		}
	}
}
