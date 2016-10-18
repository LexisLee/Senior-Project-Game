using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(EnemyHealth))]

	public class Attacking : MonoBehaviour {
		  
		public int attackDamge;         // The amount taken from attack

		EnemyHealth eph = null;
		GameObject enem = null;
		bool hitEnemy = false;
		float timer = 0.1f;

		// Use this for initialization 
		void Start () {

			Scene scene = SceneManager.GetActiveScene();

			if (scene.name == "Level_1") {
				attackDamge = 20;
			} 
			else if (scene.name == "Level_2") {
				attackDamge = 40;
			}

		}

		public void SwungAtEnemy(bool hitEn) {
			hitEnemy = hitEn;
		}
		
		// Update is called once per frame
		void FixedUpdate () {

			RaycastHit hit;
			float dist = 0.4f;

			enem = GameObject.FindGameObjectWithTag ("Enemy");
			if ( enem != null )
				eph = (EnemyHealth)enem.GetComponent (typeof(EnemyHealth));

			if (Physics.Raycast (transform.position, transform.forward, out hit, dist)) {
				timer -= Time.deltaTime;
				if ((hit.collider.gameObject.tag == "Enemy" && hitEnemy) && (timer < 0)) {
					Debug.Log ("Hit enemy");
					eph.TakeDamage (attackDamge);
					timer = 0.1f;
				} 
			}
		
		}
	}
}
