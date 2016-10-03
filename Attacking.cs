using UnityEngine;
using System.Collections;

public class Attacking : MonoBehaviour {
	  
	public int attackDamge = 15;         // The amount taken from attack

	EnemyHealth eph;
	GameObject enem;
	bool hitEnemy = false;

	// Use this for initialization 
	void Start () {
			
		enem = GameObject.FindGameObjectWithTag("Enemy");
		eph = (EnemyHealth)enem.GetComponent (typeof(EnemyHealth));

	}

	public void SwungAtEnemy(bool hitEn) {
		hitEnemy = hitEn;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		RaycastHit hit;
		float dist = 0.31f;

		if (Physics.Raycast (transform.position, transform.forward, out hit, dist)) {
			if (hit.collider.gameObject.tag == "Enemy" && hitEnemy) {
				Debug.Log ("Hit enemy");
				eph.TakeDamage (attackDamge);
			}
		}
	
	}
}
