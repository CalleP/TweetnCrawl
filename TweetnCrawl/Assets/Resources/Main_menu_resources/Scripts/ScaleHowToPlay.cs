using UnityEngine;

public class ScaleHowToPlay : MonoBehaviour 
{
	public Texture2D Image;
	public Texture2D CloseImage;
	public float x;
	public float y;
	public float x2;
	public float y2;
	public GameObject HowTo;


	void OnGUI()
	{
		AutoResize(1920, 1080);
		if (GUI.Button (new Rect (x2, y2, CloseImage.width, CloseImage.height), CloseImage)) {

			print ("closing");
			HowTo.GetComponent<ScaleHowToPlay>().enabled = false;
		}

		GUI.DrawTexture(new Rect(x, y, Image.width - 50, Image.height - 50), Image);


	}
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}