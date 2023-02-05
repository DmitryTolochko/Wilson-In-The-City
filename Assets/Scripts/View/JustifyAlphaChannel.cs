using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustifyAlphaChannel : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public float ChangeSpeed;

    private bool isIncreasing;
    private bool timeElapsed;

    private float transparency;
    private float initialTransparency;

    private void Start() 
    {
        initialTransparency = Sprite.color.a;
        isIncreasing = true;
        timeElapsed = true;
        transparency = Sprite.color.a * 255;
    }

    private void Update() 
    {
        if (SceneryEngine.IsGameReseted)
        {
            Sprite.color = new Color(1f, 1f, 1f, initialTransparency);
            isIncreasing = true;
            timeElapsed = true;
            transparency = Sprite.color.a * 255;
        }
        else if (timeElapsed)
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
