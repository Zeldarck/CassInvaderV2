using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnnemyBehavior : MonoBehaviour {

    protected static int _POS_X = 0; // TO SET
    protected static int _POS_Y = 0; // TO SET

    protected Rigidbody _enemyRB;
    protected float _direction;

    [SerializeField]
    protected float _enemySpeed;

    [SerializeField]
    protected int m_life;

    public UnityEvent OnDie; // TO CHECK


    // Use this for initialization
    protected void Start()
    {
        _enemyRB = gameObject.GetComponent<Rigidbody>();
        gameObject.transform.position = new Vector2(_POS_X, _POS_Y);


        _direction = 1;


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
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            // TO DO 
        }
    }
    
    // Position update which is called once per frame
    void FixedUpdate()
    {
        /*
        //check if Y < 0.45 (0.9 = ennemy's height)
        if (gameObject.transform.position.y < (_MIN_Y + 0.8))
        {
            Debug.Log("An Enemy reached the bottom!");

            GameController.EndGame();
            Destroy(gameObject);
        }
        */


        // move accordingly 
        // To change and adapt with velocity
        Vector3 directionToGo = new Vector3(0, 0, 0);
        _enemyRB.position += directionToGo * Time.deltaTime;

    }


}
