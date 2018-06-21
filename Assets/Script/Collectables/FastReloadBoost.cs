using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastReloadBoost : Collectable
{

    private float m_usedTime;
    private bool m_used = false;


    public override void PlayerUse()
    {
        m_playerController.SetNbMaxBall(4);
        m_playerController.SetReloadTime(1f);

        m_used = true;
        m_usedTime = Time.time;
        base.PlayerUse();

    }

    private void Update()
    {
        if (m_used && Time.time > m_usedTime + BoostDuration)
        {
            m_playerController.SetNbMaxBall(1);
            m_playerController.SetReloadTime(30f);

            m_playerController.SetActiveBoost(false);
            DestroyUsedBoost();
        }
    }
    public override Color GetColorPower()
    {
        return Color.red;
    }
}