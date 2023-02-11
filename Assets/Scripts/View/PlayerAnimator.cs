using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator Instance;

    public GameObject AnimatorObject;
    private SpriteRenderer Renderer;

    public RuntimeAnimatorController WilsonController;
    public RuntimeAnimatorController RichardController;
    public RuntimeAnimatorController MarvinController;

    [HideInInspector]
    public Animator Animator;
    private new Collider2D collider;
    private float offsetY;

    private void Start() 
    {
        Animator = transform.Find("Animator").GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        Renderer = AnimatorObject.GetComponent<SpriteRenderer>();
        Instance = this;

        ChangeSkin(GameModel.SkinType);
    }

    private void Update() 
    {
        if (Mathf.Abs(transform.localPosition.y - (-2.745185f)) > 0.05f)
            Animator.SetBool("IsJumping", true);
        else 
            Animator.SetBool("IsJumping", false);

        if (Time.timeScale > 0
        && (Renderer.sprite.name.Contains("_0") || Renderer.sprite.name.Contains("_18")) 
        && Animator.GetBool("IsJumping") == false
        && Animator.GetBool("IsDrifting") == false)
            SoundPlayer.Instance.PlayStepSound();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.name != "Background")
        {
            Animator.SetBool("IsDrifting", true);
            AnimatorObject.transform.position = new Vector2
            (
                AnimatorObject.transform.position.x,
                AnimatorObject.transform.position.y + offsetY
            );
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.transform.name == "Background")
            return;
        
        Animator.SetBool("IsDrifting", false);
        AnimatorObject.transform.position = new Vector2
        (
            AnimatorObject.transform.position.x,
            AnimatorObject.transform.position.y - offsetY
        );
    }

    public static void ChangeSkin(SkinType type)
    {
        Instance.AnimatorObject.transform.localPosition = new Vector2(-0.14f, 1.33f);
        if (GameModel.OpenedCharacters.Contains(type))
        {
            switch (type)
            {
                case SkinType.Wilson:
                    Instance.Animator.runtimeAnimatorController = Instance.WilsonController;
                    GameModel.SkinType = SkinType.Wilson;
                    Instance.offsetY = 0;
                    break;
                case SkinType.Richard:
                    Instance.Animator.runtimeAnimatorController = Instance.RichardController;
                    GameModel.SkinType = SkinType.Richard;
                    Instance.offsetY = -0.7f;
                    break;
                case SkinType.Marvin:
                    Instance.Animator.runtimeAnimatorController = Instance.MarvinController;
                    GameModel.SkinType = SkinType.Marvin;
                    Instance.offsetY = -0.2f;
                    break;
            }
        }

        GameModel.SaveToFile();
    }
}
