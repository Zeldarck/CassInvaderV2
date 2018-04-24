using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Collectable : MonoBehaviour
{

    private bool m_notPicked = true;
    protected PlayerController m_playerController = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collider = collision.GetComponent<Collider2D>();
        if (collider.CompareTag("Player"))
        {
            m_notPicked = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            m_playerController = collider.GetComponent<PlayerController>();
            if (m_playerController.IsBoostActive() || m_playerController == null)
            {
                Destroy(gameObject);
            }
            else
            {
                m_playerController.SetBoost(this);
                m_playerController.SetActiveBoost(true);
            }
        }
    }

    private void OnBecameInvisible()
    {
        if (m_notPicked)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Action when player use it
    /// </summary>
    public abstract void PlayerUse();

    /// <summary>
    /// Color to display on ButtonPower
    /// </summary>
    public abstract Color GetColorPower();

    public void DestroyUsedBoost()
    {
        m_playerController.SetActiveBoost(false);
        m_playerController.SetBoost(null);
        m_playerController = null;

        Destroy(gameObject);
    }
}