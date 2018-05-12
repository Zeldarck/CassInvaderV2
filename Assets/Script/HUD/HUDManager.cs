using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HUDManager : Singleton<HUDManager> {


    [SerializeField]
    Text m_scoreText;

    void Start()
    {
    }
    // Set Display Time
	public void SetScore(int a_score){
        m_scoreText.text = a_score +"";
	}
}
