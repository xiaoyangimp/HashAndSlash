using UnityEngine;

public class PlayerCharacter : BaseCharacter {

	void Update() {
		int test = (int) Random.Range (20f, 100f);

		Messenger<int, int>.Broadcast( "Player Health Update", test, 100);
	}
}
