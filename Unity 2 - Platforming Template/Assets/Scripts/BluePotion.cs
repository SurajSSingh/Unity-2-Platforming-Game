using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePotion : Collectable
{

    public int value;

    private playerManager PlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        collectableName = "Blue Potion";
        description = "Gain " + value.ToString() + " Jump Force.";
        PlayerManager = GameObject.Find("Player").GetComponent<playerManager>();
    }

    public override void Use()
    {
        player.GetComponent<NinjaController.NinjaController>().PhysicsParams.jumpUpForce += value;
        
    }
}
