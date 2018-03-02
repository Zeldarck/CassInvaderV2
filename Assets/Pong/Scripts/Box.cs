using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    public List<GameObject> m_listPrefabPickUp = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(0, 0, 0.4f);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Ball"))
        {
            if (m_listPrefabPickUp.Count > 0)
            {
              GameObject.Instantiate(m_listPrefabPickUp[(int)Mathf.Floor(Random.Range(0.0f, m_listPrefabPickUp.Count - 0.01f))],transform.position,Quaternion.identity,transform.parent);
            }
            Destroy(gameObject);
        }
    }

}
