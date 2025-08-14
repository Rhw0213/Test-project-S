using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public GameObject player;

    private Dictionary<string, List<GameObject>> enemies = new Dictionary<string, List<GameObject>>();

    public Dictionary<string, GameObject> inspectorEnemies;

    [Header ("적 이름과 객체를 저장")]
    public MySTDLib.MyDictionary<string, GameObject> enemiesRegister;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        foreach (var enemy in enemiesRegister.Ref)
        {
            GameObject obj = Instantiate(enemy.Value, transform);
            AddEnemy(enemy.Key, obj, 1);
        }
    }

    private List<GameObject> GetEnemyList(string enemyType)
    {
        if (!enemies.ContainsKey(enemyType))
        {
            enemies[enemyType] = new List<GameObject>();
        }

        return enemies[enemyType];
    }

    private void AddEnemy(string enemyType, GameObject enemyObject, uint count = 1)
    {
        List<GameObject> enemyList = GetEnemyList(enemyType);

        for (uint i = 0; i < count; i++)
        {
            enemyList.Add(Instantiate(enemyObject, transform));
        }
    }
}
