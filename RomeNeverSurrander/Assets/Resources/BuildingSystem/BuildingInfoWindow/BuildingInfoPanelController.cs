using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfoPanelController : MonoBehaviour
{
    public string description_text = "";
    public string Button_text = "";
    public bool is_show = false;
    public static float x_show_left = 0.7f, x_hide_left = 1.0f;
    public static float width = 0.3f;
    public static float y_min = 0.3f, y_max = 0.7f;
    public Text text;
    public Button button;
    // Start is called before the first frame update
    public void SetShow(bool is_show)
    {
        this.is_show = is_show;
    }
    public void SetDescription(string description)
    {
        this.description_text = description;
    }
    public void SetButtonText(string text)
    {
        this.Button_text = text;
    }



    void Start()
    {
        
    }
    public BuildingButton buildingButton;
    public Text button_text;
    public void Show(BuildingButton buildingButton)
    {
        if (!is_show)
        {
            is_show = true;
            text.text = description_text;
            button_text.text = Button_text;
            StartCoroutine(Move(gameObject, x_hide_left, x_hide_left + width, y_min, y_max, x_show_left, x_show_left + width, y_min, y_max, 5));
        }
        this.buildingButton = buildingButton;
    }
    IEnumerator Move(GameObject game_object, float start_min_x, float start_max_x, float start_min_y, float start_max_y, 
        float end_min_x, float end_max_x, float end_min_y, float end_max_y, float time
        )
    {

        for(int i = 1; i <= time; i++)
        {
            yield return new WaitForSeconds(0.03f);
            gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(start_min_x + (end_min_x - start_min_x) / time * i, start_min_y + (end_min_y - start_min_y) / time * i);
            gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(start_max_x + (end_max_x - start_max_x) / time * i, start_max_y + (end_max_y - start_max_y) / time * i);
        }


        
    }
    public void Hide()
    {
        if (is_show)
        {
            is_show = false;
            Vector2 start_min = gameObject.GetComponent<RectTransform>().anchorMin;
            Vector2 start_max = gameObject.GetComponent<RectTransform>().anchorMax;
            StartCoroutine(Move(gameObject, x_show_left, x_show_left + width, y_min, y_max, x_hide_left, x_hide_left + width, y_min, y_max, 5));
        }
    }
    public void QuickHide()
    {
        Vector2 start_min = gameObject.GetComponent<RectTransform>().anchorMin;
        Vector2 start_max = gameObject.GetComponent<RectTransform>().anchorMax;
        StartCoroutine(Move(gameObject, x_show_left, x_show_left + width, y_min, y_max, x_hide_left, x_hide_left + width, y_min, y_max, 1));
    }


    public void OnClickBuild()
    {
        //但我们点击建造或拆除按钮的时候,需要检测
        //如果我们有分配一个button,只有点击图标出现的才分配button
        //那就调用哪个button的onclick函数,在目标地点生成一个建筑物
        if (buildingButton != null)
        {

            buildingButton.Onclick();
        }
        //不然的话,检测一下是不是拆除,如果是,那么就拆了这个建筑,回点钱
        else if(button_text.text == "拆除" && building_to_destroy != null){
            StartCoroutine(WaitToSuicide());
            //Destroy(building_to_destroy);
        }
    }
    IEnumerator WaitToSuicide()
    {
        building_to_destroy.GetComponent<Animator>().SetBool("BuildingDestroy", true);
        yield return new WaitForSeconds(1.0f);
        Destroy(building_to_destroy);
    }
    public GameObject building_to_destroy;
    public void Assign_destruction(GameObject building)
    {
        building_to_destroy = building;
    }
        
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
