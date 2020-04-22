using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainInfo : MonoBehaviour
{
    //private TerrainInfo terrain = new TerrainInfo();
    public static List<GameObject> terrain_lands;
    public GameObject GetTerrainBuilding(int x, int y)
    {
        return terrain_lands[x * 8 + y];
    }
    public static void ChangePlaneState(bool state)
    {
        for(int i = 0; i < terrain_lands.Count; i++)
        {
            try
            {
                terrain_lands[i].GetComponent<MeshRenderer>().enabled = state;
            }catch(Exception e)
            {

            }
            
        }
    }
    public void Initialize(int[][] terrain_info)
    {
        terrain_lands = new List<GameObject>();
        for (int i = 0; i < terrain_info.Length; i++)
        {

            for (int j = 0; j < terrain_info[i].Length; j++)
            {
                if (terrain_info[i][j] < 0)
                {
                    terrain_lands.Add(null);

                }
                else
                {
                    //Debug.Log(i + ":" + j);
                    
                    GameObject obj = (GameObject)Resources.Load("BuildingSystem/land_base");
                    
                    obj.transform.position = new Vector3(2 + 4 * i, 0.1f, 2 + 4 * j);
                    obj.GetComponent<LandController>().position = new Vector2(2 + 4 * i, 2 + 4 * j);
                    obj.GetComponent<LandController>().hello = "Panel: " + i + ", " + j;
                    obj.tag = "terrain_plane";
                    
                    //Debug.Log(terrain_lands[i][j][0].GetComponent<LandController>().hello);
                    GameObject g = Instantiate(obj, gameObject.transform);
                    
                    
                    //Debug.Log("hello");
                    
                    terrain_lands.Add(g);
                    
                    //Debug.Log(terrain_lands.Count);
                    //StartCoroutine(temp(g));
                }
            }

        }
        ChangePlaneState(false);
    }
    
    public void OnDisable()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                //terrain_lands[i * 0 + j].SetActive(false);
                //Debug.Log("hello");
            }
        }

        
    }
}
