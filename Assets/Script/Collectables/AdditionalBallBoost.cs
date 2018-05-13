using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBallBoost : Collectable
{

    [SerializeField]
    private float m_boostDuration = 3f;

    private float m_usedTime;
    private bool m_used = false;


    public override void PlayerUse()
    {
        m_playerController.AddNbMaxBall(1);
        m_playerController.SetReloadTime(0f);

        m_used = true;
        m_usedTime = Time.time;
    }

    private void Update()
    {
        if (m_used && Time.time > m_usedTime + m_boostDuration)
        {
            m_playerController.AddNbMaxBall(-1);
            m_playerController.SetReloadTime(30f);
            m_playerController.SetActiveBoost(false);
            DestroyUsedBoost();
        }
    }
    public override Color GetColorPower()
    {
        return Color.green;
    }
}