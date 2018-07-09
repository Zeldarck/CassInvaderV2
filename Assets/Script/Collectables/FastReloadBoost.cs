using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastReloadBoost : Collectable
{



    public override void PlayerUse()
    {
        m_playerController.AddNbMaxBall(2);
        m_playerController.SetReloadSpeed(50f);

        base.PlayerUse();

    }

    protected override void CleanBoost()
    {
        m_playerController.AddNbMaxBall(-2);
        m_playerController.ResetReloadSpeed();

        base.CleanBoost();
    }

    public override Color GetColorPower()
    {
        return Color.blue;
    }
}