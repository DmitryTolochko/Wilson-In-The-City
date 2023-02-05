using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryEngine : MonoBehaviour
{
    public static bool IsGameReseted;

    public GameObject FirstObject;
    public GameObject SecondObject;
    public float Speed;
    public LayerTypes LayerType;

    private SpriteRenderer FirstSprite;
    private SpriteRenderer SecondSprite;

    private Vector2 FirstInit;
    private Vector2 SecondInit;

    private Sprite FirstInitSprite;
    private Sprite SecondInitSprite;

    private void Start() 
    {
        FirstSprite = FirstObject.GetComponent<SpriteRenderer>();
        SecondSprite = SecondObject.GetComponent<SpriteRenderer>();

        FirstInitSprite = FirstSprite.sprite;
        SecondInitSprite = SecondSprite.sprite;

        FirstInit = FirstObject.transform.position;
        SecondInit = SecondObject.transform.position;
    }

    private void Update()
    {
        if (Time.deltaTime != 0)
        {
            if (IsGameReseted)
            {
                ResetSprites();
                IsGameReseted = false;
            }
            else if (SecondObject.transform.position.x <= 0f)
            {
                FirstObject.transform.position = FirstInit;
                SecondObject.transform.position = SecondInit;

                SwapSprites();
            }
            else
            {
                FirstObject.transform.position = Vector2.MoveTowards(FirstObject.transform.position, 
                    new Vector3(-100f, FirstObject.transform.position.y, 0f), Time.deltaTime * Speed);
                SecondObject.transform.position = Vector2.MoveTowards(SecondObject.transform.position, 
                    new Vector3(0f, SecondObject.transform.position.y, 0f), Time.deltaTime * Speed);
            }
        }
    }

    private void SwapSprites()
    {
        FirstSprite.sprite = SecondSprite.sprite;
        SecondSprite.sprite = LayerSpriteAssets.GetRandomSpriteOfType(LayerType);
    }

    private void ResetSprites()
    {
        FirstSprite.sprite = FirstInitSprite;
        SecondSprite.sprite = SecondInitSprite;

        FirstObject.transform.position = FirstInit;
        SecondObject.transform.position = SecondInit;
    }
}
