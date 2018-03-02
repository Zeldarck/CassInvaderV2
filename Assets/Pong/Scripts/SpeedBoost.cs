using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PickUp {


    public override Color GetColorPower()
    {
        return new Color(1, 0, 0, 0.9f);
    }

    public override PICKUPTYPE GetPickUpType()
    {
        return PICKUPTYPE.BOOST;
    }

    public override void PlayerUse()
    {
        GameManager.BoostBall(1f);
        Destroy(gameObject);
    }

}
