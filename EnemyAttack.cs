using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.ThirdPerson {

	[RequireComponent(typeof(PlayerHealth))]

	public class EnemyAttack : MonoBehaviour {

		//public float BetweenAttacks = 0.6f;     // The time between attacks in seconds
		public int attackDamge;              // The amount taken from attack

		PlayerHealth ph;
		GameObject playerCharacter;
		bool hitEnemy = false;
		float timer = 0.1f;

		// Use this for initialization 
		void Start () {
			Scene scene = SceneManager.GetActiveScene();

			if (scene.name == "Level_1") {
				attackDamge = 5;
			} 
			else if (scene.name == "Level_2") {
				attackDamge = 10;
			}

		}

		public void SwungAtUser(bool hitEn) {
			hitEnemy = hitEn;
			//Debug.Log (hitEnemy);
		}

		// Update is called once per frame
		void FixedUpdate () {

			RaycastHit hit;
			float dist = 0.4f;

			playerCharacter = GameObject.FindGameObjectWithTag("user");
			ph = (PlayerHealth)playerCharacter.GetComponent (typeof(PlayerHealth));

			if (Physics.Raycast (transform.position, transform.forward, out hit, dist)) {
				timer -= Time.deltaTime;
				if ((hit.collider.gameObject.tag == "user" && hitEnemy) && (timer < 0)) {   // added in is not dead and hitting check parameters
					Debug.Log ("Hit player");                                  // also slow down hit rate
					ph.DamageDone (attackDamge);
					timer = 0.3f;
				}
			}
		}
	}
}