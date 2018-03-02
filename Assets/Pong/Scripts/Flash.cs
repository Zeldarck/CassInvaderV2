using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {

    SpriteRenderer m_spriteRenderer;
    List<FlashGroup> m_observers = new List<FlashGroup>();
    Color m_baseColor;
	// Use this for initialization
	void Start () {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_baseColor = m_spriteRenderer.color;
    }
	

    public void addObserver(FlashGroup a_flashGroup)
    {
        m_observers.Add(a_flashGroup);
    }

    public void changeColor(Color a_color)
    {
        m_spriteRenderer.color = a_color;
        StartCoroutine(BackColor());
    }

    IEnumerator BackColor()
    {
        yield return new WaitForSeconds(0.5f);
        m_spriteRenderer.color = m_baseColor;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(FlashGroup group in m_observers)
        {
            group.Notify();
        }
    }
}
