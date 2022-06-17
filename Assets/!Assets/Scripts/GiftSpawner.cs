using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> giftPool = new List<GameObject>();
    [SerializeField] private GameObject gift;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private SurfaceEffector2D mainbelt;
    [SerializeField] private int giftCount;
    [SerializeField] private float spawnMaxInterval;
    [SerializeField] private float spawnMinInterval;
    [SerializeField] private RectTransform[] beltPos;

    private void Awake()
    {
        for (int i = 0; i < giftCount; i++)
        {
            giftPool.Add(CreateObj(gift, transform));
        }
    }

    private void Start()
    {
        StartCoroutine(CreateGift());
    }

    public RectTransform getBeltPostion(int key)
    {
        return beltPos[key];
    }

    IEnumerator CreateGift()
    {
        while (!GameManager.isGameOver)
        {
            float interval = Random.Range(spawnMinInterval, spawnMaxInterval);
            //Debug.Log($"Set Interval : {interval}");
            giftPool[DisableGift()].SetActive(true);
            yield return new WaitForSecondsRealtime(interval);
        }
        mainbelt.enabled = false;
    }

    int DisableGift()
    {
        List<int> num = new List<int>();
        for (int i = 0; i < giftPool.Count; i++)
        {
            if (!giftPool[i].activeSelf)
            {
                num.Add(i);
            }
        }
        int x = 0;
        if (num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x;
    }

    private GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj, parent);
        copy.SetActive(false);
        return copy;
    }

    public void ShowFailScreen()
    {
        StartCoroutine(ScreenTransition());
    }

    IEnumerator ScreenTransition()
    {
        failScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(0.2f);
        failScreen.SetActive(false);
    }
}
