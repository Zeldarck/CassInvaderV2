using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MENUTYPE {NOTHING, MAIN, END, GAME, PAUSE};


[System.Serializable]
public class MenuEntry
{
    public MENUTYPE m_type;
    public Menu m_menu;
}
public class MenuManager : MonoBehaviour {
    [SerializeField]
     List<MenuEntry> m_listMenu;
     static MenuManager INSTANCE;
    MENUTYPE m_currentMenu = MENUTYPE.NOTHING;
    [SerializeField]
    MENUTYPE m_startMenu;


    // Use this for initialization
    void Start () {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
        }
        else
        {
            INSTANCE = this;
            foreach(MenuEntry entry in m_listMenu)
            {
                entry.m_menu.gameObject.SetActive(false);
            }
            OpenMenu(m_startMenu);
        }
    }

    // Update is called once per frame
    void Update () {

        MenuEntry menuEntry = INSTANCE.m_listMenu.Find(x => x.m_type == INSTANCE.m_currentMenu);
        if (menuEntry != null)
        {
            GetComponent<Image>().enabled = menuEntry.m_menu.DisplayBack();
        }

    }

    public static void OpenMenu(MENUTYPE a_type)
    {
        CloseMenu();
        MenuEntry menuEntry = INSTANCE.m_listMenu.Find(x => x.m_type == a_type);
        if (menuEntry != null)
        {
            menuEntry.m_menu.gameObject.SetActive(true);
            INSTANCE.m_currentMenu = a_type;
        }

    }


    public static void CloseMenu()
    {
        MenuEntry menuEntry = INSTANCE.m_listMenu.Find(x => x.m_type == INSTANCE.m_currentMenu);
        if(menuEntry != null)
        {
            menuEntry.m_menu.gameObject.SetActive(false);
            INSTANCE.m_currentMenu = MENUTYPE.NOTHING;
        }
    }


}
