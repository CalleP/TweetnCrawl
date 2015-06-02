using UnityEngine;
using System.Collections;

public class PlayGame : MonoBehaviour
{	
	public Texture2D Image;
	public float x;
	public float y;

	WWW www; 
	public GUITexture guitext;
	public GUITexture loading;
	public AudioClip Error;
	public AudioClip ModemConnect;
	public AudioClip Onhover;
	public AudioClip Onclick;
	public GameObject HashtagWindow;
	public string Hashtag;
	public bool Timeout = false;
	public int time;



	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
		}
	
	void OnGUI()
	{
		AutoResize(1920, 1080);
		if (GUI.Button (new Rect (x, y, Image.width, Image.height), Image)) {

			print("Play game pressed");
			StopAllCoroutines();
			
            //Online mode disabled
            //StartCoroutine(checkConnection());
            audio.PlayOneShot(Onclick);
            HashtagWindow.GetComponent<HashtagChoice>().enabled = true;
            HashtagWindow.GetComponent<HashtagChoice>().disableGUI();
				}
	

	}
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

	IEnumerator checkConnection()
	{

        //www = new WWW("www.google.com");
        //yield return www;
        guitext.GetComponent<MessageScaling>().enabled = false;
        Ping pinger = new Ping("195.178.179.176"); //Pings the server

        while (!pinger.isDone)
        {  //Checks if ping is not done
            yield return 0;

            print("Still pinging server!");

            time = time + 1;

            if (time > 20)
            {
                break;
                Timeout = true;
            }
        }
        yield return pinger;
        int ping = pinger.time;
        print(ping);

        if (Timeout == true || ping == -1) //If ping fails or has too high latency then show fail message
        {
            guitext.GetComponent<MessageScaling>().enabled = true;
            print("faild to connect to internet, trying after 2 seconds.");
            audio.PlayOneShot(Error);
            yield return new WaitForSeconds(2);// trying again after 2 sec
            guitext.GetComponent<MessageScaling>().enabled = false;
            StartCoroutine(checkConnection());
        }
        else
        {
            print("connected to internet");
            audio.PlayOneShot(Onclick);
            HashtagWindow.GetComponent<HashtagChoice>().enabled = true;
            HashtagWindow.GetComponent<HashtagChoice>().disableGUI();

        }
		
	}

	public IEnumerator LoadLevel() {

		loading.GetComponent<MessageScaling>().enabled = true;
		audio.PlayOneShot(ModemConnect);	
		SaveHashtag ();
		yield return new WaitForSeconds(5);
		audio.Stop ();
		Application.LoadLevel("debugscene");

	}

	public void SaveHashtag() {

		GameObject go = GameObject.Find ("HashtagWindow");
		HashtagChoice Choicereference = go.GetComponent <HashtagChoice> ();
		string tag = Choicereference.Hashtag;
		print (tag);
		Hashtag = "#" + tag;

		print (Hashtag);

	}
}

