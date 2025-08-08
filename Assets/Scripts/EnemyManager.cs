using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public GameObject player;

    public GameObject enemy;

    private Dictionary<string, List<GameObject>> enemies = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        //GameObject obj = Instantiate(enemy);
        //
        //AddEnemy("default", obj);
    }


    private List<GameObject> GetEnemyList(string enemyType)
    {
        if (!enemies.ContainsKey(enemyType))
        {
            enemies[enemyType] = new List<GameObject>();
        }

        return enemies[enemyType];
    }

    private void AddEnemy(string enemyType, GameObject enemyObject)
    {
        List<GameObject> enemyList = GetEnemyList(enemyType);
        enemyList.Add(enemyObject);
    }
}
