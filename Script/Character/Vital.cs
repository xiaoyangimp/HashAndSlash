/// <summary>
/// Vital.
/// Jul 22, 2015
/// Jiyu Xiao
/// 
/// This is the vitals class that describe health, energy, mana etc. values of characters or mobs
/// </summary>
public class Vital : ModifiedStat {

	private int _curValue;		// the current value of this value

	/// <summary>
	/// Initializes a new instance of the <see cref="Vital"/> class.
	/// </summary>
	public Vital () 
	{
		_curValue = 0;
		ExpToLevel = 50;
		LevelModifer = 1.1f;
	}

	/// <summary>
	/// Gets or sets the current value.
	/// 
	/// if current value is greater than the adjusted base value ( max value), then 
	/// set it to max value
	/// </summary>
	/// <value>The current value.</value>
	public int CurValue 
	{ 
		get { 
			if( _curValue > AdjustedBaseValue ) _curValue = AdjustedBaseValue;

			return _curValue; 
		} 
		set { _curValue = value; } 
	}
}

/// <summary>
/// Vital name.
/// 
/// A list of names of vital instances
/// </summary>
public enum VitalName {
	Health,
	Energy,
	Mana,
}