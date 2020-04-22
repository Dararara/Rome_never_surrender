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
        if(buildingButton != null)
        {
            buildingButton.Onclick();
        }
    }
        
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
