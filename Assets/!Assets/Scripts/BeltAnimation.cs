using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeltAnimation : MonoBehaviour
{
    private Image image;
    [SerializeField] private float railSpeed;
    [SerializeField] private Sprite[] anims;
    [SerializeField] private int animIndex;
    [SerializeField] private float divOffset = 7.0f;


    void Start()
    {
        image = GetComponent<Image>();
        railSpeed = GameManager.speed;
        GetComponentInParent<SurfaceEffector2D>().speed = railSpeed;
        animIndex = 0;

        StartCoroutine(SpriteChange());
    }

    IEnumerator SpriteChange()
    {
        while (!GameManager.isGameOver)
        {
            railSpeed = GameManager.speed;
            GetComponentInParent<SurfaceEffector2D>().speed = railSpeed;
            image.sprite = anims[animIndex];
            yield return new WaitForSecondsRealtime(RailTransitionSpeed());

            if(animIndex == anims.Length - 1)
                animIndex = 0;
            else
                animIndex++;

        }
    }

    private float RailTransitionSpeed()
    {
        return divOffset / railSpeed / 10;
    }
}
