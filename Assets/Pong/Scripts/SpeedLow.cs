using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLow : PickUp {


    public override Color GetColorPower()
    {
        return new Color(0, 0, 1, 0.9f);
    }

    public override PICKUPTYPE GetPickUpType()
    {
        return PICKUPTYPE.LOW;
    }

    public override void PlayerUse()
    {
        GameManager.BoostBall(-2.5f);
        Destroy(gameObject);
    }

}
