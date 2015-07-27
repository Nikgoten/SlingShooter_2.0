using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Button startText;
	public Button exitText;

	// Use this for initialization
	void Start () {
	
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();

	}

	// Update is called once per frame
	public void StartLevel () {
	
		Application.LoadLevel (1);

	}

	public void ExitGame () {

		Application.Quit ();

	}
}
