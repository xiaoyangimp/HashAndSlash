/// <summary>
/// 
/// BaseStat.cs
/// Jiyu Xiao
/// Jul. 19, 2015
/// 
/// This is the basic class of for all statues.
/// 
///  
/// </summary>
public class BaseStat {

	public const int STARTING_EXP_COST = 100;  // publicly accessibile value for base stats to start at

	private int _baseValue;			//the base value of this stat
	private int _buffValue;			//the amount of th buff to this stat
	private int _expToLevel;		//the total amount of exp needed to raise this skill
	private float _levelModifier;	// the modifer applied to the exp needed to raise the skill
	private string _name; 						//name of the attribute
	
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class with default value
	/// </summary>
	public BaseStat() 
	{
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;
		_expToLevel = STARTING_EXP_COST;
	}

	#region Basic Setters and Getters
	/// <summary>
	/// Gets or sets the base value.
	/// </summary>
	/// <value> _baseValue.</value>
	public int BaseValue { get{ return _baseValue;} set{ _baseValue = value;} }

	/// <summary>
	/// Gets or sets the buff value.
	/// </summary>
	/// <value> _buffValue.</value>
	public int BuffValue { get{ return _buffValue;} set{ _buffValue = value;} }

	/// <summary>
	/// Gets or sets the exp to level.
	/// </summary>
	/// <value>T _expToLevel.</value>
	public int ExpToLevel { get{ return _expToLevel;} set{ _expToLevel = value;} }

	/// <summary>
	/// Gets or sets the level modifer.
	/// </summary>
	/// <value>The _levelmodifer.</value>
	public float LevelModifer { get{ return _levelModifier;} set{ _levelModifier = value;} }

	/// <summary>
	/// Gets or sets the name of attribute.
	/// </summary>
	/// <value>The name.</value>
	public string Name { get {return _name;} set { _name = value;}}
	#endregion

	/// <summary>
	/// Realculates the exp tp level.
	/// </summary>
	/// <returns>The exp tp level.</returns>
	private int CalculateExpToLevel() {
		return (int) (_expToLevel * _levelModifier ) ;
	}

	/// <summary>
	/// Levels up for a higher expToLevelm, then increase the baseValue by 1
	/// </summary>
	public void LevelUp() {
		_expToLevel = CalculateExpToLevel();
		_baseValue++;
	}

	/// <summary>
	/// Gets the adjust base value to the sum of BaseValue and BuffValue
	/// </summary>
	/// <value>The adjust base value.</value>
	public int AdjustBaseValue {
		get {return _baseValue + _buffValue;}
	}
}
