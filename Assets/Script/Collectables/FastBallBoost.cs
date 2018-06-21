using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBallBoost : Collectable
{

    [SerializeField]
    private float m_boostSpeed = 1f;

    [SerializeField]
    private int m_boostStrength = 1;

    private float m_usedTime;
    private bool m_used = false;


    public override void PlayerUse()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<BallController>().BoostSpeed(m_boostSpeed,BoostDuration);
            ball.GetComponent<BallController>().BoostStrength(m_boostStrength, BoostDuration);
        }

        m_used = true;
        m_usedTime = Time.time;
        base.PlayerUse();
    }

    private void Update()
    {
        if (m_used && Time.time > m_usedTime + BoostDuration)
        {
            m_playerController.SetActiveBoost(false);
            DestroyUsedBoost();
        }
    }
    public override Color GetColorPower()
    {
        return Color.blue;
    }

}
