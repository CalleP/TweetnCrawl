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

    public static HashtagGrid Grid;

    public List<HashTagSet> PopularHashtags;

    public static List<string> Words = new List<string>() { "wonderful", "serenity", "has", "taken", "possession",     "entire", "soul", "like", "sweet", "mornings",   "spring", "which", "enjoy",     "whole", "heart",   "alone",   "feel", "charm",   "existence", "in", "this", "spot", "which", "was", "created", "for",  "bliss",   "souls", "like", "mine",     "happy",   "dear", "friend",   "absorbed", "in",  "exquisite", "sense",   "mere", "tranquil", "existence", "that", "neglect",   "talents", "should",   "incapable",   "drawing", "single", "stroke",     "present", "moment",   "yet", "feel", "that", "never", "was", "greater", "artist", "than", "now", "When", "while",   "lovely", "valley", "teems",   "vapour", "around",       "meridian", "sun", "strikes",   "upper", "surface",     "impenetrable", "foliage",     "trees",   "but", "few", "stray", "gleams", "steal", "into",   "inner", "sanctuary", "throw", "myself", "down", "among",   "tall", "grass",     "trickling", "stream",     "lie", "close",     "earth", "thousand", "unknown", "plants", "are", "noticed",     "when", "hear",   "buzz",     "little", "world", "among",   "stalks",   "grow", "familiar",     "countless", "indescribable", "forms",     "insects",   "flies", "then", "feel",   "presence",     "Almighty", "who", "formed", "us", "in", "his", "own", "image",     "breath",   "that", "universal", "love", "which", "bears",   "sustains", "us",   "it", "floats", "around", "us", "in", "an", "eternity",   "bliss",   "then",   "friend", "when", "darkness", "overspreads",   "eyes",   "heaven",   "earth", "seem",   "dwell", "in",   "soul",   "absorb", "its", "power", "like",   "form",   "beloved", "mistress", "then", "often", "think",   "longing", "Oh", "would", "could", "describe", "these", "conceptions", "could", "impress", "upon", "paper", "all", "that", "is", "living",   "full",   "warm", "within",   "that", "it", "might",     "mirror",     "soul",     "soul", "is",   "mirror",     "infinite", "God",   "friend", "but", "it", "is", "too", "much", "for",   "strength", "sink", "under",   "weight",     "splendour",   "these", "visions", "wonderful", "serenity", "has", "taken", "possession",     "entire", "soul", "like", "these", "sweet", "mornings",   "spring", "which", "enjoy",     "whole", "heart",   "alone",   "feel",   "charm",   "existence", "in", "this", "spot", "which", "was", "created", "for",   "bliss",   "souls", "like", "mine",     "happy",   "dear", "friend",   "absorbed", "in",   "exquisite", "sense",   "mere", "tranquil", "existence", "that", "neglect",   "talents", "should",   "incapable",   "drawing", "single", "stroke",     "present", "moment",   "yet", "feel", "that", "never", "was", "greater", "artist", "than", "now", "When", "while",   "lovely", "valley", "teems",   "vapour", "around",       "meridian", "sun", "strikes",   "upper", "surface",     "impenetrable", "foliage",     "trees",   "but", "few", "stray", "gleams", "steal", "into",   "inner", "sanctuary", "throw", "myself", "down", "among",   "tall", "grass",     "trickling", "stream",     "lie", "close",     "earth", "thousand", "unknown", "plants", "are", "noticed",     "when", "hear",   "buzz",     "little", "world", "among",   "stalks",   "grow", "familiar",     "countless", "indescribable", "forms",     "insects",   "flies", "then", "feel",   "presence",     "Almighty", "who", "formed", "us", "in", "his", "own", "image",     "breath",   "that", "universal", "love", "which", "bears",   "sustains", "us",   "it", "floats", "around", "us", "in", "an", "eternity",   "bliss",   "then",   "friend", "when", "darkness", "overspreads",   "eyes",   "heaven",   "earth", "seem",   "dwell", "in",   "soul",   "absorb", "its", "power", "like",   "form",   "beloved", "mistress", "then", "often", "think",   "longing", "Oh", "would", "could", "describe", "these", "conceptions", "could", "impress", "upon", "paper", "all", "that", "is", "living",   "full",   "warm", "within",   "that", "it", "might",     "mirror",     "soul",     "soul", "is",   "mirror",     "infinite", "God",   "friend", "but", "it", "is", "too", "much", "for",   "strength", "sink", "under",   "weight",     "splendour",   "these", "visions", "wonderful", "serenity", "has", "taken", "possession",     "entire", "soul", "like", "these", "sweet", "mornings",   "spring", "which", "enjoy",     "whole", "heart",   "alone",   "feel",   "charm",   "existence", "in", "this", "spot", "which", "was", "created", "for",   "bliss",   "souls", "like", "mine",     "happy",   "dear", "friend",   "absorbed", "in",   "exquisite", "sense",   "mere", "tranquil", "existence", "that", "neglect",   "talents", "should",   "incapable",   "drawing", "single", "stroke",     "present", "moment",   "yet", "feel", "that", "never", "was", "greater", "artist", "than", "now", "When", "while",   "lovely", "valley", "teems",   "vapour", "around",       "meridian", "sun", "strikes",   "upper", "surface",     "impenetrable", "foliage",     "trees",   "but", "few", "stray", "gleams", "steal", "into",   "inner", "sanctuary", "throw", "myself", "down", "among",   "tall", "grass",     "trickling", "stream",     "lie", "close",     "earth", "thousand", "unknown", "plants", "are", "noticed",     "when", "hear",   "buzz",     "little", "world", "among",   "stalks",   "grow", "familiar",     "countless", "indescribable", "forms",     "insects",   "flies", "then", "feel",   "presence",     "Almighty", "who", "formed", "us", "in", "his", "own", "image",     "breath",   "that", "universal", "love", "which", "bears",   "sustains", "us",   "it", "floats", "around", "us", "in", "an", "eternity",   "bliss",   "then",   "friend", "when", "darkness", "overspreads",   "eyes",   "heaven",   "earth", "seem",   "dwell", "in",   "soul",   "absorb", "its", "power", "like",   "form",   "beloved", "mistress", "then", "often", "think",   "longing", "Oh", "would", "could", "describe", "these", "conceptions", "could", "impress", "upon", "paper", "all", "that", "is", "living",   "full",   "warm", "within",   "that", "it", "might",     "mirror",     "soul",     "soul", "is",   "mirror",     "infinite", "God",   "friend", "but", "it", "is", "too", "much", "for",   "strength", "sink", "under",   "weight",     "splendour",   "these", "visionsA", "wonderful", "serenity", "has", "taken", "possession",     "entire", "soul", "like", "these", "sweet", "mornings",   "spring", "which", "enjoy",     "whole", "heart",   "alone",   "feel",   "charm",   "existence", "in", "this", "spot", "which", "was", "created", "for",   "bliss",   "souls", "Dorcas", "Severian", "Terminus", "Balanders", "Aquila", "Autarch", "Hipparch", "Venice" };

    void Start()
    {
        //var connect = new ServerConnector();

        //connect.Connect();


        

        List<HashTagSet> hashTagSet = new List<HashTagSet>();

        for (int i = 0; i < 10; i++)
        {
            for (int y = 0; y < 10; y++)
			{
                hashTagSet.Add(new HashTagSet(Words[Random.Range(0, Words.Count)], Random.Range(100,1000)));
			}
            
        }

        PopularHashtags = hashTagSet;

        Grid = new HashtagGrid(hashTagSet, 10);


        //connect.Close();

        

    }

	// Use this for initialization
	void OnGUI () {
		GUI.skin.label.font = f;
		GUI.skin.button.font = f;
		GUI.Label(new Rect(Screen.width / 3 + 30, Screen.height/4, 500, 50), "Select one of these popular #Hashtags!");
	
		if (GUI.Button (new Rect (Screen.width / 3 + 100, Screen.height/3, 300, 50), "#"+PopularHashtags[0].Value)) {
	
			Hashtag = PopularHashtags[0].Value;
            Grid.MoveToHashtag(PopularHashtags[0].Value);
			print(Hashtag);
			StartLevel();


		}
        if (GUI.Button(new Rect(Screen.width / 3 +100, Screen.height / 3 + 50, 300, 50), "#" + PopularHashtags[1].Value))
        {

			Hashtag = PopularHashtags[1].Value;
            Grid.MoveToHashtag(PopularHashtags[1].Value);
			print(Hashtag);
			StartLevel();
		
		}
        if (GUI.Button(new Rect(Screen.width / 3 + 100, Screen.height / 3 + 100, 300, 50), "#" + PopularHashtags[2].Value))
        {

			Hashtag = PopularHashtags[2].Value;
            Grid.MoveToHashtag(PopularHashtags[2].Value);
			print(Hashtag);
			StartLevel();
		
		
		}
        if (GUI.Button(new Rect(Screen.width / 3 + 100, Screen.height / 3 + 150, 300, 50), "#" + PopularHashtags[3].Value))
        {

			Hashtag = PopularHashtags[3].Value;
            Grid.MoveToHashtag(PopularHashtags[3].Value);
			print(Hashtag);
			StartLevel();

	

		}
        if (GUI.Button(new Rect(Screen.width / 3 + 100, Screen.height / 3 + 200, 300, 50), "#" + PopularHashtags[4].Value))
        {

			Hashtag = PopularHashtags[4].Value;
            Grid.MoveToHashtag(PopularHashtags[4].Value);
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
