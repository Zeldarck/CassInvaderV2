﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : Menu {

    public void OnBackButton()
    {
        MenuManager.INSTANCE.BackToMainMenu();
    }
}
