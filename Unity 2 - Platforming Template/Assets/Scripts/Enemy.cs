using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hurtAmount = -10;
    public float speed = 5;
    public float TravelDistance = 10;
    public bool movingRight;

    private float targetLoc;

    public void Start()
    {
        if (movingRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            targetLoc = transform.position.x + TravelDistance;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
            targetLoc = transform.position.x - TravelDistance;
        }
    }

    public void FixedUpdate()
    {
        if (movingRight)
        {
            if (transform.position.x > targetLoc)
            {
                movingRight = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed,0);
                targetLoc = targetLoc - TravelDistance;
            }
        }
        else
        {
            if (transform.position.x < targetLoc)
            {
                movingRight = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed,0);
                targetLoc = targetLoc + TravelDistance;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerManager>()
                                .ChangeHealth(hurtAmount);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerManager>()
                                .ChangeHealth(hurtAmount/10);
        }
    }
}
