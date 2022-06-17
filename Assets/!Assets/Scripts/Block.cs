using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody[] characters;

    Belt belt;

    [SerializeField] private int type;
    [SerializeField] private float hitPower;
    void Start()
    {
        belt = GetComponentInParent<Belt>();
        Init();
    }

    void LateUpdate()
    {
        if(transform.position.z == -3)
        {
            transform.position = new Vector3(0, 0, belt.getBlockCount() * belt.getBlockSize() - 3);
            //transform.Translate(0, 0, belt.getBlockCount() * belt.getBlockSize());
            Init();
        }
    }

    void Init()
    {
        type = Random.Range(0, 3);

        for(int i=0; i<characters.Length; i++)
        {
            characters[i].gameObject.SetActive(type == i);
        }
        StartCoroutine(InitPhysics());
    }

    public bool check(int selectedType)
    {
        bool result = type == selectedType;
        if (result)
            StartCoroutine(hit());

        return result;
    }

    IEnumerator InitPhysics()
    {
        characters[type].isKinematic = true;
        yield return new WaitForFixedUpdate();

        characters[type].velocity = Vector3.zero;
        characters[type].angularVelocity = Vector3.zero;
        yield return new WaitForFixedUpdate();

        characters[type].transform.localPosition = Vector3.zero;
        characters[type].transform.rotation = Quaternion.identity;

    }

    IEnumerator hit()
    {
        characters[type].isKinematic = false;
        yield return new WaitForFixedUpdate();

        switch (type)
        {
            case 0:
                characters[type].AddForce((Vector3.left * hitPower) * hitPower, ForceMode.Impulse);
                break;
            case 1:
                characters[type].AddForce((Vector3.back * hitPower) * hitPower, ForceMode.Impulse);
                break;
            case 2:
                characters[type].AddForce((Vector3.right * hitPower) * hitPower, ForceMode.Impulse);
                break;
        }
    }
}
