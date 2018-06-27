using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HUDManager : Singleton<HUDManager> {


    [SerializeField]
    Text m_scoreText;

    [SerializeField]
    Button m_pauseButton;


    void Start()
    {
        m_pauseButton.onClick.AddListener(() =>
        {
            Menu pause = MenuManager.INSTANCE.OpenMenu(MENUTYPE.OPTION);
            pause.GetComponent<OptionMenu>().SetBackButtonAsClose();
        });
    }
    // Set Display Time
    public void SetScore(int a_score){
        m_scoreText.text = a_score +"";
	}
}
