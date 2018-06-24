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


    public override void PlayerUse()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {

            ball.GetComponent<BallController>().AddSpeed(m_boostSpeed);
            ball.GetComponent<BallController>().AddStrength(m_boostStrength);
        }
        base.PlayerUse();
    }

    protected override void CleanBoost()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {

            ball.GetComponent<BallController>().AddSpeed(-m_boostSpeed);
            ball.GetComponent<BallController>().AddStrength(-m_boostStrength);
        }

        base.CleanBoost();
    }

    public override Color GetColorPower()
    {
        return Color.red;
    }

}
