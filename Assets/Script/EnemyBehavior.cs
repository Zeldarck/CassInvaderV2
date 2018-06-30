using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyBehavior : Ennemies
{

    protected float m_localMovementSpeed = 0;
    protected float m_enemyRadius = 1;
    protected int m_life = 1;
    protected int m_enemyLevel = 1;
    protected int m_frameCount = 0;

    public UnityEvent OnDie;


    // GameObject Setup
    //------------------------------------------------------------------------------------
    /// <summary>
    /// Start an OnDie Listener which whill trigger when an enemy shall be destroyed
    /// </summary>
    override protected void Start()
    {
        _INIT_POS_X = this.transform.position.x;
        _INIT_POS_Y = this.transform.position.y;

        _X_MOVE_EAST = 2; // ( this.transform.parent.childCount)/2;
        _X_MOVE_WEST = 2; // (this.transform.parent.childCount)/2;


        OnDie.AddListener(() => { GameManager.INSTANCE.AddPoint(m_enemyLevel); });
        OnDie.AddListener(() => StartCoroutine(AutoDestroy()));
        OnDie.AddListener(() => { CollectableManager.INSTANCE.EnemyDestroyed(m_enemyLevel, gameObject.transform.position); });
        OnDie.AddListener(() => { SoundManager.INSTANCE.StartAudio(AUDIOCLIP_KEY.ENEMY_DIE, MIXER_GROUP_TYPE.SFX_BAD, false, false, AUDIOSOURCE_KEY.NO_KEY_AUTODESTROY); });

    }


    // Damage undertaking
    //------------------------------------------------------------------------------------

    /// <summary>
    /// On trigger get damages from any damage source
    /// </summary>
    public virtual bool GetDamage(int a_damage)
    {
        m_life -= a_damage;
        if (m_life <= 0)
        {
            GetComponent<ParticleSystem>().time = 0;
            GetComponent<ParticleSystem>().Emit(10);
            OnDie.Invoke();
            return true;
        }
        SoundManager.INSTANCE.StartAudio(AUDIOCLIP_KEY.ENNEMY_HITTED, MIXER_GROUP_TYPE.SFX_GOOD, false, false, AUDIOSOURCE_KEY.NO_KEY_AUTODESTROY);
        return false;
    }

    /// <summary>
    /// Self Destruction on trigger and wait two frames in order to let the ball bounce on the collider
    /// Without this part of code, the ball would go through the enemy and not bounce
    /// </summary>
    protected IEnumerator AutoDestroy(float a_delay = 0.1f)
    {
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(a_delay);

        Destroy(gameObject);
    }



    // Movement behavior
    //------------------------------------------------------------------------------------

    /// <summary>
    /// Compute the direction vector of the enemy depending of the current situation
    /// Here a default null vector is computed as this function shall be overriden by each kind of enemy
    /// </summary>
    override protected Vector2 DirectionComputation()
    {
        return new Vector2(0, 0);
    }

    /// <summary>
    /// Update the position of the enemy
    /// </summary>
    override protected void FixedUpdate()
    {
        // check if an invader has made it to the Dead Zone
        if (gameObject.transform.position.y < (_DEADZONE + m_enemyRadius))
        {           
            GameManager.INSTANCE.EndGame();
        }

        // move accordingly 
        Vector2 direction = DirectionComputation();
        gameObject.transform.position = (Vector2)gameObject.transform.position + direction * Time.deltaTime;
    }

    protected virtual void LaunchProjectile()
    {
        SoundManager.INSTANCE.StartAudio(AUDIOCLIP_KEY.ENEMY_FIRE, MIXER_GROUP_TYPE.SFX_BAD, false, false, AUDIOSOURCE_KEY.NO_KEY_AUTODESTROY);
    }


}
