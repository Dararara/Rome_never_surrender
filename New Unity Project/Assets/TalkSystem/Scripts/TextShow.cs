using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    public bool onDisplaying;
    TextMeshProUGUI text;
    string display_text;
    void Start()
    {
        time_per_char = 0.01f;
        onDisplaying = false;
        count_down = 0;
        text = gameObject.GetComponent<TextMeshProUGUI>();
        finish = true;
    }

    public float time_per_char = 0.05f;
    float count_down;
    int text_index;
    public bool finish;
    public bool Is_finish()
    {
        return finish;
    }
    // Update is called once per frame
    void Update()
    {
        if (onDisplaying && text_index < display_text.Length)
        {

            if(count_down < time_per_char)
            {
                count_down += Time.deltaTime;

            }
            else
            {
                count_down = 0;
                text_index += 1;
                text.text = display_text.Substring(0, text_index);
            }
            /*if (Input.GetMouseButtonDown(0))
            {
                text_index = display_text.Length;
                text.text = display_text.Substring(0, text_index);
            }*/
            if (text_index == display_text.Length)
            {
                //Debug.Log("展示结束");
            }
        }
        else if (!finish && text_index == display_text.Length)
        {
            finish = true;
            //  text.text = "done";
            text_obs.ShowFinish();

        }

               
    }
    public void Clear_text()
    {
        text.text = "";
        text.text = "";
    }
    TextShowObserver text_obs;
    public void Display_text(string str, TextShowObserver text_observer)
    {
        display_text = str;
        text_index = 0;
        count_down = 0;
        onDisplaying = true;
        finish = false;
        text_obs = text_observer;
        Debug.Log("this is text show, i will do it");
        
    }
}
