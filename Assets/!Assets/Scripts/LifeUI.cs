using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private Image hp;
    //[SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        hp.fillAmount = 1.0f;
        //text.text = $"{GameManager.hp} / {GameManager.hp / 100f}";
        StartCoroutine(checkHp());
    }

    IEnumerator checkHp()
    {
        while (!GameManager.isGameOver)
        {
            //text.text = $"{GameManager.hp} / {GameManager.hp / 100f}";
            hp.fillAmount = GameManager.hp / 100f;
            yield return null;
        }
    }
}
