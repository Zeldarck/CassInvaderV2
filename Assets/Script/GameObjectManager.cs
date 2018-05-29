using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GameObjectManager : Singleton<GameObjectManager>
{

    [SerializeField]
    GameObject m_spawnDestructibleObjectsContainer;

    [SerializeField]
    GameObject m_spawnUndestructiblesObjectsContainer;

    void Start()
    {
    }

    public GameObject SpawnObject(GameObject a_gameObject, Vector3 a_position, Quaternion a_rotation, string a_type)
    {
        GameObject container;

        switch (a_type)
        {
            case "Destructible":
                container = m_spawnDestructibleObjectsContainer;
                break;

            case "Undestructible":
                container = m_spawnUndestructiblesObjectsContainer;
                break;
                
            default:
                container = m_spawnDestructibleObjectsContainer;
                break;
        }

        return Instantiate(a_gameObject, a_position, a_rotation, container.transform);

    }
    
    public void DestroyObjects(string a_type)
    {
        GameObject container;

        switch (a_type)
        {
            case "Destructible":
                container = m_spawnDestructibleObjectsContainer;
                break;

            case "Undestructible":
                container = m_spawnUndestructiblesObjectsContainer;
                break;

            default:
                container = m_spawnDestructibleObjectsContainer;
                break;
        }

        Utils.DestroyChilds(container.transform);
    }
    
    void Update()
    {
    }
    
}
