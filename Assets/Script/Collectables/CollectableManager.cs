using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ProbaCollectableLink
{
    [SerializeField]
    GameObject m_prefabCollectable;
    [SerializeField]
    float m_spawnProba;

    public float SpawnProba
    {
        get
        {
            return m_spawnProba;
        }
    }

    public GameObject PrefabCollectable
    {
        get
        {
            return m_prefabCollectable;
        }
    }
}

public class CollectableManager : Singleton<CollectableManager>
{


    [SerializeField]
    List<ProbaCollectableLink> m_probaCollectableLinkList;

    public void EnemyDestroyed(int a_enemyLvl, Vector3 a_pos)
    {
        int enemyIndex = a_enemyLvl - 1;
        float proba = Utils.RandomFloat(0, 1);

       if (proba <= m_probaCollectableLinkList[enemyIndex].SpawnProba)
        {
            SpawnCollectable(a_pos, m_probaCollectableLinkList[enemyIndex].PrefabCollectable);
        }
    }

    public void SpawnCollectable(Vector3 a_pos, GameObject a_boost)
    {
        GameObjectManager.INSTANCE.Instantiate(a_boost, a_pos, new Quaternion(0, 0, 0, 0), SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);
    }
}
