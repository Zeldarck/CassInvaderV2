using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{

    public static CollectableManager INSTANCE;

    [SerializeField]
    List<GameObject> m_collectables;

    [SerializeField]
    List<float> m_spawnProba;

    private void Start()
    {
        #region Singleton
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
        }
        else
        {
            INSTANCE = this;
        }
        #endregion
    }

    public void EnemyDestroyed(int a_enemyLvl, Vector3 a_pos)
    {
        int enemyIndex = a_enemyLvl - 1;
        float proba = Random.Range(0f, 1f);

       if (proba <= m_spawnProba[enemyIndex])
        {
            SpawnCollectable(a_pos, m_collectables[enemyIndex]);
        }
    }

    public void SpawnCollectable(Vector3 a_pos, GameObject a_boost)
    {
        Instantiate(a_boost, a_pos, new Quaternion(0, 0, 0, 0));
    }
}
