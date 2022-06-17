using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //private bool isInGift = false;

    /*private void Start()
    {
        StartCoroutine(CheckGifts());
    }

    IEnumerator CheckGifts()
    {
        while (!GameManager.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {

            }
            else if (Input.GetKeyDown(KeyCode.D))
            {

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {

            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {

            }
            yield return null;
        }
    }*/

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        isInGift = true;
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Gift")
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                select(obj.GetComponent<Gift>(), 0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                select(obj.GetComponent<Gift>(), 1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                select(obj.GetComponent<Gift>(), 2);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                select(obj.GetComponent<Gift>(), 3);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                select(obj.GetComponent<Gift>(), 4);
            }
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        isInGift = false;
    }*/

    public void select(Gift gift, int selectedType)
    {
        gift.check(selectedType);
        /*if (gift.check(selectedType))//정답
        {
            //GameManager.Success();

        }
        else
        {                                        //오답
            //GameManager.Fail();
        }*/
    }
}
