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
    Button m_backButton;

    protected virtual void Start()
    {
        if (m_backButton)
        {
            m_backButton.onClick.AddListener(() =>
            {
                MenuManager.INSTANCE.BackToMainMenu();
            });
        }
    }

    public void CloseMenu()
	{
		MenuManager.INSTANCE.CloseMenu();
	}

    public virtual void OnOpen()
    {

    }

    public virtual void OnClose()
    {

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


}