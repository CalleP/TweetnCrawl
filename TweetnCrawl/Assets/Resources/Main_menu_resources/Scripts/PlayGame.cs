﻿using UnityEngine;
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
	protected string Hashtag;
	
	void OnGUI()
	{
		AutoResize(1920, 1080);
		if (GUI.Button (new Rect (x, y, Image.width, Image.height), Image)) {

			print("Play game pressed");
			www = new WWW("www.google.com");
			StartCoroutine(checkConnection());

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
			audio.PlayOneShot(Onclick);
			HashtagWindow.GetComponent<HashtagChoice>().enabled = true;
			HashtagWindow.GetComponent<HashtagChoice>().disableGUI();
			
		}
		
	}

	public IEnumerator LoadLevel() {

		loading.GetComponent<MessageScaling>().enabled = true;
		audio.PlayOneShot(ModemConnect);
		yield return new WaitForSeconds(5);
		Application.LoadLevel("debugscene");

	}
}

