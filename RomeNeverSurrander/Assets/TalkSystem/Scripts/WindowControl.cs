using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface TextShowObserver
{
    void ShowFinish();
}

public class WindowControl : MonoBehaviour, TextShowObserver
{
    public Button[] buttons = new Button[4];
    public Image background;
    public Image character;
    public AudioSource audio_source;
    public Sprite sprite;
    public bool wait_for_click;
    // Start is called before the first frame update
    public Node talk_node;
    private string audio_path = null;
    public void ShowFinish()
    {
        wait_for_click = true;
    }
    void Start()
    {
        
        buttons = gameObject.GetComponentsInChildren<Button>();
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        wait_for_click = false;
        
    }
    public Talker talker;
    public TextShow textshow;
    public int sentence_index;
    public string text_name;
    
    // Update is called once per frame
    void Update()
    {
        if (wait_for_click)
        {
            //如果接下来要展示的是选项，就直接展示，不等待用户点击，如果要跳转，就等待用户点击
            if(sentence_index >= talk_node.contents.Count && talk_node.child_name != null && talk_node.child_name.Count > 0)
            {
                //textshow.Display_text("ohhh", this);
                wait_for_click = false;
                ShowButtons(talk_node.child_name);
                
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    
                    if(sentence_index >= talk_node.contents.Count && (talk_node.child_name == null || talk_node.child_name.Count == 0))
                    {
                        wait_for_click = false;
                        talker.GetBack(name, -1);
                    }
                    else
                    {
                        wait_for_click = false;
                        ShowTexts(talk_node.contents[sentence_index]);
                        sentence_index += 1;
                    }
                }
            }
        }
        



    }
    List<string> buttons_triggers;
    public void Onclick1()
    {
        Debug.Log("hide buttons");
        buttons_triggers = new List<string> {"be_clicked", "click_down", "click_down", "click_down"};
        HideButtons(buttons_triggers, 0);
    }
    public void Onclick2()
    {
        Debug.Log("hide buttons");
        buttons_triggers = new List<string> { "click_down", "be_clicked", "click_down", "click_down" };
        HideButtons(buttons_triggers, 1);
    }
    public void Onclick3()
    {
        Debug.Log("hide buttons");
        buttons_triggers = new List<string> { "click_down", "click_down", "be_clicked", "click_down" };
        HideButtons(buttons_triggers, 2);
    }
    public void Onclick4()
    {
        Debug.Log("hide buttons");
        buttons_triggers = new List<string> { "click_down", "click_down", "click_down", "be_clicked" };
        HideButtons(buttons_triggers, 3);
        
    }
    public void hello()
    {
        Debug.Log("hello");
    }

    private void HideButtons(List<string> button_triggers = null, int click_button_id = -1)
    {
        Debug.Log("do");
        StartCoroutine(HideButton(new List<Button>(buttons), button_triggers, click_button_id));


    }
    IEnumerator HideButton(List<Button> buttons_show, List<string> tris, int click_button_id = -1)
    {
        for(int i = 0; i < buttons_show.Count; i++)
        {
            buttons_show[i].GetComponent<Animator>().SetTrigger(tris[i]);
        }
        
        yield return new WaitForSeconds(0.3f);
        for(int i = 0; i < buttons_show.Count; i++)
        {
            buttons_show[i].gameObject.SetActive(false);
        }
        if(click_button_id != -1)
        {
            talker.GetBack(text_name, click_button_id);
        }
    }
    

    private void ShowButtons(string[] choices)
    {
        ShowButtons(new List<string>(choices));
    }
    public void ShowButtons(List<string> choices)
    {
        int i = 0;
        Debug.Log(choices.Count);
        for (; i < choices.Count && i < buttons.Length; i++)
        {
           Debug.Log("button: " + choices[i] + buttons[i].gameObject);
           buttons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = choices[i];
           buttons[i].gameObject.SetActive(true);
           
            
        }
        for (; i < buttons.Length; i++)
        {
            Debug.Log("set false");
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void ShowTexts(string str)
    {
        textshow.Display_text(str, this);
    }
    private void SetColor(Image image, float[] color)
    {
        float[] temp_color = { 255, 255, 255, 255};
        for(int i = 0; i < color.Length && i < 4; i++)
        {
            //Debug.Log(color[i]);
            temp_color[i] = color[i];
        }
        //Debug.Log(image);
        image.color = new Color(temp_color[0]/255f, temp_color[1]/255f, temp_color[2]/255f, temp_color[3]/255f);
    }
    public void HideWindow()
    {
        audio_source.Stop();
        talker = null;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        textshow.Clear_text();
        background.gameObject.SetActive(false);
    }
    public void WakeUp(Talker talk)
    {
        talker = talk;
    }
    public void Talk(Node talk_n, Talker t)
    {
        if (t == null || talk_n == null) 
        {
            //Debug.LogError("get null talk node or talker");
            return;
        }
        talker = t;
        wait_for_click = false;
        if(talk_n == null)
        {
            
            HideWindow();
            return;
        }
        for(int i = 0; i < talk_n.contents.Count; i++)
        {
            Debug.Log(talk_n.contents[i]);
        }


        this.talk_node = talk_n;
        sentence_index = 0;
        text_name = talk_n.name;
        if(talk_n.char_image != null)
        {
            character.sprite = Resources.Load<Sprite>(talk_n.char_image);
            
        }
        if (talk_n.background_image != null)
        {
            background.sprite = Resources.Load<Sprite>(talk_n.background_image);
        }
        SetColor(character, talk_n.color);
        SetColor(background, talk_n.background_color);


        if(talk_n.sound == null)
        {
            audio_path = null;
            audio_source.Stop();
            
        }
        else if(talk_n.sound != audio_path)
        {
            audio_source.Stop();
            audio_path = talk_n.sound;
            audio_source.clip = Resources.Load<AudioClip>(audio_path);
            audio_source.Play();
        }
        ShowTexts(talk_node.contents[sentence_index]);
        sentence_index += 1;
        Debug.Log("this is window control, get it");


    }


}
