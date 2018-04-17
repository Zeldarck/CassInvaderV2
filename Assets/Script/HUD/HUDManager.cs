using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HUDManager : MonoBehaviour {

    public static HUDManager INSTANCE;

    [SerializeField]
    Text m_scoreText;

    void Start()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
        }
        else
        {
            INSTANCE = this;
        }
    }
    // Set Display Time
	public void SetScore(int a_score){
        m_scoreText.text = a_score +"";
	}
}
