using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public float power;
    BoxCollider2D coll;
    Rigidbody2D rig;
    /*public Vector3 respawnVec;
    public float maxRight;

    private void LateUpdate()
    {
        if (transform.position.x > maxRight)
        {
            respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.SetParent(collision.gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.SetParent(null);
    }

    public void respawn()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;

        transform.position = respawnVec;
    }*/

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();

        rig.AddForce(Vector2.right * (power/2), ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            coll.isTrigger = true;
            rig.velocity = Vector2.zero;
            rig.AddForce((Vector2.down * power) * power, ForceMode2D.Impulse);
        }
    }
}
