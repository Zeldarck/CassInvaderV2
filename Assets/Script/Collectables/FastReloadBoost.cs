using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastReloadBoost : Collectable
{

    [SerializeField]
    private float m_boostDuration = 8f;

    private float m_usedTime;
    private bool m_used = false;


    public override void PlayerUse()
    {
        m_playerController.SetNbMaxBall(5);
        m_playerController.SetReloadTime(1f);

        m_used = true;
        m_usedTime = Time.time;
    }

    private void Update()
    {
        if (m_used && Time.time > m_usedTime + m_boostDuration)
        {
            m_playerController.SetNbMaxBall(1);
            m_playerController.SetReloadTime(30f);
            DestroyUsedBoost();
        }
    }
    public override Color GetColorPower()
    {
        return Color.red;
    }
}