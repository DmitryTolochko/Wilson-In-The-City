using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustifyAlphaChannel : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public float ChangeSpeed;

    private bool isIncreasing = true;
    private bool timeElapsed = true;

    private float transparency;

    private void Start() 
    {
        transparency = Sprite.color.a * 255;
    }

    private void Update() 
    {
        if (timeElapsed)
        {
            if (transparency < 255 && isIncreasing)
            {
                transparency += 1;
                Sprite.color = new Color(1f, 1f, 1f, transparency/255);
            }
            else if (transparency > 0 && !isIncreasing)
            {
                transparency -= 1;
                Sprite.color = new Color(1f, 1f, 1f, transparency/255);
            }

            if (transparency == 255)
                isIncreasing = false;
            if (transparency == 0)
                isIncreasing = true;

            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        timeElapsed = false;
        yield return new WaitForSeconds(ChangeSpeed/60);
        timeElapsed = true;
    }
}
