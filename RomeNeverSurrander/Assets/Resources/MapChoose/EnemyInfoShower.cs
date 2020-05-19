using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class EnemyInfoShower : MonoBehaviour
{
    public List<string> story_str;
    public List<string> story_pic;
    public List<string> story_3d_pic;
    public void ParseText(string story_path)
    {
        //这个用来读取故事的信息
        //格式应该是这样的
        //图片路径
        //故事
        //空格
        //图片路径,如此往复
        //break结束
        TextAsset text = Resources.Load<TextAsset>(story_path);
        string temp = text.text;
        string[] temps = temp.Split('\n');
        Debug.Log(temps.Length);
        for(int i = 0; i < temps.Length; i++)
        {
            story_3d_pic.Add(temps[i++].Trim());
            story_pic.Add(temps[i++].Trim());
            story_str.Add("<color=#000000ff><b><size=32>" + temps[i++] + "</size></color></b>\n");//把第一句加进去
            while (temps[i].Trim().Length > 0)
            {
                Debug.Log(temps[i]);
                story_str[story_str.Count - 1] +=  temps[i++] + "\n";

            }
              
        }
        

    }
    public GameObject enemyInfoItem;
    public Button button;
    public List<Vector2> mins = new List<Vector2>() { new Vector2(0.0f, 0.3f), new Vector2(0.35f, 0.65f), new Vector2(0.70f, 1.0f)};
    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        ParseText("MapChoose/StoryPartInfo");
        Debug.Log("hello");
        for(int i = 0; i < 3; i++)
        {
            GameObject temp = Instantiate(enemyInfoItem, transform);
            temp.GetComponent<RectTransform>().anchorMin = new Vector2(mins[i][0], 0.2f);
            temp.GetComponent<RectTransform>().anchorMax = new Vector2(mins[i][1], 1);
            temp.GetComponent<InfoItemController>().SetContent(story_str[i]);
            temp.GetComponent<InfoItemController>().SetImage(story_pic[i]);
            temp.GetComponent<InfoItemController>().Set3DImage(story_3d_pic[i]);
        }
        button.transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonClick()
    {
        Hide();
    }
    GameObject panel;
    public void Hide()
    {
        animator.SetTrigger("Hide");
        
    }
    public void Show()
    {
        animator.SetTrigger("Show");
    }
    public void BeginButton()
    {
        SceneManager.LoadScene("FightScene");
    }
}
