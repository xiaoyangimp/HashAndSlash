using UnityEngine;
using System.Collections;

public class Weapon : BuffItem
{
	private int _minDamage;
	private int _dmgVar;
	private float _range;
	private DamageType _dmgType;

	public Weapon() {
		_minDamage = 0;
		_dmgVar = 0;
		_range = 0;
		_dmgType = DamageType.Fire;
	}

	public Weapon( int dmg, int dvar, float range, DamageType dt ) {
		_minDamage = dmg;
		_dmgVar = dvar;
		_range = range;
		_dmgType = dt;
	}

	public int MinDamage {
		get { return _minDamage; }
		set { _minDamage = value; }
	}

	public int DmgVar {
		get { return _dmgVar; }
		set { _dmgVar = value; }
	}

	public float Range {
		get { return _range; }
		set { _range = value; }
	}

	public DamageType DmgType {
		get { return _dmgType; }
		set { _dmgType = value; }
	}
}

public enum DamageType {
	Fire,
	Ice,
	Lightning,
	Acid
}