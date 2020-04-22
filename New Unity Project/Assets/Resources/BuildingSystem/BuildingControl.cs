using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControl
{
    //控制建筑的建造情况
    public static int x_grid = 8, y_grid = 8;
    public static GameObject[][] building_infos = new GameObject[x_grid][];
    private Vector2 position = new Vector2(0, 0);
    public static BuildingControl building = new BuildingControl();
    public BuildingControl()
    {
        for(int i = 0; i < x_grid; i++)
        {
            building_infos[i] = new GameObject[y_grid];
        }
    }

    static public BuildingControl GetInstance()
    {
        return building;
    }
    public void SetPosition(int x, int y)
    {
        position = new Vector2(x, y);
    }
    public void SetPosition(Vector2 v)
    {
        position = v;
    }
    public Vector2 GetPosition()
    {
        if(position.x == 0 && position.y == 0)
        {
            return new Vector2(-1, -1);
        }
        return position;
    }
    public Vector2 GetPositionForBuilding(GameObject gameObject = null)
    {
        if (position.x == 0 && position.y == 0)
        {
            return new Vector2(-1.0f, -1.0f);
        }
        int temp_x = ((int)position.x - 2) / 4;
        int temp_y = ((int)position.y - 2) / 4;
        if (building_infos[temp_x][temp_y] == null)
        {
            building_infos[temp_x][temp_y] = gameObject;
            return position;
        }
        return new Vector2(-1.0f, -1.0f);
    }
    public Vector2 RegisterBuilding(GameObject gameObject)
    {
        //这里多线程的时候可能会炸，慢慢改吧
        return GetPositionForBuilding(gameObject);
    }
    public static void UpdateAll()
    {
        for(int i = 0; i < x_grid; i++)
        {
            for(int j = 0; j < y_grid; j++)
            {
                if(building_infos[i][j] != null)
                {
                    building_infos[i][j].GetComponent<Building>().UpdateInfo();
                }
                
            }
        }
    }
}
