using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int health = 100;
    public int score = 0;
    public List<Collectable> inventory = new List<Collectable>();

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        score = 0;
        if (GameObject.Find("PlayerInfo") != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
