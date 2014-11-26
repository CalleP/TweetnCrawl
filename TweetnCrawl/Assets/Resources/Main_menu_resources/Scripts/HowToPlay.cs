using UnityEngine;
using System.Collections;

public class HowToPlay : MonoBehaviour 
{   
	public Texture2D Image;
	public float x;
	public float y;
	public GameObject HowTo;
	public AudioClip Onhover;
	public AudioClip Onclick;

	void OnGUI()
	{
		AutoResize(1920, 1080);
		if (GUI.Button (new Rect (x, y, Image.width, Image.height), Image)) {

			print("HowToPlay pressed");
			HowTo.GetComponent<ScaleHowToPlay>().enabled = true;
			print("done");
			audio.PlayOneShot(Onclick);
		}	
		
	}
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}