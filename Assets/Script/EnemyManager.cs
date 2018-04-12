using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager INSTANCE;

    #region Variables

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
    [SerializeField]
    private string m_type = "walker";

    private int m_nbWavesEnemysExecuted = 0;
    private int m_currentLevel = 1;
    private GameObject currentPrefab;

    #endregion

    #region SetUp

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
        
    }

    public void StartSpawn()
    {
        LoadGameData(m_gameConfig);
        LevelInvoker();
    }

    #endregion

    #region JSON

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

    #endregion

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
        switch (m_type)
        {
            case "walker" :
                currentPrefab = m_walkerPrefab;
                break;

            case "runner" :
                currentPrefab = m_runnerPrefab;
                break;

            case "giant" :
                currentPrefab = m_giantPrefab;
                break;

            default:
                currentPrefab = m_walkerPrefab;
                break;
        }
        
        if (m_nbWavesEnemysExecuted < m_nbWavesEnemys)
        {
            GameObject EnemyGroup = Instantiate(m_enemyGroupPrefab, m_initialPosition, m_initialRotation);
            for (float i = 0; i < m_nbEnemyToSpawn; ++i)
            {
                m_enemyPosition = new Vector3(((i * 1.2f) / 2 - ((m_nbEnemyToSpawn - 1) * 1.2f) / 4), 4, 0);
                EnemyGroup.GetComponent<EnemyGroupBehavior>().AddChild(Instantiate(m_walkerPrefab, m_enemyPosition, m_initialRotation).GetComponent<Ennemies>());
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

    #endregion
}
