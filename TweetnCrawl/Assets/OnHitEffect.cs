using UnityEngine;
using System.Collections;

public class OnHitEffect : MonoBehaviour {

    public bool HitEnemy = false;
    public float PauseTime = 0.02f;
	// Use this for initialization
	void Start () {
        if (HitEnemy)
        {
            StartCoroutine(ImpactPause());
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator ImpactPause()
    {
        if (PauseTime == 0)
        {
            yield return null;
        }
        Time.timeScale = 0;
        float pauseEndTime = Time.realtimeSinceStartup + PauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;

    
    }
}
