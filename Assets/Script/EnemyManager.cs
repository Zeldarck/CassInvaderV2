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
    private string gameLevel = "level_1";

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
        

            loadGameData();

            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            for (float j = 0 ; j < _nbWavesEnemys; j++)
            {
                Spawn();
                StartCoroutine(Timer());
            }

            Debug.Log("1st wave finished");
        
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
        /// <summary>
        /// BUUUUGGGGGG  : La fonction bloque à la ligne jsonString = textAsset.text; 
        /// L'idée est de récupérer le texte du fichier json et ensuite updater les variables concernées pour 
        /// mettre a jour le level.
        /// Aucune idée du type qu'il faut mettre après "FromJson"... le type EnemyManager ne fonctionne
        /// pas.......
        /// </summary>
        TextAsset textAsset = (TextAsset)Resources.Load("level_1"); // Don't include the .json extension
        Debug.Log("step1");
        string jsonString = textAsset.text;
        Debug.Log("step2");
        JsonUtility.FromJsonOverwrite(JsonUtility.FromJson<string> (jsonString), this);

    }

    IEnumerator Timer()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(spawnTime);
        print(Time.time);
    }

}
