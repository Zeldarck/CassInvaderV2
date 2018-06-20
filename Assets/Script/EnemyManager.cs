using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{



    /// <summary>
    /// Serialization of prefabs and set of spawn attributes
    /// </summary>

    [SerializeField]
    GameObject m_enemyGroupPrefab;

    [SerializeField]
    GameObject m_walkerPrefab;

    [SerializeField]
    GameObject m_runnerPrefab;

    [SerializeField]
    GameObject m_giantPrefab;

    [SerializeField]
    GameObject m_flankerPrefab;

    [SerializeField]
    GameObject m_sweeperPrefab;

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
    private int m_nbWavesEnemys = 3;
    [SerializeField]
    private string[] m_typeArray = { "walker" };

    [SerializeField]
    float m_enemySpeed = 1;

    float m_currentEnnemySpeed;
    [SerializeField]
    float m_enemySpeedStep = 0;


    int m_nbWavesEnemysExecuted = 0;
    int m_currentLevel = 1;
    protected GameObject currentPrefab;



    #region GetterSetter

    public float EnemySpeed
    {
        get
        {
            return m_currentEnnemySpeed;
        }
    }

    #endregion

// SetUp
//------------------------------------------------------------------------------------

    void Start()
    {
        
    }

    public void StartSpawn()
    {
        LoadGameData(m_gameConfig);
        m_currentLevel = 1;
        m_currentEnnemySpeed = m_enemySpeed;
        LevelInvoker();
    }

    public void StopSpawn()
    {        
        CancelInvoke();
    }

    public int GetCurrentLevel()
    {
        return m_currentLevel;
    }

    public int GetMaxLevel()
    {
        return m_nbOfLevels;
    }


    // JSON
    //------------------------------------------------------------------------------------

    /// <summary>
    /// Read a JSON file with indications about the current level
    /// Ideally, to put into another JSON function with a dissociation of level parameters 
    /// </summary>
    void LoadGameData(string fileName)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(fileName); // Don't include the .json extension
        string jsonString = textAsset.text;
        JsonUtility.FromJsonOverwrite(jsonString, this);
    }
        

    #region LevelManagement

    /// <summary>
    /// Load a new level and start invoking ennemies waves
    /// </summary>
    void LevelInvoker()
    {

        LoadGameData(string.Concat(m_gameLevel, m_currentLevel));

        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", 2, m_spawnTime);

        // Here some UI stuff to show that a new level is upcoming...
    }
    
    /// <summary>
    /// Instantiate a new group of ennemies
    /// </summary>
    void Spawn()
    {

        if (m_nbWavesEnemysExecuted < m_nbWavesEnemys)
        {
            m_currentEnnemySpeed += m_enemySpeedStep;
            GameObject EnemyGroup = GameObjectManager.INSTANCE.Instantiate(m_enemyGroupPrefab, m_initialPosition, m_initialRotation, SPAWN_CONTAINER_TYPE.DESTRUCTIBLE);
            for (int i = 0; i < m_typeArray.Length; ++i)
            {
                switch (m_typeArray[i])
                {
                    case "walker":
                        currentPrefab = m_walkerPrefab;
                        break;

                    case "runner":
                        currentPrefab = m_runnerPrefab;
                        break;

                    case "giant":
                        currentPrefab = m_giantPrefab;
                        break;

                    case "flanker":
                        currentPrefab = m_flankerPrefab;
                        break;

                    case "sweeper":
                        currentPrefab = m_sweeperPrefab;
                        break;

                    default:
                        currentPrefab = m_walkerPrefab;
                        break;
                }

                m_enemyPosition = new Vector3(((i * 1.2f) / 2 - ((m_typeArray.Length - 1) * 1.2f) / 4), 4, 0);
                EnemyGroup.GetComponent<EnemyGroupBehavior>().AddChild(GameObjectManager.INSTANCE.Instantiate(currentPrefab, m_enemyPosition ,m_initialRotation).GetComponent<Ennemies>());
            }
        }

        ++m_nbWavesEnemysExecuted;

    }

    /// <summary>
    /// Check out the number of waves already invoked and cancel the LevelInvoker function accordingly
    /// Ideally to rework accordingly to the JSON rework
    /// </summary>
    void Update()
    {
        // Cancel all Invoke calls
        if (m_nbWavesEnemysExecuted > m_nbWavesEnemys && GameManager.INSTANCE.IsGameRunning)
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

    #endregion
}
