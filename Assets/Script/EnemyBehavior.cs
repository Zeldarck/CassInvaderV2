using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour {

    protected static int _POS_X = 0; // TO SET : Initial Z position of the enemy
    protected static int _POS_Y = 0; // TO SET : Initial Y position of the enemy
    protected static int _DEADZONE = 0; // TO SET : Lower limit for which the game ends if an invader come through

    protected float _enemyRadius;
    protected float _XReverse = -1;
    protected float _YForward = 0;

    [SerializeField]
    protected float _enemySpeed;

    [SerializeField]
    protected int m_life;

    public UnityEvent OnDie; // TO CHECK

    // Use this for initialization
    protected void Start()
    {
        
        // Set the initial position of the enemy     
        gameObject.transform.position = new Vector2(_POS_X, _POS_Y);
        
        OnDie.AddListener(() => StartCoroutine(AutoDestroy())); // TO CHECK
                
    }
    
    // On trigger get damages from any damage source
    public bool GetDamage(int a_damage)
    {

        m_life -= a_damage;
        if (m_life <= 0)
        {
            OnDie.Invoke(); // TO CHECK
            return true;
        }

        return false;

    }

    
    // Self Destruction on trigger
    IEnumerator AutoDestroy()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }


    
    // In case of collision with the border walls, change the direction of the ennemy
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            _XReverse *= -1;
            _YForward = -1;
        }
    }
    

    protected Vector2 DirectionComputation()
    {
        Vector2 directionComputed = new Vector2(_XReverse, _YForward);

        directionComputed *= _enemySpeed;

        Debug.Log(directionComputed);
        return directionComputed;
    }


    // Position update which is called once per frame
    void FixedUpdate()
    {
        
        // check if an invader has made it to the Dead Zone
        if (gameObject.transform.position.y < (_DEADZONE + _enemyRadius))
        {
            Debug.Log("An Enemy reached the bottom!");

            // GameController.EndGame(); // TO LINK OR IMPLEMENT LATER
            Destroy(gameObject);
        }
                
        // move accordingly 
        Vector2 direction = DirectionComputation();
        Debug.Log(direction);
        gameObject.transform.position = (Vector2) gameObject.transform.position + direction * Time.deltaTime;

    }


}
