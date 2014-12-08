using UnityEngine;
using System.Collections;

public class BulletFade : MonoBehaviour
{



    public float FadeTime = 5f;

    void Start()
    {
        StartCoroutine(Fade());
    }

    public IEnumerator Fade()
    {
        var portion = FadeTime / 5;
        yield return new WaitForSeconds(portion*4);
        gameObject.renderer.enabled = false;
        yield return new WaitForSeconds(portion / 2);
        gameObject.renderer.enabled = true;
        yield return new WaitForSeconds(portion / 1.5f);
        gameObject.renderer.enabled = false;
        yield return new WaitForSeconds(portion / 1);
        gameObject.renderer.enabled = true;
        yield return new WaitForSeconds(portion / 0.5f);

        Destroy(gameObject);
        yield return null;
    }

}

