using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float BetweenAttacks = 0.6f;     // The time between attacks in seconds
	public int attackDamge = 1;         // The amount taken from attack

	PlayerHealth ph;
	GameObject playerCharacter;

	// Use this for initialization 
	void Start () {

		playerCharacter = GameObject.FindGameObjectWithTag("user");
		ph = (PlayerHealth)playerCharacter.GetComponent (typeof(PlayerHealth));

	}

	// Update is called once per frame
	void FixedUpdate () {

		RaycastHit hit;
		float dist = 0.25f;

		if (Physics.Raycast (transform.position, transform.forward, out hit, dist)) {
			if (hit.collider.gameObject.name == "ThirdPersonController") {   // added in is not dead and hitting check parameters
				//Debug.Log ("Hit player");                                  // also slow down hit rate
				ph.DamageDone (attackDamge);
			}
		}
	}
}