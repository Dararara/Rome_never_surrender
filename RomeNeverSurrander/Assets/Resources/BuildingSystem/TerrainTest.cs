using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class TerrainTest : MonoBehaviour
{
    public int x_grid;
    public int y_grid;

    public int[][] grid;
    TerrainInfo terrainInfo;
    // Start is called before the first frame update
    void Start()
    {
        x_grid = BuildingControl.x_grid;
        y_grid = BuildingControl.y_grid;

        if (x_grid > 0 && y_grid > 0)
        {
            grid = new int[x_grid][];
            for (int i = 0; i < x_grid; i++)
            {
                grid[i] = new int[y_grid];
            }
        }

        TextAsset terrain_infomation = Resources.Load<TextAsset>("BuildingSystem/terrain_infomation");
        string terrain_content = terrain_infomation.text.Trim();
        string[] terrains = terrain_content.Split('\n');
        for(int i = 0; i < terrains.Length; i++)
        {
            string[] temp_terrain = terrains[i].Split(' ');
            for(int j = 0; j < temp_terrain.Length; j++)
            {
                int.TryParse(temp_terrain[j], out int result);
                grid[i][j] = result;
            }
        }
        
        //Debug.Log(terrain_infomation.text);
        
        //Debug.Log("done");
        terrainInfo = gameObject.GetComponent<TerrainInfo>();
        terrainInfo.Initialize(grid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
