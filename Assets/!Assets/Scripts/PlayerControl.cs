using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private AudioClip sound_throw;
    [SerializeField] private AudioClip sound_punch;
    [SerializeField] private KeyCode[] keys;
    [SerializeField] private Sprite[] playerSprites;
    [SerializeField] private GameObject[] keyImages;
    [SerializeField] private Gift gift;
    [SerializeField] private Image img;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(CheckPressedKey());
        KeyReset();
        img.gameObject.GetComponent<RectTransform>()
            .DOLocalMoveY(20, 0.5f)
            .SetEase(Ease.InCubic)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gift")
        {
            gift = collision.gameObject.GetComponent<Gift>();
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gift" || onGift)
        {
            onGift = true;
            Gift gift = collision.gameObject.GetComponent<Gift>();
            for(int i=0; i<keys.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    select(gift, i);
                }
            }
        }
    }*/



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gift")
        {
            gift = null;
        }
    }

    public void select(Gift gift, int pressedKey)
    {
        if (gift.check(pressedKey))//Á¤´ä
        {
            img.gameObject.GetComponent<RectTransform>().DOKill();
            if (pressedKey == 4)
            {
                StartCoroutine(SpriteChange(3));
                img.gameObject.GetComponent<RectTransform>()
                    .DOLocalMoveY(-20, 5 / GameManager.speed / 10)
                    .SetEase(Ease.OutQuad)
                    .SetLoops(2, LoopType.Yoyo);
                audioSource.PlayOneShot(sound_punch);
            }
            else
            {
                StartCoroutine(SpriteChange(2));
                img.gameObject.GetComponent<RectTransform>()
                    .DOLocalMoveX(50, 5 / GameManager.speed / 10)
                    .SetEase(Ease.OutQuad)
                    .SetLoops(2, LoopType.Yoyo);
                audioSource.PlayOneShot(sound_throw);
            }
            gift.setSuccess();
            GameManager.Success();
        }
        else
        {
            GameManager.Miss();
        }
    }

    IEnumerator CheckPressedKey()
    {
        int keyLength;
        while (!GameManager.isGameOver)
        {
            if (GameManager.stage == 4)
            {
                keyLength = keys.Length;
            }
            else
            {
                keyLength = (keys.Length + GameManager.stage) - 3;
            }

            for (int i = 0; i < keyLength; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    KeyReset();
                    keyImages[i].gameObject.SetActive(true);
                    if (gift != null)
                    {
                        select(gift, i);
                    }
                }
            }
            yield return null;
        }
    }

    void KeyReset()
    {
        foreach (GameObject obj in keyImages)
            obj.SetActive(false);
    }

    IEnumerator SpriteChange(int index)
    {
        img.sprite = playerSprites[index];
        yield return new WaitForSecondsRealtime(5 / GameManager.speed / 10);
        img.sprite = playerSprites[1];
    }
}
