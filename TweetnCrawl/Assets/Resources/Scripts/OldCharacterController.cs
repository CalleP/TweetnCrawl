using UnityEngine;
using System.Collections;

public class OldCharacterController : MonoBehaviour {

	void OnGUI() {
		AutoResize(1920, 1080);

		if (Escape == true) {
			Panel.GetComponent<ScaleGUI>().enabled = true;
			Time.timeScale = 0;
			if (GUI.Button (new Rect (x1, y1, Resume.width, Resume.height), Resume)) {
				
				print("Resume pressed");
				Time.timeScale = 1;
				print("done");
				audio.PlayOneShot(Onclick);
				Panel.GetComponent<ScaleGUI>().enabled = false;
				Escape = false;
			}	
			if (GUI.Button (new Rect (x2, y2, HowToPlay.width, HowToPlay.height), HowToPlay)) {
				
				print("HowToPlay pressed");
				HowTo.GetComponent<ScaleHowToPlay>().enabled = true;
				print("done");
				audio.PlayOneShot(Onclick);
			}	
			

			if (GUI.Button (new Rect (x3, y3, MainMenu.width, MainMenu.height), MainMenu)) {
				
				print("MainMenu pressed");
				audio.PlayOneShot(Onclick);
				Panel.GetComponent<ScaleGUI>().enabled = false;
				DestroyAllGameObjects();
				Time.timeScale = 1;
				Escape = false;
			
				Application.LoadLevel(0);
			}
			
		}

	}
	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();


	}
	public Texture2D Resume;
	public Texture2D HowToPlay;
	public Texture2D MainMenu;
    public Sprite IdleState1;
    public Sprite IdleState2;
    public Sprite HurtState;
    public Sprite DeathState;
    private bool state = true;
    private SpriteRenderer sr;
	private bool Escape = false;
	public GameObject HowTo;
	public GameObject Panel;


    private int health;

    public float idleInterval = 0.5f;
    private float time;
	public float x1;
	public float y1;
	public float x2;
	public float y2;
	public float x3;
	public float y3;
	public AudioClip Onclick;

    void Update()
    {
        health = GetComponent<CharacterHealth>().health;
        if (health > 0)
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        rigidbody2D.isKinematic = true;
        rigidbody2D.isKinematic = false;
    }
	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation = Quaternion.identity;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0.32f, transform.position.z), 1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);

        }
        if (Input.GetKey(KeyCode.S))
        {
             transform.position = Vector3.MoveTowards(transform.position, transform.position+new Vector3(0, -0.32f, transform.position.z), 1f);
             transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-0.32f, 0, transform.position.z), 1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0.32f, 0, transform.position.z), 1f);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
		if (Input.GetKey(KeyCode.Escape))
		{

			Escape = true;

		}

        if (Time.time >= time)
        {
            if (health <= 0)
            {
                sr.sprite = DeathState;
            }
            if (state == true)
            {
                state = false;
                sr.sprite = IdleState1;

            }
            else
            {
                state = true;
                sr.sprite = IdleState2;
            }
            time = Time.time + idleInterval;
        }






	}

	public static void AutoResize(int screenWidth, int screenHeight)
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}


	public void DestroyAllGameObjects()
	{
		GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
		
		for (int i = 0; i < GameObjects.Length; i++)
		{
		
			Destroy(GameObjects[i]);

		}

		Transform[] Transforms = (FindObjectsOfType<Transform>() as Transform[]);
		
		for (int i = 0; i < Transforms.Length; i++)
		{
			
			Destroy(Transforms[i]);
			
		}

		Collider[] Colliders = (FindObjectsOfType<Collider>() as Collider[]);
		
		for (int i = 0; i < Colliders.Length; i++)
		{
			
			Destroy(Colliders[i]);

		}

		Collider2D[] Colliders2d = (FindObjectsOfType<Collider2D>() as Collider2D[]);
		
		for (int i = 0; i < Colliders2d.Length; i++)
		{
			
			Destroy(Colliders2d[i]);
			
		}

		Texture2D[] Textures2d = (FindObjectsOfType<Texture2D>() as Texture2D[]);
		
		for (int i = 0; i < Textures2d.Length; i++)
		{
			
			Destroy(Textures2d[i]);
			
		}


	}

}



