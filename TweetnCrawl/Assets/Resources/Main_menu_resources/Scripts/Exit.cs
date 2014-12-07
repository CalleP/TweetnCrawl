using UnityEngine;

public class Exit : MonoBehaviour 
{	string hover;
	public Texture2D Image;
	public float x;
	public float y;
	public AudioClip Onhover;
	public AudioClip Onclick;
	Vector2 mouse;
	public Rect rect;

    void Awake()
    {
        Screen.showCursor = true;

    }

	void OnGUI()
	{
		 rect = new Rect (x, y, Image.width, Image.height);
	
		AutoResize(1920, 1080);
		if (GUI.Button (rect, Image)) {
			print("Exit game pressed");
			this.audio.PlayOneShot(Onclick);
			Application.Quit();
		}
	}
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}


	//TODO play sound when mouse is hovering this code is unfinished
	void start() {


		rect = new Rect (x, y, Image.width, Image.height);
		}

	void Update()
	{
		rect = new Rect (x, y, Image.width, Image.height);
		mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		
		if(rect.Contains(mouse))
		{
			this.audio.PlayOneShot(Onhover);
		}
	}
}

