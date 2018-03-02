using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PICKUPTYPE { BOOST,LOW };

public abstract class PickUp : MonoBehaviour{

    int m_direction;
    [SerializeField]
     float m_speed;
    private bool m_notPick = true;
    
    private void Start()
    {
        m_direction = GameManager.m_lastHit == PLAYER.PLAYER_1 ? -1 : 1;
    }
    private void FixedUpdate()
    {
        if (m_notPick)
        {
            transform.Translate(0, m_speed * m_direction, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collider = collision.GetComponent<Collider2D>();
        if (collider.CompareTag("Player"))
        {
            m_notPick = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            if (!collider.GetComponent<PlayerControl>().SetPickUp(this))
            {
                Destroy(gameObject);
            }
       }
    }

    private void OnBecameInvisible()
    {
        if (m_notPick)
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

    /// <summary>
    /// Category of PickUp for AI
    /// </summary>
    public abstract PICKUPTYPE GetPickUpType();
}
