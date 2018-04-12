using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{

    public static CollectableManager INSTANCE;

    [SerializeField]
    List<GameObject> m_collectables;

    [SerializeField]
    static float m_spawnProba = 1f;

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
        int rndIndex = Random.Range((int)0, (int)m_collectables.Count - 1);
        float proba = Random.Range(0f, 1f);

        if (proba >= m_spawnProba)
        {
            SpawnCollectable(a_pos, m_collectables[rndIndex]);
        }
    }

    public void SpawnCollectable(Vector3 a_pos, GameObject a_boost)
    {
        Instantiate(a_boost, a_pos, new Quaternion(0, 0, 0, 0));
    }
}
