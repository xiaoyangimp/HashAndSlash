using UnityEngine;
using System.Collections;

[AddComponentMenu("Day Night Cycle/Sun")]
public class Stars : MonoBehaviour {


	public float maxBrightness;
	public float minBrightness;

	public bool giveLight = false;

	void Awake() {

		if( GetComponent<Light>() != null ) {
			giveLight = true;
		}
	}
}
