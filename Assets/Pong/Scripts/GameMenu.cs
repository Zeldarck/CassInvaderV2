using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu {

    [SerializeField]
    List<Text> m_playerScoreText1;
    [SerializeField]
    List<Text> m_playerScoreText2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DisplayScore();

    }

    void DisplayScore()
    {
        foreach (Text text in m_playerScoreText1)
        {
            text.text = GameManager.m_playerScore1 + "";
        }
        foreach (Text text in m_playerScoreText2)
        {
            text.text = GameManager.m_playerScore2 + "";
        }
    }

    public void Pause(){
        GameManager.m_paused.Invoke();
    }

    public override bool DisplayBack()
    {
        return false;
    }
}
