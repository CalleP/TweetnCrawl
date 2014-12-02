using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	void OnGUI() {
		AutoResize(1920, 1080);

		if (GameEnd == true) {

			Panel.GetComponent<ScaleGUI>().enabled = true;
			Time.timeScale = 0;


			if (GUI.Button (new Rect (x1, y1, Restart.width, Restart.height), Restart)) {
				print("Restart pressed");
				audio.PlayOneShot(Onclick);
				Panel.GetComponent<ScaleGUI>().enabled = false;
				Time.timeScale = 1;
				GameEnd = false;
				Application.LoadLevel(1);
			}	
			
			
			if (GUI.Button (new Rect (x2, y2, MainMenu.width, MainMenu.height), MainMenu)) {
				
				print("MainMenu pressed");
				audio.PlayOneShot(Onclick);
				Panel.GetComponent<ScaleGUI>().enabled = false;
				Time.timeScale = 1;
				GameEnd = false;
				Application.LoadLevel(0);
			}
			
		}
		
	}


	public AudioClip[] sounds;
	private bool GameEnd = false;
	public float x1 = 320;
	public float y1 = 410;
	public float x2 = 320;
	public float y2 = 480;
	public AudioClip Onclick;
	public Texture2D Restart;
	public Texture2D MainMenu;

	public GameObject Panel;



	void FixedUpdate() {



		if(GameObject.Find("Player").GetComponent<CharacterHealth>().health <= 0) {
			gameover();

		}

	                    }

	public void gameover() {
		StartCoroutine(RandomSound());
		GameEnd = true;
		}


	public IEnumerator RandomSound() {

		audio.PlayOneShot(sounds [Random.Range (0, sounds.Length)]);
		yield return null;

		}

	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
}
