using UnityEngine;
using System.Collections;

public class ScaleHowToPlay : MonoBehaviour 
{
	public Texture2D Image;
	public Texture2D CloseImage;
	public Texture2D Instructions;
	public float x;
	public float y;
	public float x2;
	public float y2;
	public float x3;
	public float y3;
	public float x4;
	public float y4;
	public GameObject HowTo;
	public string stringToEdit;
	public GUIStyle style;
	public Vector2 scrollPosition = Vector2.zero;
	public AudioClip Onhover;
	public AudioClip Onclick;


	void OnGUI()
	{
		AutoResize(1920, 1080);
		if (GUI.Button (new Rect (x2, y2, CloseImage.width, CloseImage.height), CloseImage)) {

			print ("closing");
			audio.PlayOneShot(Onclick);
			HowTo.GetComponent<ScaleHowToPlay>().enabled = false;
		}

		GUI.DrawTexture(new Rect(x, y, Image.width - 50, Image.height - 50), Image);
		scrollPosition = GUI.BeginScrollView(new Rect(x3, y3, CloseImage.width + 345, CloseImage.height + 350), scrollPosition, new Rect(x3, y3, CloseImage.width  + 345 * 2, CloseImage.height  + 350 * 2));
		GUI.Label(new Rect(x3,y3,CloseImage.width + 345, CloseImage.height + 350), stringToEdit, style);
		GUI.DrawTexture(new Rect(x4, y4, Instructions.width - 50, Instructions.height - 50), Instructions);
		GUI.EndScrollView ();

	}
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}