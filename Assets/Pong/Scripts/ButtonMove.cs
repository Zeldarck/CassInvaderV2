using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMove : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler{

    [HideInInspector]
    public bool m_isPointerIn = false;
     public void OnPointerEnter(PointerEventData eventData)
     {
         m_isPointerIn = true;
     }

     public void OnPointerExit(PointerEventData eventData)
     {
         m_isPointerIn = false;
     }

}
