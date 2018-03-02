using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPower : MonoBehaviour, IPointerEnterHandler{


    PlayerControl m_owner;
    Image m_ownImage;
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_owner.UsePickUp();

    }




    // Use this for initialization
    void Start () {
        m_owner = GetComponentInParent<PlayerControl>();
        m_ownImage = GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        m_ownImage.color = m_owner.GetColorPower();

    }
}
