using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour 
{
	public Texture2D Image;
	public float x;
	public float y;
	float speed = 20f; 
	bool crawling = false;
	float time;
	public GameObject credits;

	
	void OnGUI()
	{
		AutoResize(1920, 1080);
		if (GUI.Button (new Rect (x, y, Image.width, Image.height), Image)) {
			crawling = true;
			print("Credits pressed");
			
		}
		
		
	}
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}


	void Start()
	{
		time = Time.deltaTime;
		credits = GameObject.Find("CreditsObject");
		}

	 void Update ()
	{
		if (crawling == true) {

			//credits.transform.Translate(Vector3.forward * Time.deltaTime);
			credits.transform.Translate(Vector3.forward * Time.deltaTime, Camera.main.transform);

						if (credits.transform.position.z > 70) {
								crawling = false;
				credits.transform.position = new Vector3(-0.6568451f, -4.86139f, -7.337857f);

						}
				}
	}
}