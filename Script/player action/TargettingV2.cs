/// <summary>
/// Targetting script for Hash and Slash game
/// July 29, 2015
/// Jiyu Xiao
/// 
/// This script provides the targetting system for any mobs that is with in the player's
/// pre-defined sight
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargettingV2 : MonoBehaviour {
	
	public List<Transform> targets;		// a list that saves all the targets that the player can see
	public Transform selectedTarget;	// a reference to the selected target transform
	private Transform myTransform;		// a reference to the player target transform
	public GameObject mobHealthbarSet;
	private Transform _mhs;
	
	// Use this for initialization
	void Start () {
		
		myTransform = transform;
		selectedTarget = null;
		targets = new List<Transform>();
	}

	// Update is called once per frame
	void Update () {

		if( Input.GetKeyDown( KeyCode.Tab ) )  
		{	

			targets.Clear ();
			AddAllEnemies();

			Debug.Log( targets.Count );
			if( targets.Count == 0 ) return ;

			Debug.Log ( targets[0].name );

			SortTargetByDistance();
			
			if( selectedTarget == null ) {
				selectedTarget = targets[0];
				SelectTarget();
				
			}
			else {
				Debug.Log ( selectedTarget.name );

				changeTarget();
				selectedTarget = targets[ (targets.IndexOf(selectedTarget) + 1 ) % targets.Count ] ;
				SelectTarget();
			}

		}
		
	}

	
	public void AddAllEnemies() 
	{
		GameObject[] es = GameObject.FindGameObjectsWithTag("Enemy");
		foreach( GameObject e in es ) 
		{
			targets.Add(e.transform);
		}
	}
	
	private void SortTargetByDistance()
	{
		
		targets.Sort( delegate( Transform t1, Transform t2 ) 
		             { 
			return Vector3.Distance (t1.position, myTransform.position).CompareTo(Vector3.Distance (t2.position, myTransform.position));   
		});
	}

	private void SelectTarget () {
		Transform name = selectedTarget.FindChild("MobName");

		if( name == null ) {
			Debug.LogError("Can not find the Name on" + selectedTarget.name ) ;
			return;
		}

		name.GetComponent<TextMesh>().color = Color.red;

		selectedTarget.GetComponent<Mob>().DisplayHealth();

		Messenger<bool>.Broadcast( "Show Mob Vitalbars", true );
	}

	private void changeTarget() {

		Transform name = selectedTarget.FindChild("MobName");
		
		if( name == null ) {
			Debug.LogError("Can not find the Name on" + selectedTarget.name ) ;
			return;
		}
		
		name.GetComponent<TextMesh>().color = Color.yellow;
	}

	private void DeselectTarget() {
		Transform name = selectedTarget.FindChild("MobName");
		
		if( name == null ) {
			Debug.LogError("Can not find the Name on" + selectedTarget.name ) ;
			return;
		}
		
		name.GetComponent<TextMesh>().color = Color.yellow;

		Messenger<bool>.Broadcast( "Show Mob Vitalbars", false );
		selectedTarget = null;
	}
}