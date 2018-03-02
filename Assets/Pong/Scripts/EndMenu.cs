using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : Menu {

    [SerializeField]
    Text m_playerResult1;
    [SerializeField]
     Text m_playerResult2;


    private void OnEnable()
    {
        string win = "WIN !";
        string lose = "NOOB...";
        m_playerResult1.text = GameManager.m_lastWinner == PLAYER.PLAYER_1 ? win : lose;
        m_playerResult2.text = GameManager.m_lastWinner == PLAYER.PLAYER_2 ? win : lose;
    }

}


