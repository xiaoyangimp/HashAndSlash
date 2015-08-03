using System.Collections.Generic; 			//Generic is added for list usage


/// <summary>
/// Modified stat.
/// Jiyu Xiao
/// July 19, 2015
/// 
/// The base class for all statues that will be modified by attributes
/// </summary>
public class ModifiedStat : BaseStat {

	private List<ModifyingAttribute> _mods;
	private int _modValue;

	public ModifiedStat() {
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}

	/// <summary>
	/// Adds the modifier.
	/// </summary>
	/// <param name="mod">Mod.</param>
	public void AddModifier ( ModifyingAttribute mod ) {
		_mods.Add(mod);
	}

	/// <summary>
	/// Iterate througt all the attributes modifying it in _mods and 
	/// add the affecting value to it (BaseValue * Ratio)
	/// </summary>
	private void CalculateModValue() {
		_modValue = 0;

		if( _mods.Count > 0 ) {
			foreach ( ModifyingAttribute m in _mods) {
				_modValue += (int) ( m.attribute.AdjustBaseValue * m.ratio );
			}
		}
	}

	/// <summary>
	/// Gets the adjusted base value.
	/// </summary>
	/// <value>The adjusted base value.</value>
	public int AdjustedBaseValue {
		get {return AdjustBaseValue + _modValue;}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update() {
		CalculateModValue();
	}

	public string GetModifyingStatString() {
		string temp = "";

		for( int i = 0; i < _mods.Count; i ++ ) {

			temp += _mods[i].attribute.Name;
			temp += "-";
			temp += _mods[i].ratio;

			if( i < _mods.Count - 1 ) {
				temp += "|";
			}
		}

		return temp;
	}
}

/// <summary>
/// Struct that holds the attribute and the ratio that will affact and modifying attributes
/// </summary>
public struct ModifyingAttribute {
	public Attribute attribute;
	public float ratio;

	/// <summary>
	/// Initializes a new instance of the <see cref="ModifyingAttribute"/> struct.
	/// </summary>
	/// <param name="attr">Attr.</param>
	/// <param name="ra">Ra.</param>
	public ModifyingAttribute ( Attribute attr, float ra ) {
		attribute = attr;
		ratio = ra;
	}
}