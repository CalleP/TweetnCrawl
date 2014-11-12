﻿using UnityEngine;

public class ScaleGUI : MonoBehaviour 
{
	public Texture2D Image;
	public float x;
	public float y;
	
	void OnGUI()
	{
		AutoResize(1920, 1080);
		
		GUI.DrawTexture(new Rect(x, y, Image.width, Image.height), Image);
	}
	
	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}