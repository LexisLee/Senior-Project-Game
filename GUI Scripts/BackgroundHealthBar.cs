using UnityEngine;
using System.Collections;

public class BackgroundHealthBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		
		GUI.Box (new Rect(Screen.width - 265, Screen.height - 52, 260, 45), "Health");  // ( x position, y position, width of box, heighth of box)
	}
}
