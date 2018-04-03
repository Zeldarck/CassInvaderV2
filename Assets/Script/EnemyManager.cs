using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager INSTANCE;

    /// <summary>
    /// Serialization of prefabs and set of spawn attributes
    /// </summary>
    
    [SerializeField]
    GameObject EnemyGroupPrefab;

    [SerializeField]
    Ennemies m_EnemyPrefab;

    private Vector3 initialPosition = new Vector3(0, 5, 0);
    private Vector3 enemyPosition = new Vector3(0, 5, 0);
    private Quaternion initialRotation = new Quaternion(0, 0, 0, 0);

    /// <summary>
    /// Path to the level designs file and bunch of associated attributes
    /// </summary>

    private string gameConfig = "levelConfig";
    private string gameLevel = "level_";

    [SerializeField]
    private int _nbOfLevels = 1;
    [SerializeField]
    private int spawnTime = 25;            // How long between each spawn.
    [SerializeField]
    private int _nbEnemyToSpawn = 3;
    [SerializeField]
    private int _nbWavesEnemys = 3;

    private int _nbWavesEnemysExecuted = 0;
    private int _currentLevel = 1;

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

        loadGameData(gameConfig);
        LevelInvoker();
        
    }

    void Spawn()
    {
        /// <summary>
        /// Instantiate a new group of ennemies
        /// </summary>
                
        if (_nbWavesEnemysExecuted < _nbWavesEnemys)
        {
            GameObject EnemyGroup = Instantiate(EnemyGroupPrefab, initialPosition, initialRotation);
            for (float i = 0; i < _nbEnemyToSpawn; ++i)
            {
                enemyPosition = new Vector3(((i * 1.2f) / 2 - ((_nbEnemyToSpawn - 1) * 1.2f) / 4), 5, 0);
                EnemyGroup.GetComponent<EnemyGroupBehavior>().AddChild(Instantiate(m_EnemyPrefab, enemyPosition, initialRotation));
            }
        }

        else
        {

        }
            

        ++_nbWavesEnemysExecuted;
    }


    void loadGameData(string fileName)
    {
        /// <summary>
        /// Read a JSON file with indications about the current level.
        /// </summary>
        TextAsset textAsset = (TextAsset)Resources.Load(fileName); // Don't include the .json extension
        string jsonString = textAsset.text;
        JsonUtility.FromJsonOverwrite(jsonString, this);

        Debug.Log(string.Concat("levels :", _nbOfLevels));
        Debug.Log(string.Concat("spwantime", spawnTime));
        Debug.Log(string.Concat("enemy2spawn",_nbEnemyToSpawn));
        Debug.Log(string.Concat("nbwaves",_nbWavesEnemys));

    }

    void LevelInvoker()
    {
        loadGameData(string.Concat(gameLevel, _currentLevel));
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", 2, spawnTime);

        // Here some UI stuff to show that a new level is upcoming...
    }


    void Update()
    {
        // Cancel all Invoke calls
        if (Transform.childCount)
        {
            CancelInvoke();
            ++_currentLevel;
            _nbWavesEnemysExecuted = 0;
            if (_currentLevel <= _nbOfLevels)
            {
                LevelInvoker();
            }
        }
    }
}
