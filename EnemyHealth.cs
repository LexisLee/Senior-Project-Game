using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Animator))]

	public class EnemyHealth: MonoBehaviour
	{
		public int startingHealth;                                  // The amount of health the player starts the game with.
		public int currentHealth;                                   // The current health the player has.
		//public Image damageImage;                                    // Reference to an image to flash on the screen on being hurt.
		//public AudioClip deathClip;                                 // The audio clip to play when the player dies.
		public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
		public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


		Animator anim;                                              // Reference to the Animator component.
		//AudioSource playerAudio;                                    // Reference to the AudioSource component.
		                                              
		bool damaged;                                               // True when the player gets damaged.

		void Awake ()
		{
			Scene scene = SceneManager.GetActiveScene();

			// Setting up the references.
			//anim = GetComponent <Animator> ();
			//playerAudio = GetComponent <AudioSource> ();

			if (scene.name == "Level_1") {
				startingHealth = 100;
			} 
			else if (scene.name == "Level_2") {
				startingHealth = 200;
			}

			// Set the initial health of the player.
			currentHealth = startingHealth;

		}


		void Update ()
		{
			// If the player has just been damaged...
			if(damaged)
			{
				// ... set the colour of the damageImage to the flash colour.
				//damageImage.color = flashColour;
			}
			// Otherwise...
			else
			{
				// ... transition the colour back to clear.
				//damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
			}

			// Reset the damaged flag.
			damaged = false;
		}


		public void TakeDamage (int amount)
		{

			//GameObject Object = Spawning.newEnemy;
			anim = this.gameObject.GetComponent <Animator> ();

			// Set the damaged flag so the screen will flash.
			damaged = true;

			try {
		    //animation is being hit
			anim.SetTrigger ("Damage");
			Debug.Log ("Animation Called");
			} catch{};

			// Reduce the current health by the damage amount.
			currentHealth -= amount;

			// Play the hurt sound effect.
			//playerAudio.Play ();

			// If the player has lost all it's health and the death flag hasn't been set yet...
			if(currentHealth <= 0)
			{
				// ... it should die.
				Death ();
			}
		}


		void Death ()
		{

			try {
				// Tell the animator that the player is dead.
				anim.SetTrigger ("Die");
				Destroy(this.gameObject);
			} catch{};

			// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
			//playerAudio.clip = deathClip;
			//playerAudio.Play ();

		}       
	}
}