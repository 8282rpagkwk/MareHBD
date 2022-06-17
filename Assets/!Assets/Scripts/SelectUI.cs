using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        StartCoroutine(GetKeyPress());
    }

    IEnumerator GetKeyPress()
    {
        while (!GameManager.isGameOver)
        {
            if (Input.GetKeyDown(key))
            {
                btn.onClick.Invoke();
            }
            yield return null;
        }
    }
}
