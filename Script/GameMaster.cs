using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public GameObject playerCharacter;
	public Camera mainCamera;
	public GameObject gameSettings;

	public float zOffset;
	public float yOffset;
	public float xRotOffset;

	private GameObject _pc;
	private PlayerCharacter _pcScript;
	private CameraMove _mainCameraScript;
	private Vector3 _playerSpawnPosition;	// the place of the player spawn point

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find (GameSettings.PLAYER_SPAWN_POINT);
		_playerSpawnPosition = new Vector3 (570, 6, 630);

		if( go == null ) {

			go = new GameObject( GameSettings.PLAYER_SPAWN_POINT );
			go.transform.position = _playerSpawnPosition;
		}

		_pc = Instantiate( playerCharacter, go.transform.position, Quaternion.identity ) as GameObject;
		_pc.name = "pc";

		_pcScript = _pc.GetComponent<PlayerCharacter>();

		zOffset = -2.5f;
		yOffset = 2.5f;
		xRotOffset = 15f;

		mainCamera.transform.position = new Vector3(_pc.transform.position.x , _pc.transform.position.y + yOffset, _pc.transform.position.z + zOffset);
		mainCamera.transform.Rotate(xRotOffset, 0, 0);
		_mainCameraScript = mainCamera.GetComponent<CameraMove>();
		_mainCameraScript.target = _pc.transform;

		LoadCharacter();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadCharacter() {

		GameObject gs = GameObject.Find ("__GameSettings");

		if(gs == null ) {
			GameObject gs1 = Instantiate ( gameSettings, Vector3.zero, Quaternion.identity ) as GameObject;
			gs1.name = "__GameSettings";
		}

		GameSettings gsScript = GameObject.Find ("__GameSettings").GetComponent<GameSettings>();

		gsScript.LoadCharacterData();
	}
}
