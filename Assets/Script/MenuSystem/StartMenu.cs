using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : Menu {


    public void OnPlayButton()
    {
        GameManager.INSTANCE.StartGame();
    }
}
