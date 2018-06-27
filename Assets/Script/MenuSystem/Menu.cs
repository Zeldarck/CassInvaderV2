using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

#if NETWORK
public class Menu : NetworkBehaviour
#else
public abstract class Menu : MonoBehaviour
#endif
{

    [SerializeField]
    protected Button m_backButton;

    protected virtual void Awake()
    {
        SetUpBackButtonListenner();
    }


    protected virtual void Start()
    {
    }

    public void CloseMenu()
	{
		MenuManager.INSTANCE.CloseMenu();
	}

    public virtual void OnOpen()
    {
        SetUpBackButtonListenner();
        Time.timeScale = 0;
    }

    public virtual void OnClose()
    {
        Time.timeScale = 1;
    }

#if NETWORK
    [ClientRpc]
    protected void RpcCloseMenu()
    {
        CloseMenu();
    }
#endif

    public virtual float GetAlphaBack()
    {
        return 1.0f;
    }

    protected void SetUpBackButtonListenner()
    {
        if (m_backButton)
        {
            m_backButton.onClick.RemoveAllListeners();
            m_backButton.onClick.AddListener(() =>
            {
                MenuManager.INSTANCE.BackToMainMenu();
            });
        }
    }

    public void SetBackButtonAsClose()
    {
        if (m_backButton)
        {
            m_backButton.onClick.RemoveAllListeners();
            m_backButton.onClick.AddListener(() =>
            {
                MenuManager.INSTANCE.CloseMenu();
            });
        }
    }


}