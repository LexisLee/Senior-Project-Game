using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.ThirdPerson 
{

	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(EnemyHealth))]
	[RequireComponent(typeof(AICharacterControl))]
	[RequireComponent(typeof(EnemyAttack))]

	public class Spawning : MonoBehaviour {
		int count;                      // Number of enemies to spawn
		int size = 0, i = 0;
		bool flag = false;

		public Transform[] location;    // Random location of new enemy
		public GameObject AI;           // Original Prefab
		private GameObject newEnemy;    // New Enemy
		float timer = 15f;

		// setting up AI objects
		Rigidbody m_rigidbody;
		CapsuleCollider m_capsule;
		private string[] names = new string[] { "AI1", "AI2", "AI3", "AI4", "AI5", "AI6", "AI7", "AI8", "AI9", "AI10" };
		private string[] childname = new string[] { "Hand1", "Hand2", "Hand3", "Hand4", "Hand5", "Hand6", "Hand7", "Hand8", "Hand9", "Hand10" };
		private IList taken = new string[10];

		// Use this for initialization
		void Start () {

			Scene scene = SceneManager.GetActiveScene();

			if (scene.name == "Level_1") {
				count = 3;
			} 
			else if (scene.name == "Level_2") {
				count = 5;
			}

			InvokeRepeating ("Spawn", timer, timer);

		}

		void Spawn () {

			//spawnObject.GetComponentInChildren<stomp> ().controller = Resources.Load ("PlayersFolderLocation/Player");

			if (count > 0) {
				int index = Random.Range (0, location.Length);
				newEnemy = Instantiate (AI.gameObject, location [index].position, location [index].rotation) as GameObject;
				if (newEnemy == null) {
					Debug.Log ("Problem instantiating");
					return;
				}
				do {
					flag = false;
					int n = Random.Range (0, names.Length);
					newEnemy.name = names [n];
					for (i = 0; i < size; i++) {
						if (newEnemy.name == (string)taken [i]) {
							flag = true;
						}
					}
				} while (flag == true);
				taken [i] = newEnemy.name;

				// Disconnecting clone from original AI prefab
				GameObject temp = new GameObject();
				newEnemy.transform.parent = temp.transform;
				newEnemy.transform.parent = null;
				Destroy (temp);

				// Setting up stats of New Enemy
				//
				// Enemy Health Script
				EnemyHealth EnemyHea = newEnemy.AddComponent<EnemyHealth> ();

				// Rigidbody
				m_rigidbody = newEnemy.GetComponent<Rigidbody>();
				m_rigidbody.mass = 150f;

				// Capsule Collider
				m_capsule = newEnemy.GetComponent<CapsuleCollider>();
				m_capsule.material = (PhysicMaterial)Resources.Load ("Character");
				if (m_capsule.material == null) {
					Debug.Log ("No material on AI");
				}

				// Setting Target
				AICharacterControl AIC = (AICharacterControl)newEnemy.GetComponent (typeof(AICharacterControl));
				GameObject player = GameObject.FindGameObjectWithTag ("user");
				AIC.target = player.transform;

				// Setting Stopping Distance
				NavMeshAgent SD = (NavMeshAgent)newEnemy.GetComponent (typeof(NavMeshAgent));
				SD.stoppingDistance = 1.0f;

				//Enemy Attack Script
				GameObject hand = new GameObject();
				GameObject x = GameObject.Find ("EthanRightHand");
				do {
					flag = false;
					int y = Random.Range (0, childname.Length);
					x.name = childname [y];
					for (i = 0; i < size; i++) {
						if (x.name == (string)taken [i]) {
							flag = true;
						}
					}
				} while (flag == true);
				taken [i] = x.name;
				size++;
				hand.transform.position = GameObject.Find(x.name).transform.position;
				hand.transform.parent = GameObject.Find(x.name).transform;
				EnemyAttack EnemyAtt = hand.AddComponent<EnemyAttack> ();
				hand.name = "HitPlayer";

				//
				// End of set up of stats for New Enemy

				size++;    // The size of the list of names of new enemies
				count--;   // Keeping track of how many enemies to spawn
			}

			GameObject[] AllEnemies = GameObject.FindGameObjectsWithTag ("Enemy");

			if (AllEnemies.Length == 0) {
				//Debug.Log ("here1");
				Scene scene = SceneManager.GetActiveScene ();
				if (scene.name == "Level_1") {
					//Debug.Log ("here2");
					SceneManager.LoadScene (2);
				}
			}
		
		}

		void Update() {
			StartCoroutine (WaitTime ());
		}
			
		IEnumerator WaitTime() {
			yield return new WaitForSeconds (15f);
		}
			
	}
}
