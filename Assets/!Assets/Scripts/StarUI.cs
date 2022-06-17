using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUI : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject[] btns;

    public void ShowStar(int i)
    {
        stars[i].SetActive(true);
    }

    public GameObject[] getStars()
    {
        return stars;
    }

    public void showEndingBtn()
    {
        btns[0].SetActive(false);
        btns[1].SetActive(true);
    }
}
