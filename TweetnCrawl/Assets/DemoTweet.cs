using UnityEngine;
using SimpleJSON;
using System.Collections;

public class DemoTweet : MonoBehaviour {
	
	// Use this for initialization
    public static string Hash;
	void Start () {
		var N = JSON.Parse(ServerConnector.TweetData);


        Hash = N["statuses"].ToString();// text will be a string containing "sub object"
        //Debug.Log("k1" + hashtag);
		//string name = N["screen_name"];
		//Debug.Log ("k1"+N["statuses"]["text"]);
		//ServerConnector.Hashtags.Add(hashtag["text"]);
		//ServerConnector.TwitterNAmes = name;
		
	}
	float timeStamp = 0f;
	float coolDown = 3000f;
	
	System.Random rand = new System.Random();
	// Update is called once per frame
	void Update () {
		guiText.text = Hash;
		
		
		if (timeStamp <= Time.time)
		{
			
			//var text = ServerConnector.Hashtags[rand.Next(0,ServerConnector.Hashtags.Count-1)];
			//Debug.Log(text);
			//guiText.text = text;
			//timeStamp = Time.time + coolDown;
			
		}
	}
	
}