using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string building_name = "";
    private string building_path = "BuildingSystem/Buildings/Tower_A";
    public int price;
    
    public void Set_Building_Path(string b_path)
    {
        this.building_path = b_path;
    }
    public void Onclick()
    {
        GameObject temp =   (GameObject) Resources.Load(building_path);
        Vector2 position = BuildingControl.GetInstance().GetPositionForBuilding();
        if(position.x < 0 || position.y < 0)
        {
            return;
        }
        temp.GetComponent<Building>().price = price;
        temp.transform.position = new Vector3(position.x, 0, position.y);
        BuildingControl.GetInstance().RegisterBuilding(Instantiate(temp));
    }
    public void OnClickDescription()
    {
        BuildingInfoPanelController bi = GameObject.Find("BuildingInfoPanel").GetComponent<BuildingInfoPanelController>();
        BuildingDescription.getInstance().building_description_dict.TryGetValue(building_name, out string output);
        Building building = Resources.Load<Building>(building_path);
        output += "\n挣钱速度: " + building.money_earn + "/" + building.money_time + "时间单位";
        output += "\n价格: " + price;
        if (output != null)
        {
            bi.SetDescription(output);
        }
        bi.SetButtonText("建造");
        bi.is_show = false;
        
        bi.Show(this);
    }

    private bool IsPointerOverUIObject()
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
