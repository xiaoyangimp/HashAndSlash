using UnityEngine;
using System.Collections;


[AddComponentMenu("Environments/Timed Lighting")]
public class TimeToLight : MonoBehaviour {


	public void OnEnable() {
		Debug.Log("Listener logged");
		Messenger<bool>.AddListener("Morning Time", ToggleLight );
	}

	public void OnDisable() {
		Messenger<bool>.RemoveListener("Morning Time", ToggleLight );
	}

	private void ToggleLight ( bool t ) {
		gameObject.GetComponent<Light>().enabled =  !t ;
	}
}
