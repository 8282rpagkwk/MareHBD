using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainUI : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private Button[] btns;
    [SerializeField] private RectTransform vfx;

    private void Start()
    {
        if (!GameManager.isFirst) rect.anchoredPosition = new Vector2(-1920, 0);
        vfx.gameObject.SetActive(true);

        for (int i = 0; i<=GameManager.clearStage; i++)
        {
            if (i == 3)
            {
                vfx.gameObject.SetActive(false);
                break;
            }
            else
            {
                btns[i].interactable = true;
                vfx.anchoredPosition = btns[i].gameObject.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, -234f);
            }
        }
    }

    public void ToStageSelect()
    {
        rect.DOLocalMoveX(-1920, 0.3f).SetEase(Ease.OutExpo);
    }

    public void ToMain()
    {
        rect.DOLocalMoveX(0, 0.3f).SetEase(Ease.OutExpo);
    }

    public void startStage(int i)
    {
        GameManager.stage = i;
        btns[i - 1].enabled = false;
        btns[i - 1].transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.4f).SetEase(Ease.OutQuad).OnComplete(() => {
            btns[i - 1].enabled = true;
            SceneManager.LoadSceneAsync("GameScene");
        });
    }
}
