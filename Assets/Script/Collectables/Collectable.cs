using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Collectable : MonoBehaviour
{
    [SerializeField]
    private float m_boostDuration = 0f;


    [SerializeField]
    private float m_collectSpeed = 2f;

    private bool m_notPicked = true;
    protected PlayerController m_playerController = null;
    protected bool m_used = false;
    protected float m_usedTime;

    protected virtual void Start()
    {
        GetComponent<SpriteRenderer>().color = GetColorPower();
    }

    public float BoostDuration
    {
        get
        {
            return m_boostDuration;
        }
    }

    protected virtual void Update()
    {
        if (m_used && Time.time > m_usedTime + BoostDuration)
        {
            Destroy(gameObject);
        }
    }


    private void FixedUpdate()
    {
        gameObject.transform.position = gameObject.transform.position + Vector3.down* m_collectSpeed * Time.deltaTime;
    }


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
                SoundManager.INSTANCE.StartAudio(AUDIOCLIP_KEY.BONUS_PICKED, MIXER_GROUP_TYPE.SFX_GOOD, false, false, AUDIOSOURCE_KEY.NO_KEY_AUTODESTROY);
                m_playerController.SetBoost(this);
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
    public virtual void PlayerUse()
    {
        m_used = true;
        m_usedTime = Time.time;
        SoundManager.INSTANCE.StartAudio(AUDIOCLIP_KEY.BONUS_USED, MIXER_GROUP_TYPE.SFX_GOOD, false, false, AUDIOSOURCE_KEY.NO_KEY_AUTODESTROY);
    }

    /// <summary>
    /// Color to display on ButtonPower
    /// </summary>
    public abstract Color GetColorPower();

    protected virtual void CleanBoost()
    {
        DestroyUsedBoost();
    }

    public void DestroyUsedBoost()
    {
        if (m_playerController)
        {
            m_playerController.CleanBoost();
        }
        m_playerController = null;
    }

    protected void OnDestroy()
    {
        if (m_used)
        {
            CleanBoost();
        }
    }

}