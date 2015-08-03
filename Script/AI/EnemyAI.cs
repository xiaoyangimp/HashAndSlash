using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public Transform target;
	private float moveSpeed = 2f;
	private float maxDistance;

	// Use this for initialization
	void Start () {
		maxDistance = 2f;

		GameObject go = GameObject.FindWithTag ("Player");
		target = go.transform;

	}
	
	// Update is called once per frame
	void Update () {

		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position), rotationSpeed * Time.deltaTime);

		transform.LookAt (target);

		if(	Vector3.Distance( transform.position , target.transform.position ) > maxDistance) 
		{
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
		}
	}
}
