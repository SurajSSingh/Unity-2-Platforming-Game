using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGoalScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<playerManager>().WinGame();
        }
    }
}
