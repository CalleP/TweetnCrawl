using UnityEngine;
using System.Collections;

public class HashtagChoice : MonoBehaviour{

	public GameObject PlayReference;
	public GameObject CreditsReference;
	public GameObject ExitReference;
	public GameObject HowToPlayReference;
	public GameObject HowToPlayBoxReference;
	public GameObject MainMenuPanelReference;
	public GameObject SelfReference;
	public Font f;


	// Use this for initialization
	void OnGUI () {
		GUI.skin.label.font = f;
		GUI.skin.button.font = f;
		GUI.Label(new Rect(Screen.width / 4 + 20, Screen.height/4, 500, 50), "Select one of these popular #Hashtags!");
	
		if (GUI.Button (new Rect (Screen.width / 3 + 50, Screen.height/3, 200, 50), "#Hashtag1")) {
			print("Choice1");
			StartLevel();


		}
		if (GUI.Button (new Rect (Screen.width / 3 + 50, Screen.height/3 + 50, 200, 50), "#Hashtag2")) {
			print("Choice2");
			StartLevel();
		
		}
		if (GUI.Button (new Rect (Screen.width / 3 + 50, Screen.height/3 + 100, 200, 50), "#Hashtag3")) {
			print("Choice3");
			StartLevel();
		
		
		}
		if (GUI.Button (new Rect (Screen.width / 3 + 50, Screen.height/3 + 150, 200, 50), "#Hashtag4")) {
			print("Choice4");
			StartLevel();

	

		}
		if (GUI.Button (new Rect (Screen.width / 3 + 50, Screen.height/3 + 200, 200, 50), "#Hashtag5")) {
			print("Choice5");
			StartLevel();

		
		}


		if (GUI.Button (new Rect (Screen.width / 3 + 50, Screen.height/3 + 270, 200, 50), "Cancel")) {
		
			enableGUI();
			disableself();
			
		}
	}



	public void StartLevel() {

		var play = PlayReference.GetComponent<PlayGame> ().LoadLevel ();
		disableGUI ();
		disableself ();
		StartCoroutine (play);
	}

	public void disableGUI() {
		PlayReference.GetComponent<PlayGame>().enabled = false;
		CreditsReference.GetComponent<Credits>().enabled = false;
		ExitReference.GetComponent<Exit>().enabled = false;
		HowToPlayReference.GetComponent<HowToPlay>().enabled = false;
		HowToPlayBoxReference.GetComponent<ScaleHowToPlay>().enabled = false;
		MainMenuPanelReference.GetComponent<ScaleGUI>().enabled = false;


		}

	public void disableself() {

		SelfReference.GetComponent<HashtagChoice>().enabled = false;

	}

	public void enableGUI() {
		PlayReference.GetComponent<PlayGame>().enabled = true;
		CreditsReference.GetComponent<Credits>().enabled = true;
		ExitReference.GetComponent<Exit>().enabled = true;
		HowToPlayReference.GetComponent<HowToPlay>().enabled = true;
		MainMenuPanelReference.GetComponent<ScaleGUI>().enabled = true;
		
		
	}

	

}
