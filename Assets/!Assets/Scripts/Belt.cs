using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField] private int blockCount;
    [SerializeField] private float blockSize;
    [SerializeField] private float beltSpeed;
    [SerializeField] private int nowblock = 0;
    //[SerializeField] private float beltMoveOffset;

    [SerializeField] private GameObject block;

    [SerializeField] private Block[] blocks;
    [SerializeField] private List<GameObject> blockObjs;

    public int getBlockCount()
    {
        return blockCount;
    }

    public float getBlockSize()
    {
        return blockSize;
    }

    private void Awake()
    {
        for (int i = 0; i < blockCount; i++)
        {
            //Debug.Log(i);
            blockObjs.Add(Instantiate(block, this.gameObject.transform));
        }
    }
    void Start()
    {
        Application.targetFrameRate = 60;

        blocks = GetComponentsInChildren<Block>();
        Align();
    }

    void Align()
    {
        if(blockCount == 0)
        {
            Debug.Log("블록이 없습니다.");
            return;
        }

        blockSize = blocks[0].GetComponentInChildren<BoxCollider>().transform.localScale.z;

        for(int i=0; i<blockCount; i++)
        {
            blocks[i].transform.Translate(0, 0, i * blockSize);
        }
    }

    IEnumerator Move()
    {
        int nextZ = (int)(transform.position.z - blockSize);
        while (transform.position.z > nextZ)
        {
            transform.Translate(0, 0, Time.deltaTime * beltSpeed * -1);
            yield return null;
        }

        transform.position = Vector3.forward * nextZ;
        nowblock = (nowblock + 1) % blockCount;
    }

    public void select(int selectedType)
    {
        if (blocks[nowblock].check(selectedType))//정답
        {
            GameManager.Success();
            StartCoroutine(Move());
        }
        else
        {                                        //오답
            GameManager.Fail();
        }
    }
}
