using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    NoWeapon = 0,
    Archer = 1,
    Oruga = 2,
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
    public float time_limit = 60f;
    public int index = 0;
    public List<EnemyGroup> enemyGroups = new List<EnemyGroup>();
    void Start()
    {
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.NoWeapon}));
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.NoWeapon, EnemyType.NoWeapon }));
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.NoWeapon, EnemyType.NoWeapon, EnemyType.NoWeapon }));
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.NoWeapon, EnemyType.NoWeapon, EnemyType.NoWeapon }));
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.NoWeapon, EnemyType.Archer, EnemyType.Archer }));
        enemyGroups.Add(new EnemyGroup(new List<EnemyType>() { EnemyType.Oruga }));
        //countDown = time_limit;
        kibo_index = 0;
        kibo_limit = 5;
    }
    public int kibo_index;
    public int kibo_limit;
    public GameObject NoWeapon1;
    public GameObject Archer;
    public GameObject Oruga;
    public Transform SpawnPoint;
    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown < 0/* && index < enemyGroups.Count*/)
        {
            StartCoroutine(SpawnEnemy());
            countDown = time_limit;
        }
    }

    IEnumerator SpawnEnemy()
    {
        /*
        EnemyGroup enemyGroup = enemyGroups[index++];
        for (int i = 0; i < enemyGroup.enemys.Count; i++)
        {
            Debug.Log(i);
            Instantiate(GetPrefab(enemyGroup.enemys[i]), SpawnPoint.position, SpawnPoint.rotation);
            yield return new WaitForSeconds(1f / DataRecorder.play_speed);
        }*/
        kibo_index++;
        if(kibo_index > kibo_limit)
        {
            kibo_index = 0;
            Instantiate(GetPrefab(EnemyType.Oruga), SpawnPoint.position, SpawnPoint.rotation);
            countDown = 150;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(GetPrefab(RandomEnumValue<EnemyType>()), SpawnPoint.position, SpawnPoint.rotation);
                yield return new WaitForSeconds(2f / DataRecorder.play_speed);

            }
        }
        

    }
    public static T RandomEnumValue<T>()
    {
        var v = System.Enum.GetValues(typeof(T));
        return (T)v.GetValue(new System.Random().Next(v.Length - 1));
    }
    
    public GameObject GetPrefab(EnemyType enemyType)
    {
        //根据type得到对应的prefab

        switch (enemyType)
        {
            case EnemyType.NoWeapon:
                return NoWeapon1;
            case EnemyType.Archer:
                return Archer;
            case EnemyType.Oruga:
                return Oruga;
        }
        return null;
    }
}
