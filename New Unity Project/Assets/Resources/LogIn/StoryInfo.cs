using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryInfo : MonoBehaviour
{
    public string text;
    public string imagePath;
    public Scrollbar scrollbar;
    public Button button;
    public TextMeshProUGUI button_text;
    public string title;
    public void SetTitle(string title)
    {
        this.title = title;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        text = "???\n???";
        imagePath = "Pictures/lancer";
        button_str = "锐意发开中";
        button_enable = false;
    }
    public int id;
    public void SetID(int id)
    {
        this.id = id;
    }
    void Start()
    {

    }
    public void SetText(string text)
    {
        this.text = text;
    }
    public void SetImagePath(string image_path)
    {
        imagePath = image_path;
    }
    public string button_str;
    public bool button_enable;
    public void SetButtonText(string str)
    {
        button_str = str;
    }
    public void SetButtonEnable(bool enable)
    {
        button_enable = enable;
    }

    public TextMeshProUGUI text_part;
    public Image image_part;
    public void LoadContent()
    {
        Sprite sprite = Resources.Load<Sprite>(imagePath);
        image_part.sprite = sprite;
        text_part.text = text;
        scrollbar.value = 1;
        if (!button_enable)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
        button_text.text = button_str;


    }
    public RotateStory rotateStory;

    public void Onclick()
    {
        rotateStory.GetClick(id);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public static string next_scene = "DialogScene";
    public void OnClick()
    {
        Debug.Log(title);
        DataRecorder.next_scene = "FightScene";
        DataRecorder.DisplayTitle = title;
        SceneManager.LoadScene(next_scene);
    }


}
