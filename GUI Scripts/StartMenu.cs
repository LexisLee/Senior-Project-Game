using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public Canvas QuitMenu;
	public Button Play;
	public Button Exit;

	// Use this for initialization
	void Start () {
	
		QuitMenu = QuitMenu.GetComponent<Canvas>();
		Play = Play.GetComponent<Button>();
		Exit = Exit.GetComponent<Button>();

		QuitMenu.enabled = false;

	}

	// Happens when exit is pressed
	public void ExitDown() {
		QuitMenu.enabled = true;
		Play.enabled = false;      
		Exit.enabled = false;
	}

	// Push No in Quit Menu
	public void NoDown() {
		QuitMenu.enabled = false;
		Play.enabled = true;
		Exit.enabled = true;
	}

	// Push play
	public void PlayDown() {
		SceneManager.LoadScene (1);     // Used 1 instead of the name so the very first level pops up no matter the scene name
	}

	// Push Yes in Quit Menu
	public void ExitGame() {
		Application.Quit ();            // Terminates the game application
	}
}
