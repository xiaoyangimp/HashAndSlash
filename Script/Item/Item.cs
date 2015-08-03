/// <summary>
/// Item.
/// July 29, 2015
/// Jiyu Xiao
/// </summary>

using UnityEngine;

public class Item {

	private string _name;
	private int _value;
	private RarityType _rarity;

	public Item() {
		_name = "Default";
		_value = 0;
		_rarity = RarityType.Common;
	}

	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>The name.</value>
	public string Name {
		get {return _name;}
		set {_name = value;}
	}

	/// <summary>
	/// Gets or sets the value.
	/// </summary>
	/// <value>The value.</value>
	public int Value {
		get {return _value;}
		set {_value = value; }
	}

	/// <summary>
	/// Gets or sets the item rarity.
	/// </summary>
	/// <value>The item rarity.</value>
	public RarityType Rarity {
		get {return _rarity;}
		set {_rarity = value;}
	}
}

public enum RarityType {
	Common,
	Uncommon,
	Rare
}