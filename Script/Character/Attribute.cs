/// <summary>
/// Attribute.
/// Jiyu Xiao
/// Jul 19, 2015
/// 
/// This is the class of all the character attributes uses
/// </summary>
public class Attribute : BaseStat {

	new public const int STARTING_EXP_COST = 50; // this is the starting exp cost for all attributes

	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute() {
		Name = "";
		ExpToLevel = STARTING_EXP_COST;
		LevelModifer = 50;
	}


}

/// <summary>
/// List of Attribute Names
/// </summary>
public enum AttributeName {
	Might,
	Constitution,
	Nimbleness,
	Speed,
	Concentration,
	Willpower,
	Charisma
}