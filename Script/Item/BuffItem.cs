using UnityEngine;
using System.Collections;

public class BuffItem : Item {

	private Hashtable buffs;

	public BuffItem() {

		buffs = new Hashtable();
	}

	public BuffItem( Hashtable ht ) {
		buffs = new Hashtable();

		foreach( DictionaryEntry pair in buffs ) {
			buffs.Add ( pair.Key, pair.Value );
		}
	}

	public void AddBuff( BaseStat bs, int modifier) {

		buffs[bs.Name] = modifier;
	}

	public void RemoveBuff( BaseStat bs ) {

		buffs.Remove( bs.Name );
	}

	public int BuffCount() {
		return buffs.Count;
	}

	public Hashtable GetBuffs() {
		return buffs;
	}
}
