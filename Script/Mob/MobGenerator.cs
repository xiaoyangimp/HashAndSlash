using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {

	public enum State {
		Idle,
		Initialize,
		Setup,
		SpawnMobs
	}

	public GameObject[] mobPrefabs;		//an array that holds all of the prefabs of mobs
	public GameObject[] spawnPoints;	//an array that holds the reference of spawn points existed in this scene

	private State state ;	


	void Awake() {
		state = MobGenerator.State.Initialize;	// initialize before the start function
	}

	// Use this for initialization
	IEnumerator Start () {


		while(true) {
			switch(state) {
			case State.Initialize: 
				Initialize();
				break;
			case State.Setup: 
				Setup ();
				break;
			case State.SpawnMobs: 
				SpawnMob();
				break;
			case State.Idle: 
				SpawnMob();
				break;
			}

			yield return 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//called in the initialize state
	private void Initialize() {

		if( !CheckForMobPrefabs() ) return;
		if( !CheckForSpawnPoints() ) return;

		state = MobGenerator.State.Setup;
	}

	// called in the setup state
	private void Setup() {


		state = MobGenerator.State.SpawnMobs;
	}

	// called in the spawn mob state
	private void SpawnMob() {

		GameObject[] gos = checkSpawnPoint();

		for( int i = 0; i < gos.Length; i++ ) {
			GameObject tempMob = Instantiate( mobPrefabs[ Random.Range ( 0, mobPrefabs.Length ) ], 
			                                 gos[i].transform.position,
			                                 Quaternion.identity
			                                 ) as GameObject;

			tempMob.transform.SetParent( gos[i].transform );
		}

		state = MobGenerator.State.Idle;
	}

	//check at least one Mob is on the map
	private bool CheckForMobPrefabs() {

		if( mobPrefabs.Length > 0 ) return true;
		else return false;
	}

	//check at lease one spawn point on the map
	private bool CheckForSpawnPoints() {

		if( spawnPoints.Length > 0 ) return true;
		else return false;
	}

	// find all the spawn points that do not have any mob prototype inside
	private GameObject[] checkSpawnPoint() {

		List<GameObject> list = new List<GameObject>();

		for( int i = 0; i < spawnPoints.Length; i++ ) {
			if ( spawnPoints[i].transform.childCount == 0 ) {
				list.Add ( spawnPoints[i] );
			}
		}

		return list.ToArray();
	}
}
