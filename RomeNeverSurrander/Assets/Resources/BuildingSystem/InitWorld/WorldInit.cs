using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Building_Record
{
    public Building_Record(string p, float x, float y)
    {
        path = p;
        position = new Vector2(x, y);
    }
    public string path;
    public Vector2 position;
    public Vector3 GetVector3()
    {
        return new Vector3(position.x, 0, position.y);
    }
}


public class WorldInit : MonoBehaviour
{

    List<Building_Record> record = new List<Building_Record> {new Building_Record("BuildingSystem/Buildings/Farm", 30, 22),
        new Building_Record("BuildingSystem/Buildings/Castle", 14, 10) };

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(init());
        

    }
    IEnumerator init()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < record.Count; i++)
        {
            GameObject temp = (GameObject)Resources.Load(record[i].path);
            temp.transform.position = record[i].GetVector3();
            BuildingControl.GetInstance().SetPosition(record[i].position);
            BuildingControl.GetInstance().RegisterBuilding(Instantiate(temp));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
