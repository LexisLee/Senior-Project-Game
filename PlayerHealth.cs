using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.ThirdPerson 
{

	[RequireComponent(typeof(Animator))]

	public class PlayerHealth : MonoBehaviour
	{
		public int startingHealth;                                    // The amount of health the player starts the game with.
		public int currentHealthPlayer;                                   // The current health the player has.
		//public Slider healthSlider;                                 // Reference to the UI's health bar.
		//public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
		//public AudioClip deathClip;                                 // The audio clip to play when the player dies.
		public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
		public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
		public float ch;                                             // current health bar width

		GUIStyle style;
		Texture2D texture;

		Animator anim;                                              // Reference to the Animator component.
		//AudioSource playerAudio;                                    // Reference to the AudioSource component.

		bool isDead;                                                // Whether the player is dead.
		bool damaged;                                               // True when the player gets damaged.

		bool LevelOn = false;                                       // true if the level screen is on


		void Awake ()
		{
			Scene scene = SceneManager.GetActiveScene();

			// Setting up the references.
			anim = GetComponent <Animator> ();

			//playerAudio = GetComponent <AudioSource> ();

			if (scene.name == "Level_1") {
				startingHealth = 100;
			}
			else if (scene.name == "Level_2") {
				startingHealth = 300;
			}

			// Set the initial health of the player.
			currentHealthPlayer = startingHealth;

			style = new GUIStyle ();
			texture = new Texture2D (128, 128);

			isDead = false;

			ch = 0f;

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


		public void DamageDone (int amount)
		{
			// Set the damaged flag so the screen will flash.
			damaged = true;

			//animation is being hit
			anim.SetTrigger ("Damage");

			// Reduce the current health by the damage amount.
			currentHealthPlayer -= amount;

			if (startingHealth != currentHealthPlayer) {
				ch = (currentHealthPlayer / (float)startingHealth) * 250f;
			}

			// Play the hurt sound effect.
			//playerAudio.Play ();

			// If the player has lost all it's health and the death flag hasn't been set yet...
			if(currentHealthPlayer <= 0 && !isDead)
			{
				// ... it should die.
				Death ();
			}
		}
			
		void OnGUI() 
		{
			if (!LevelOn) {

				int fullBar = 250;
			
				// color of health bar which was set to green
				for (int y = 0; y < texture.height; ++y) {
					for (int x = 0; x < texture.width; ++x) {
						Color color = new Color (0, 90, 0);
						texture.SetPixel (x, y, color);
					}
				}
				texture.Apply ();

				style.normal.background = texture;
				if (startingHealth == currentHealthPlayer) {
			
					GUI.Box (new Rect (Screen.width - 260, Screen.height - 32, fullBar, 20), new GUIContent (""), style);  // ( x position, y position, width of box, heighth of box)
				} 
				else {
					GUI.Box (new Rect (Screen.width - 260, Screen.height - 32, ch, 20), new GUIContent (""), style);  // ( x position, y position, width of box, heighth of box)
				}
			}

		}
			
		void Death ()
		{
			// Set the death flag so this function won't be called again.
			isDead = true;

			// Tell the animator that the player is dead.
			anim.SetTrigger("Die");

			// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
			//playerAudio.clip = deathClip;
			//playerAudio.Play ();

		}      

		// Tells GameOverScene Script that the user is dead
		public bool ifDead() {
			return isDead;
		}

		// Determines if the level name gui is up then sets health bar to unactive
		public void LevelNameOn(bool Level) {
			LevelOn = Level;
		}
	}
}