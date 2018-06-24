using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBallBoost : Collectable
{



    public override void PlayerUse()
    {
        m_playerController.AddNbMaxBall(1);
        m_playerController.SetReloadTime(0f);

        base.PlayerUse();

    }


    protected override void CleanBoost()
    {
        m_playerController.AddNbMaxBall(-1);
        m_playerController.SetReloadTime(20f);
        base.CleanBoost();
    }
    public override Color GetColorPower()
    {
        return Color.green;
    }
}