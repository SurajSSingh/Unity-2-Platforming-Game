using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public int score = 0;
    public int health = 0;
    public List<Collectable> inventory;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
