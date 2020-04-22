using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    NoWeapon = 0,
    Archer = 1
}

public class EnemyGroup
{
    public List<EnemyType> enemys;
    public EnemyGroup(List<EnemyType> enemyTypes)
    {
        enemys = enemyTypes;
    }
}


public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public float countDown;
    public float time_limit = 5f;
    public int index = 0;
    public List<EnemyGroup> enemyGroups = new List<EnemyGroup>();
    void Start()
    {
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.NoWeapon}));
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.NoWeapon, EnemyType.NoWeapon }));
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.NoWeapon, EnemyType.NoWeapon, EnemyType.NoWeapon }));
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.NoWeapon, EnemyType.NoWeapon, EnemyType.NoWeapon }));
        //countDown = time_limit;
    }
    public GameObject NoWeapon1;
    public Transform SpawnPoint;
    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown < 0 && index < enemyGroups.Count)
        {
            StartCoroutine(SpawnEnemy());
            countDown = time_limit;
        }
    }
    IEnumerator SpawnEnemy()
    {
        EnemyGroup enemyGroup = enemyGroups[index++];
        for (int i = 0; i < enemyGroup.enemys.Count; i++)
        {
            Debug.Log(i);
            Instantiate(NoWeapon1, SpawnPoint.position, SpawnPoint.rotation);
            yield return new WaitForSeconds(1f);
        }
    }
}
