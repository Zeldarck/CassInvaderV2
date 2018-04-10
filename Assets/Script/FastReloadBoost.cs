using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastReloadBoost : Collectable {
	

    public override void PlayerUse()
    {




        m_playerController.SetActiveBoost(false);
        m_playerController.SetBoost(null);
        m_playerController = null;
        Destroy(gameObject);
    }


    public override Color GetColorPower()
    {
        return Color.red;
    }
}
