using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HashtagChoice : MonoBehaviour{

	public GameObject PlayReference;
	public GameObject CreditsReference;
	public GameObject ExitReference;
	public GameObject HowToPlayReference;
	public GameObject HowToPlayBoxReference;
	public GameObject MainMenuPanelReference;
	public GameObject SelfReference;
	public Font f;
	public string Hashtag;

    public List<HashTagSet> PopularHashtags;
    void Start()
    {
        var connect = new ServerConnector();

        connect.Connect();


        PopularHashtags = connect.ParseTopList(connect.Send("GetTopList"));

        connect.Close();

        

    }

	// Use this for initialization
	void OnGUI () {
		GUI.skin.label.font = f;
		GUI.skin.button.font = f;
		GUI.Label(new Rect(Screen.width / 3 + 30, Screen.height/4, 500, 50), "Select one of these popular #Hashtags!");
	
		if (GUI.Button (new Rect (Screen.width / 3 + 100, Screen.height/3, 300, 50), "#"+PopularHashtags[0].Value)) {
	
			Hashtag = PopularHashtags[0].Value;
			print(Hashtag);
			StartLevel();


		}
        if (GUI.Button(new Rect(Screen.width / 3 +100, Screen.height / 3 + 50, 300, 50), "#" + PopularHashtags[1].Value))
        {

			Hashtag = PopularHashtags[1].Value;
			print(Hashtag);
			StartLevel();
		
		}
        if (GUI.Button(new Rect(Screen.width / 3 + 100, Screen.height / 3 + 100, 300, 50), "#" + PopularHashtags[2].Value))
        {

			Hashtag = PopularHashtags[2].Value;
			print(Hashtag);
			StartLevel();
		
		
		}
        if (GUI.Button(new Rect(Screen.width / 3 + 100, Screen.height / 3 + 150, 300, 50), "#" + PopularHashtags[3].Value))
        {

			Hashtag = PopularHashtags[3].Value;
			print(Hashtag);
			StartLevel();

	

		}
        if (GUI.Button(new Rect(Screen.width / 3 + 100, Screen.height / 3 + 200, 300, 50), "#" + PopularHashtags[4].Value))
        {

			Hashtag = PopularHashtags[4].Value;
			print(Hashtag);
			StartLevel();

		
		}


		if (GUI.Button (new Rect (Screen.width / 3 + 100, Screen.height/3 + 270, 300, 50), "Cancel")) {
		
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
