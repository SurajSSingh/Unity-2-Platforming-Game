using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotion : Collectable
{

    public int value;

    private playerManager PlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        collectableName = "Red Potion";
        description = "Replenish " + value.ToString() + " Health.";
        PlayerManager = GameObject.Find("Player").GetComponent<playerManager>();
    }

    public override void Use()
    {
        PlayerManager.ChangeHealth(value);
    }

}
