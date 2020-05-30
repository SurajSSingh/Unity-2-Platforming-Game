using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

    public int value;

    private playerManager PlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        collectableName = "coin";
        description = "Increase score by " + value.ToString();
        PlayerManager = GameObject.Find("Player").GetComponent<playerManager>();
    }

    public override void Use()
    {
        PlayerManager.ChangeScore(value);
    }

}
