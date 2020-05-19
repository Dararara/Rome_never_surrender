using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class BuildingItem
{
    public string image_src;
    public string description;
    public int price;
    public string building_path;
    public string building_name;
    public BuildingItem(string building_name, string image_src, string description = "", int price = 999, string building_path = "BuildingSystem/Buildings/Tower_A")
    {
        this.image_src = image_src;
        this.description = description;
        this.price = price;
        this.building_path = building_path;
        this.building_name = building_name;
    }
    public BuildingItem()
    {
        this.image_src = "Pictures/green";
        this.description = "";
        this.price = 999;
        this.building_path = "BuildingSystem/Buildings/Tower_A";
    }
}

public class BuildingPackage
{
    public List<BuildingItem> building_infos;
    public static BuildingPackage build = new BuildingPackage();
    public BuildingPackage()
    {
        building_infos = new List<BuildingItem>();
        building_infos.Add(new BuildingItem("Castle", "Pictures/castle", building_path :"BuildingSystem/Buildings/Castle", price:100));
        building_infos.Add(new BuildingItem("Farm", "Pictures/farm", building_path: "BuildingSystem/Buildings/Farm", price:50));
        building_infos.Add(new BuildingItem("Ballista", "Pictures/ballista", building_path: "BuildingSystem/Buildings/Ballista_lv1", price: 300));
        //building_infos.Add(new BuildingItem());
        //building_infos.Add(new BuildingItem());
        //building_infos.Add(new BuildingItem());

    }
}
public class BuildingPackController : MonoBehaviour
{
    //public float pack_scale = 1.5f;
    //
    public float BuildingItemWidth;
    //public float BuildingPanelWidth = 0.5f * 1.5f;
    //public float BuildingItemStartX = 0.075f * 1.5f;
    public float y_max = 0.3f;
    public float y_min = 0.0f;

    public float button_width;//展开收回的button的宽度
    public float pack_width = 1.5f;
    public BuildingPackage my_pack;
    public Image image;
    //这里负责控制建筑物背包的展示和收回，用位移的方法展示和收回背包，动态控制背包长度和内容
    // Start is called before the first frame update
    void Start()
    {
        BuildingItemWidth = 0.075f * pack_width;
        button_width = 0.075f * pack_width;
        //GameObject[] temp = GameObject.FindGameObjectsWithTag("BuildingItem");
        //for (int i = 0; i < temp.Length; i++)
        //{
        //    buildings.Add(temp[i]);
        //}
        my_pack = BuildingPackage.build;
        
    }
    
    public void ClickShow()
    {
        TerrainInfo.ChangePlaneState(true);
        Debug.Log(true);
        text.text = "";
        is_show = true;
        image.sprite = Resources.Load<Sprite>("Pictures/back");
        LoadBuildingItem(my_pack.building_infos);
        
        FileOperation.FileWrite(Path.Combine(FileOperation.BaseAndroidPath, "newfile"), "hello world");
        AddLog(FileOperation.BaseAndroidPath);
        AddLog(FileOperation.FileRead(Path.Combine(FileOperation.BaseAndroidPath, "newfile")));
    }
    public void ClickHide()
    {
        TerrainInfo.ChangePlaneState(false);
        //GameObject.Find("Main Camera").GetComponent<Camera>().cullingMask &= ~(1 << 10);
        GameObject.Find("BuildingInfoPanel").GetComponent<BuildingInfoPanelController>().Hide();
        text.text = "";
        is_show = false;
        image.sprite = Resources.Load<Sprite>("Pictures/on");
        BuildingControl.GetInstance().SetPosition(new Vector2(-1, -1));
        
        StartCoroutine(Move(gameObject.GetComponent<RectTransform>().anchorMin,
            gameObject.GetComponent<RectTransform>().anchorMax,
            new Vector2(1 - button_width, y_min), new Vector2(1 + pack_width - button_width, y_max)
            ));
    }
    public bool is_show = true;
    public void Onclick()
    {
        
        if (is_show)
        {
            ClickHide();
            
        }
        else
        {
            ClickShow();
            
        }
    }

    public Vector2 slx, srx, tlx, trx;
    IEnumerator Move(Vector2 start_left_x, Vector2 start_right_x, Vector2 target_left_x, Vector2 target_right_x)
    {
        slx = start_left_x;
        srx = start_right_x;
        tlx = target_left_x;
        trx = target_right_x;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.005f);
            gameObject.GetComponent<RectTransform>().anchorMin = start_left_x + (target_left_x - start_left_x) * i / 19;
            gameObject.GetComponent<RectTransform>().anchorMax = start_right_x + (target_right_x - start_right_x) * i / 19;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public Text text;
    public void AddLog(string str)
    {
        text.text += str;
    }


    public List<GameObject> buildings;
    public void LoadBuildingItem(List<BuildingItem> building_infos)
    {
        Debug.Log("Get it, start to load buildings");
        for (int i = 0; i < building_infos.Count; i++)
        {
            //挨个更新背包内建筑物信息
            text.text += building_infos[i].image_src + "\n";
            text.text += building_infos[i].price + "\n";


            //Debug.Log(building_infos.Count);
            //绘制图片
            Sprite sprite = Resources.Load<Sprite>(building_infos[i].image_src);
            buildings[i].GetComponent<BuildingItemController>().SetImage(sprite);
            //建筑路径
            buildings[i].GetComponent<BuildingButton>().Set_Building_Path(building_infos[i].building_path);
            buildings[i].GetComponent<BuildingButton>().building_name = building_infos[i].building_name;
            buildings[i].GetComponent<BuildingButton>().price = building_infos[i].price;
            buildings[i].GetComponent<BuildingItemController>().SetPriceText(building_infos[i].price);
        }
        //0.075 * (i + 1)
        //gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(1 - 0.075f * (building_infos.Count+1), 0.0f);
        //gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(2 - 0.075f * (building_infos.Count+1), 0.2f);
        StartCoroutine(Move(gameObject.GetComponent<RectTransform>().anchorMin,
            gameObject.GetComponent<RectTransform>().anchorMax,
            new Vector2(1 - BuildingItemWidth * (building_infos.Count + 1), y_min),
            new Vector2(1 + pack_width - BuildingItemWidth * (building_infos.Count + 1), y_max)
            ));
        GameObject.Find("BuildingInfoPanel").GetComponent<BuildingInfoPanelController>().Hide();
    }


}
