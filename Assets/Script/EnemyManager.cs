using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager INSTANCE;

    /// <summary>
    /// Serialization of prefabs and set of spawn attributes
    /// </summary>
    
    [SerializeField]
    GameObject m_enemyGroupPrefab;

    [SerializeField]
    GameObject m_enemyPrefab;

    private Vector3 m_initialPosition = new Vector3(0, 5, 0);
    private Vector3 m_enemyPosition = new Vector3(0, 5, 0);
    private Quaternion m_initialRotation = new Quaternion(0, 0, 0, 0);

    /// <summary>
    /// Path to the level designs file and bunch of associated attributes
    /// </summary>
    [SerializeField]
    private string m_gameConfig = "levelConfig";
    private string m_gameLevel = "level_";

    [SerializeField]
    private int m_nbOfLevels = 1;
    [SerializeField]
    private int m_spawnTime = 25;            // How long between each spawn.
    [SerializeField]
    private int m_nbEnemyToSpawn = 3;
    [SerializeField]
    private int m_nbWavesEnemys = 3;

    private int m_nbWavesEnemysExecuted = 0;
    private int m_currentLevel = 1;

    void Start()
    {

        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(this);
        }
        else
        {
            INSTANCE = this;
        }
        
        LoadGameData(m_gameConfig);
        LevelInvoker();
        
    }

    void Spawn()
    {
        /// <summary>
        /// Instantiate a new group of ennemies
        /// </summary>
                
        if (m_nbWavesEnemysExecuted < m_nbWavesEnemys)
        {
            GameObject EnemyGroup = Instantiate(m_enemyGroupPrefab, m_initialPosition, m_initialRotation);
            for (float i = 0; i < m_nbEnemyToSpawn; ++i)
            {
                m_enemyPosition = new Vector3(((i * 1.2f) / 2 - ((m_nbEnemyToSpawn - 1) * 1.2f) / 4), 4, 0);
                EnemyGroup.GetComponent<EnemyGroupBehavior>().AddChild(Instantiate(m_enemyPrefab, m_enemyPosition, m_initialRotation).GetComponent<Ennemies>());
            }
        }
                 
        ++m_nbWavesEnemysExecuted;

    }


    void LoadGameData(string fileName)
    {
        /// <summary>
        /// Read a JSON file with indications about the current level.
        /// </summary>
        TextAsset textAsset = (TextAsset)Resources.Load(fileName); // Don't include the .json extension
        string jsonString = textAsset.text;
        JsonUtility.FromJsonOverwrite(jsonString, this);

    }

    void LevelInvoker()
    {

        LoadGameData(string.Concat(m_gameLevel, m_currentLevel));

        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", 2, m_spawnTime);

        // Here some UI stuff to show that a new level is upcoming...
    }


    void Update()
    {
        // Cancel all Invoke calls
        if (m_nbWavesEnemysExecuted > m_nbWavesEnemys)
        {
            CancelInvoke();
            ++m_currentLevel;
            m_nbWavesEnemysExecuted = 0;
            if (m_currentLevel <= m_nbOfLevels)
            {
                LevelInvoker();
            }
        }
    }
}
