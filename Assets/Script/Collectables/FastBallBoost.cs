using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBallBoost : Collectable
{

    [SerializeField]
    private float m_boostDuration = 6f;

    [SerializeField]
    private float m_boostSpeed = 1.5f;

    private float m_usedTime;
    private bool m_used = false;


    public override void PlayerUse()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<BallController>().Boost(m_boostSpeed,m_boostDuration);
        }

        m_used = true;
        m_usedTime = Time.time;
    }

    private void Update()
    {
        if (m_used && Time.time > m_usedTime + m_boostDuration)
        {
            DestroyUsedBoost();
        }
    }
    public override Color GetColorPower()
    {
        return Color.blue;
    }
}
