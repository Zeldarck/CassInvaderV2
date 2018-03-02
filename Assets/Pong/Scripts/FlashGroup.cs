using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashGroup : MonoBehaviour {

    [SerializeField]
    List<Flash> m_flashList = new List<Flash>();
        
   void Start () {
		foreach(Flash flash in m_flashList)
        {
            flash.addObserver(this);
        }
	}
	
    public void Notify()
    {
        Color color = new Color();
        color.r = Random.Range(0.0f, 2.0f) < 1 ? 0.0f : 1.0f;
        color.g = Random.Range(0.0f, 2.0f) < 1 ? 0.0f : 1.0f;
        color.b = Random.Range(0.0f, 2.0f) < 1 ? 0.0f : 1.0f;
        color.a = 1.0f;
        foreach (Flash flash in m_flashList)
        {
            flash.changeColor(color);
        }

    }


}
