using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager INSTANCE;
    
    [SerializeField]
    GameObject EnemyGroupPrefab;

    [SerializeField]
    Ennemies m_EnemyPrefab;

    /// <summary>
    /// Path to the level designs file and associated attributes
    /// </summary>
    private string gameDataProjectFilePath = "/Assets/Levels/level_1.txt";

    private float spawnTime = 25f;            // How long between each spawn.
    private int _nbEnemyToSpawn = 6;
    private int _nbWavesEnemys = 6;

    private Vector3 initialPosition = new Vector3(0, 5, 0);
    private Vector3 enemyPosition = new Vector3(0, 5, 0);
    private Quaternion initialRotation = new Quaternion(0, 0, 0, 0);

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
        
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        /// <summary>
        /// If the game has ended
        /// </summary>
        /*
        if (CONDITION)
        {
            // ... exit the function.
            return;
        }
        */

        /// <summary>
        /// Instantiate a new group of ennemies
        /// </summary>
        GameObject EnemyGroup = Instantiate(EnemyGroupPrefab, initialPosition, initialRotation);

        for(float i = 0; i < _nbEnemyToSpawn; ++i)
        {
            Debug.Log(i);
            enemyPosition = new Vector3(((i*1.2f)/2 - (_nbEnemyToSpawn*1.2f)/4), 5 , 0);
            EnemyGroup.GetComponent<EnemyGroupBehavior>().AddChild(Instantiate(m_EnemyPrefab, enemyPosition, initialRotation));
        }
            
    }


    void loadGameData()
    {



    }










    /*
    public GameData gameData;

    

    [MenuItem("Window/Game Data Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(GameDataEditor)).Show();
    }

    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
        }
    }



    */







}
