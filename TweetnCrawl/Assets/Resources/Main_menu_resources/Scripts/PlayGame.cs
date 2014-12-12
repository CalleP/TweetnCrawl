using UnityEngine;
using System.Collections;

public class PlayGame : MonoBehaviour 
{
	public Texture2D Image;
	public float x;
	public float y;
	public float x2;
	public float y2;
	WWW www; 
	public GUITexture guitext;
	public GUITexture loading;
	public AudioClip Error;
	public AudioClip ModemConnect;
	public AudioClip Onhover;
	public AudioClip Onclick;
	private bool Connection = false;
	protected string Hashtag;
	
	void OnGUI()
	{
		AutoResize(1920, 1080);
		if (GUI.Button (new Rect (x, y, Image.width, Image.height), Image)) {

			print("Play game pressed");
			www = new WWW("www.google.com");
			StartCoroutine(checkConnection());

				}


		if (Connection = true) {
			//TODO move this code to HashtagChoice script instead and use enable/disable script
			if (GUI.Button (new Rect (x2, y2, 500, 50), "Hashtag1")) {
				print("Choice1");
				Hashtag = "1";
				StartCoroutine(LoadLevel());
				Connection = false;
			}
			if (GUI.Button (new Rect (x2, y2 + 50, 500, 50), "Hashtag2")) {
				print("Choice2");
				Hashtag = "2";
				StartCoroutine(LoadLevel());
				Connection = false;
			}
			if (GUI.Button (new Rect (x2, y2 + 100, 500, 50), "Hashtag3")) {
				print("Choice3");
				Hashtag = "3";
				StartCoroutine(LoadLevel());
				Connection = false;
			}
			if (GUI.Button (new Rect (x2, y2 + 150, 500, 50), "Hashtag4")) {
				print("Choice4");
				Hashtag = "4";
				StartCoroutine(LoadLevel());
				Connection = false;
			}
			if (GUI.Button (new Rect (x2, y2 + 200, 500, 50), "Hashtag5")) {
				print("Choice5");
				Hashtag = "5";
				StartCoroutine(LoadLevel());
				Connection = false;
			}

				}

	}
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

	IEnumerator checkConnection()
	{
		www = new WWW("www.google.com");
		yield return www;
		guitext.GetComponent<MessageScaling>().enabled = false;

		if(www.error != null)
		{
			guitext.GetComponent<MessageScaling>().enabled = true;
			print("faild to connect to internet, trying after 2 seconds.");
			audio.PlayOneShot(Error);
			yield return new WaitForSeconds(2);// trying again after 2 sec
			guitext.GetComponent<MessageScaling>().enabled = false;
			StartCoroutine(checkConnection());
		}else
		{
			print("connected to internet");
			Connection = true;
			
		}
		
	}

	public IEnumerator LoadLevel() {

		loading.GetComponent<MessageScaling>().enabled = true;
		audio.PlayOneShot(ModemConnect);
		yield return new WaitForSeconds(5);
		Application.LoadLevel("debugscene");

	}
}

