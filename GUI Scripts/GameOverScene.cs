using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour {

	public Image go;
	public Text go2;

	GameObject obj;
	PlayerHealth alive;

	// Use this for initialization
	void Start () {
	
		go = go.GetComponent<Image> ();
		go.enabled = false;
		go2 = go2.GetComponent<Text> ();
		go2.enabled = false;

		obj = GameObject.FindGameObjectWithTag ("user");
		alive = (PlayerHealth)obj.GetComponent (typeof (PlayerHealth));

	}
	
	public void Update() {

		float time = 10f;  // 10 seconds

		if (alive.ifDead()) {

			go.enabled = true;
			go2.enabled = true;

			while (time > 0) {
				time -= Time.deltaTime;
			}
			SceneManager.LoadScene (0);
		}

	}
}
