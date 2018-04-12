using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackGround : MonoBehaviour {

    static public MenuBackGround INSTANCE;


    // Use this for initialization
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

    public void SetAlpha(float a_alpha)
    {
        Color temp = GetComponent<Image>().color;
        temp.a = a_alpha;
        GetComponent<Image>().color = temp;
    }


}
