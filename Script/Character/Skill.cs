/// <summary>
///Skill.
/// 
/// July 22, 2015
/// Jiyu Xiao
/// 
/// This is the class that describes the skill of a character 
/// </summary>
public class Skill : ModifiedStat {

	private bool _known;			// bool value that determine whether the character knows this skill

	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	public Skill() {
		_known = false;
		ExpToLevel = 25;
		LevelModifer = 1.5f;
	}

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skill"/> is known.
	/// </summary>
	/// <value><c>true</c> if known; otherwise, <c>false</c>.</value>
	public bool Known { 
		get { return _known;}
		set { _known = value;}

	}
}

/// <summary>
/// Skill name.
/// 
/// A list of names of skills that a character can learn
/// </summary>
public enum SkillName {
	Melee_Offence,
	Melee_Defence,
	Ranged_Offence,
	Ranged_Defence,
	Magic_Offence,
	Magic_Defence
}
