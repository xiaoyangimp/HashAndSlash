using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	public GameObject target;
	private float attackTimer;
	private float cooldown;

	// Use this for initialization
	void Start () {
		attackTimer = 0f;
		cooldown = 2.2f;
	}
	
	// Update is called once per frame
	void Update () {
		attackTimer -= Time.deltaTime;
		attackTimer = attackTimer < 0 ? 0 : attackTimer;

		if (target != null && Input.GetKeyUp (KeyCode.F)) 
		{
			if(attackTimer == 0 ) 
			{
				Attack();
				attackTimer = cooldown;
			}
		}

	}

	private void Attack() 
	{
		float distance = Vector3.Distance (target.transform.position, transform.position);

		Vector3 dir = (target.transform.position - transform.position). normalized;

		float direction = Vector2.Dot(dir, transform.forward);

		if (distance < 3f && direction > 0f) 
		{
				EnemyHealth ehb = (EnemyHealth)target.GetComponent ("EnemyHealth");
				ehb.AdjustCurrentHealth (-5);
		}

	}
}
