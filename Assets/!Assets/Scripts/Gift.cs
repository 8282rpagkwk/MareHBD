using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Gift : MonoBehaviour
{
    [SerializeField] private GameObject[] Sprites;
    [SerializeField] private GameObject[] keyImages;
    //[SerializeField] private RectTransform[] beltPos;

    [SerializeField] private int key;
    [SerializeField] private int type;
    [SerializeField] private float power;
    [SerializeField] private bool isSuccess = false;

    [SerializeField] private GiftSpawner spawner;
    private Rigidbody2D rig;
    private BoxCollider2D coll;
    private RectTransform rect;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        rect = GetComponent<RectTransform>();
        spawner = transform.parent.GetComponent<GiftSpawner>();
    }

    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        type = Random.Range(0, Sprites.Length);
        if(GameManager.stage == 4)
        {
            key = Random.Range(0, keyImages.Length);
        }
        else
        {
            key = Random.Range(0, (keyImages.Length + GameManager.stage) - 3);
        }

        for (int i = 0; i < Sprites.Length; i++)
        {
            Sprites[i].gameObject.SetActive(type == i);
        }

        for (int i = 0; i < keyImages.Length; i++)
        {
            keyImages[i].gameObject.SetActive(key == i);
        }


        StartCoroutine(InitPhysics());
    }

    public bool check(int pressKey)
    {
        bool result = key == pressKey;
        if (result)
            hit();

        return result;
    }

    IEnumerator InitPhysics()
    {
        isSuccess = false;
        yield return new WaitForFixedUpdate();

        coll.isTrigger = false;
        yield return new WaitForFixedUpdate();

        rig.velocity = Vector2.zero;
        rig.angularVelocity = 0;
        yield return new WaitForFixedUpdate();

        rect.rotation = Quaternion.identity;
        rect.anchoredPosition = new Vector2(-840, 0);
        yield return new WaitForFixedUpdate();

        rig.AddForce(Vector2.right * (power / 2), ForceMode2D.Impulse);
    }

    void hit()
    {
        foreach (GameObject obj in keyImages)
            obj.SetActive(false);

        if (key == 4)
        {
            coll.isTrigger = true;
            rig.velocity = Vector2.zero;
            rig.AddForce((Vector2.down * power * 2) * power, ForceMode2D.Impulse);
        }
        else
        {
            rect.DOAnchorPosX(spawner.getBeltPostion(key).anchoredPosition.x, 0.3f).SetEase(Ease.InQuad);
            rect.DOAnchorPosY(spawner.getBeltPostion(key).anchoredPosition.y, 0.3f).SetEase(Ease.OutQuad);
        }
    }

    public void setSuccess()
    {
        isSuccess = true;
    }

    public bool getSuccess()
    {
        return isSuccess;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "DeadZone":
                if (!isSuccess)
                {
                    coll.isTrigger = true;
                    rig.velocity = Vector2.zero;
                    rig.AddForce((Vector2.down * power) * power, ForceMode2D.Impulse);
                    spawner.ShowFailScreen();
                    if(gameObject.name != "Sample") GameManager.Fail();
                }
                break;
            case "DisableZone":
                rect.anchoredPosition = new Vector2(-840, 0);
                gameObject.SetActive(false);
                break;
        }
    }
}
