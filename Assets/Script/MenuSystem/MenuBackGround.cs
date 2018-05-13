﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackGround : Singleton<MenuBackGround> {



    // Use this for initialization
    void Start()
    {
    }

    public void SetAlpha(float a_alpha)
    {
        Image back = GetComponent<Image>();
        back.enabled = true;

        Color temp = back.color;
        temp.a = a_alpha;
        back.color = temp;
    }

    public void Disable()
    {
        GetComponent<Image>().enabled = false;
    }

}
