using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStory : MonoBehaviour
{
    public GameObject[] canvases;
    public StoryInfo[] storys;
    public Transform[] transforms;
    // Start is called before the first frame update
    public GameObject canvas;
    public List<string> titles = new List<string>();
    public List<string> contents = new List<string>();
    public List<string> enables = new List<string>();
    public List<string> image_paths = new List<string>();
    public List<string> button_text = new List<string>();
    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Login/stories");
        string txt = textAsset.text;
        string[] temp = txt.Split('\n');
        int count = 0;
        
        for(int i = 0; i < temp.Length; i++)
        {
            temp[i] = temp[i].Trim();
        }

        for (int i = 0; i < temp.Length; i++)
        {
            if(temp[i].Length > 0)
            {
                
                titles.Add(temp[i++]);
                image_paths.Add(temp[i++]);
                contents.Add("");
                for (; i < temp.Length; i++)
                {
                    
                    if(temp[i].Length > 0)
                    {
                        contents[count] += temp[i] + "\n";
                    }
                    else
                    {
                        i++;
                        break;
                    }
                }
                enables.Add(temp[i++]);
                button_text.Add(temp[i++]);
                count++;



            }
        }
        canvases = new GameObject[count];
        storys = new StoryInfo[count];
        transforms = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            GameObject g = Instantiate(canvas);
            transform.Rotate(0, -30f, 0);
            g.transform.parent = gameObject.transform;
            

            canvases[i] = g;
            storys[i] = canvases[i].GetComponent<StoryInfo>();
            
            transforms[i] = canvases[i].transform;
            storys[i].SetImagePath(image_paths[i]);
            storys[i].SetText(contents[i]);
            storys[i].SetID(-i + count - 1);
            storys[i].rotateStory = this;
            if(enables[i] == "enable")
            {
                storys[i].SetButtonEnable(true);
            }
            storys[i].SetButtonText(button_text[i]);
            storys[i].SetTitle(titles[i]);
            storys[i].LoadContent();
        }




        /*
        for (int i = 1; i <= childs; i++)
        {
            canvases[i] = gameObject.transform.GetChild(i-1).gameObject;
            storys[i] = canvases[i].GetComponent<StoryInfo>();
            storys[i].LoadContent();
            transforms[i] = canvases[i].transform;
            storys[i].SetID(i);
            storys[i].rotateStory = this;
        }*/

    }

    public void GetClick(int id)
    {
        if(id == 1)
        {
            //rotate to left
            for (int i = 0; i < storys.Length; i++)
            {
                storys[i].id--;
            }
            StartCoroutine(rotate(30f));
        }
        else if(id == -1)
        {
            //rotate to right
            for (int i = 0; i < storys.Length; i++)
            {
                storys[i].id++;
            }
            StartCoroutine(rotate(-30f));
        }
    }
    IEnumerator rotate(float angles)
    {
        for(int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.1f/ 3f);
            transform.Rotate(0, angles/30f, 0);
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
