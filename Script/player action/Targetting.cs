using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour {

	public List<Transform> targets;
	public Transform selectedTarget;
	private Transform myTransform;
	private Color defc = Color.grey;

	// Use this for initialization
	void Start () {

		myTransform = transform;
		selectedTarget = null;
		targets = new List<Transform>();

		AddAllEnemies();
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

	// Update is called once per frame
	void Update () {

		if( Input.GetKeyDown( KeyCode.Tab ) )  
		{
			SortTargetByDistance();

			if( selectedTarget == null ) {
				selectedTarget = targets[0];
				defc = selectedTarget.renderer.material.color;
				selectedTarget.renderer.material.color = Color.red;

			}
			else {
				selectedTarget.renderer.material.color = defc;
				selectedTarget = targets[ (targets.IndexOf(selectedTarget) + 1 ) % targets.Count ] ;
				selectedTarget.renderer.material.color = Color.red;
			}

			PlayerAttack pa = (PlayerAttack) GetComponent("PlayerAttack");
			pa.target = selectedTarget.gameObject;
		}

	}
}
