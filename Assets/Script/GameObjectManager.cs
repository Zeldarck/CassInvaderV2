using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum SPAWN_CONTAINER_TYPE { NOTHING, DESTRUCTIBLE, UNDESTRUCTIBLE };

public class GameObjectManager : Singleton<GameObjectManager>
{

    [SerializeField]
    GameObject m_spawnDestructibleObjectsContainer;

    [SerializeField]
    GameObject m_spawnUndestructiblesObjectsContainer;

    void Start()
    {
    }

    public GameObject SpawnObject(GameObject a_gameObject, Vector3 a_position, Quaternion a_rotation, SPAWN_CONTAINER_TYPE a_type = SPAWN_CONTAINER_TYPE.NOTHING)
    {
        GameObject container;

        switch (a_type)
        {
            case SPAWN_CONTAINER_TYPE.DESTRUCTIBLE:
                container = m_spawnDestructibleObjectsContainer;
                break;

            case SPAWN_CONTAINER_TYPE.UNDESTRUCTIBLE:
                container = m_spawnUndestructiblesObjectsContainer;
                break;
                
            default:
                container = m_spawnDestructibleObjectsContainer;
                break;
        }

        return Instantiate(a_gameObject, a_position, a_rotation, container.transform);

    }
    
    public void DestroyObjects(SPAWN_CONTAINER_TYPE a_type)
    {
        GameObject container;

        switch (a_type)
        {
            case SPAWN_CONTAINER_TYPE.DESTRUCTIBLE:
                container = m_spawnDestructibleObjectsContainer;
                break;

            case SPAWN_CONTAINER_TYPE.UNDESTRUCTIBLE:
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
