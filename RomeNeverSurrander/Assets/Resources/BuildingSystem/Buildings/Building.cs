using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, Alive_Item
{
    public bool IsAlive()
    {
        return health >= 0;
    }
    
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();   
    }
    public float currentTime = -1;
    public float lastTime = -1;
    public int money_time;
    public int money_earn;
    public string hello;
    public string description;

    public virtual void UpdateInfo()
    {
        if(lastTime == -1)
        {
            lastTime = Time.time;
            currentTime = Time.time;
            return;

        }

        currentTime = Time.time;
        if(currentTime - lastTime >= money_time)
        {
            DataRecorder.AddMoney(money_earn);
            Debug.Log(money_earn);
            lastTime = currentTime;
            GameObject popup = Resources.Load<GameObject>("BuildingSystem/PopUp/MoneyPopUp");
            popup.transform.position = transform.position;
            
            Instantiate(popup, transform.position, Quaternion.LookRotation(GameObject.Find("Main Camera").transform.position)).GetComponent<PopUpController>().SetText("Money +" + money_earn.ToString()); ;
            

        }
        //Debug.Log(hello);
    }
    public string building_name = "Castle";
    public int price = 10;
    public void OnClickDescription()
    {
        if (TargetHelper.IsPointerOverUIObject()) return;
        BuildingInfoPanelController bi = GameObject.Find("BuildingInfoPanel").GetComponent<BuildingInfoPanelController>();
        BuildingDescription.getInstance().building_description_dict.TryGetValue(building_name, out string output);
        output += "挣钱速度: " + money_earn + "/" + money_time + "时间单位";
        output += "价格: " + price;
        
        if (output != null)
        {
            bi.SetDescription(output);
        }
        bi.SetButtonText("拆除");
        bi.is_show = false;

        bi.Show(null);
    }
    public void OnMouseDown()
    {
        if (TargetHelper.IsPointerOverUIObject()) return;
        GameObject panel;
        try
        {
            panel = GameObject.Find("BuildingInfoPanel");
        }
        catch
        {
            return;
        }
        panel.TryGetComponent<BuildingInfoPanelController>(out BuildingInfoPanelController bi);
        if (bi == null) return;

        BuildingDescription.getInstance().building_description_dict.TryGetValue(building_name, out string output);
        output += "挣钱速度: " + money_earn + "/" + money_time + "时间单位";
        output += "价格: " + price + "挣钱呀";
        
        if (output != null)
        {
            bi.SetDescription(output);
        }
        bi.SetButtonText("拆除");
        bi.is_show = false;
        bi.Assign_destruction(gameObject);
        bi.Show(null);
    }
    public float health = 10.0f;

    public void GetHit(float damage)
    {
        if(health < 0)
        {
            return;
        }
        health -= damage;
        if(health < 0)
        {
            StartCoroutine(WaitToSuicide());
        }
    }
    IEnumerator WaitToSuicide()
    {
        gameObject.GetComponent<Animator>().SetBool("BuildingDestroy", true);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }


    public void GetDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
