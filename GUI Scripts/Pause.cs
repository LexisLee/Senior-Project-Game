using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pause : MonoBehaviour {

	public Canvas PauseM;
	public Button MainMenu;
	//public Button Controls;
	public Button Quit;
	public Button Resume;

	// Use this for initialization
	void Start () {
	
		MainMenu = MainMenu.GetComponent<Button> ();
		//Controls = Controls.GetComponent<Button> ();
		Quit = Quit.GetComponent<Button> ();

		PauseM.enabled = false;
	}
	
	public void Update() {
		if (Input.GetKey (KeyCode.Escape)) {
			PauseM.enabled = true;
			Time.timeScale = 0.0f;
			MainMenu.enabled = true;
			//Controls.enabled = true;
			Quit.enabled = true;
		}
	}

	public void PushMainMenu() {
		SceneManager.LoadScene (0);
	}

	public void PushQuit() {
		Application.Quit ();
	}

	public void PushResume() {
		PauseM.enabled = false;
		Time.timeScale = 1.0f;
	}
}
