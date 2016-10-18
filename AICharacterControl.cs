using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
	[RequireComponent(typeof (EnemyAttack))]

    public class AICharacterControl : MonoBehaviour
    {
		public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
		private NavMeshPath path;
		private float timer;
		EnemyAttack hitEnemy;
		GameObject hand;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
			path = new NavMeshPath ();

	        agent.updateRotation = false;
	        agent.updatePosition = true;

			timer = 2f;

			hand = GameObject.Find ("HitPlayer");
        }

        private void Update()
		{
			if (hand != null)
				hitEnemy = (EnemyAttack)hand.GetComponent(typeof(EnemyAttack));

			// test to make sure the object is on the nav mesh
			if (agent.isOnNavMesh) {
				
				// Makes sure there is a target
				if (target != null) {
					
					if (NavMesh.CalculatePath (transform.position, target.position, NavMesh.AllAreas, path)) {
						agent.SetDestination (target.position);
						character.movePlayer (agent.velocity, false, false, false);
					}
						
					if (agent.remainingDistance <= agent.stoppingDistance) {
						transform.LookAt (target.transform.position);
						timer -= Time.deltaTime;
						if (timer < 0) { 
							character.movePlayer (agent.velocity, false, false, true);
							hitEnemy.SwungAtUser (true);
							timer = 1.5f;
						}
					}
				}
			} 


			else {
				Debug.LogWarning ("Warning: agent is not on the navmesh.");
			}
			
        }
			
    }
}
