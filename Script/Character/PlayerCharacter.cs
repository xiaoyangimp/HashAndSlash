using UnityEngine;

public class PlayerCharacter : BaseCharacter {

	void Update() {
		int test = 100;

		Messenger<int, int>.Broadcast( "Player Health Update", test, 100);
	}
}
