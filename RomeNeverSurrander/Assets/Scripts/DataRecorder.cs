using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataRecorder
{
    public static float view_angle = 90.0f;
    public static int total_money = 1000;
    public static int PlayMode = 1;
    public static string DisplayTitle = "凯尔特的反击";
    public static string root_dir = "Texts/";
    public static string next_scene = "";
    public static int kill_num = 0;
    public static float scene_rotate_speed = 10.0f;
    public static float play_speed = 1.0f;
    public static void Reset_Data()
    {
        total_money = 0;
        view_angle = 90.0f;
        kill_num = 0;
        PlayMode = 1;
    }
    public static string GetNextScene()
    {
        //next scene是一次性的，用完就删
        string temp = next_scene;
        next_scene = "";
        return temp;
    }
    public static int AddMoney(int money)
    {
        total_money += money;
       
        return total_money;
    }
    public static TalkTree LoadTalkTree()
    {
        TalkTree tree = new TalkTree();
        //tree.AddNode("a", new List<string> { "I am a", "nice to meet you" }, new List<string> { "a", "b", "c", "d" }, char_image: "Pictures/komaeda1", background_image: "Pictures/kon", sound: "Sounds/感情");
        //tree.AddNode("b", new List<string> { "I am b", "nice to meet you" }, new List<string> { "a", "b", "c", "d" });
        //tree.AddNode("c", new List<string> { "I am c", "nice to meet you" }, new List<string> { "a", "b", "c", "d" }, char_image: "Pictures/komaeda1", background_image: "Pictures/kon", sound: "Sounds/感情");
        //tree.AddNode("d", new List<string> { "I am d", "nice to meet you" }, new List<string> { "a", "b", "c", "e" }, sound: "Sounds/日向");
        //tree.AddNode("e", new List<string> { "到了说再见的时候了，溜了溜了" });
        TextAsset textAsset = Resources.Load<TextAsset>(root_dir + DisplayTitle);
        string[] contents = textAsset.text.Split('\n');

        string name;
        List<string> talk_contents = new List<string>();
        List<string> choices = new List<string>();
        string char_name, char_image, background_image, sound;

        float[] color, background_color = new float[3];
        //我们暂时不处理color的问题了
        for(int i = 0; i < contents.Length; i++)
        {
            contents[i] = contents[i].Trim();
        }

        for (int i = 0; i < contents.Length; )
        {
            talk_contents.Clear();
            choices.Clear();

            name = contents[i++];
            Debug.Log(name);
            Debug.Log("hello" + i);
            if(name.Length < 1)
            {
                break;
            }

            for (; i < contents.Length;)
            {
                Debug.Log(contents[i]);
                if(contents[i].Length > 0)
                {
                    talk_contents.Add(contents[i++]);
                    
                }
                else
                {
                    i++;
                    break;
                }
            }
            if(i >= contents.Length)
            {
                break;
            }

            if(contents[i] == "null")
            {
                i++;
            }
            else
            {
                string[] chos = contents[i++].Split(';');
                for (int j = 0; j < chos.Length; j++)
                {
                    choices.Add(chos[j]);

                }
            }
            
            //挨个读取名字，角色图片，背景图片
            if(contents[i] == "null")
            {
                char_name = null;
            }
            else
            {
                char_name = contents[i];
            }
            i++;

            if (contents[i] == "null")
            {
                char_image = null;
            }
            else
            {
                char_image = contents[i];
            }
            i++;

            if (contents[i] == "null")
            {
                background_image = null;
            }
            else
            {
                background_image = contents[i];
            }
            i++;

            //颜色直接没有,到时候再处理吧
            color = null;
            i++;
            background_color = null;
            i++;

            //声音路径
            if (contents[i] == "null")
            {
                sound = null;
            }
            else
            {
                sound = contents[i];
            }
            i += 2;

            Debug.Log(talk_contents[0]);
            tree.AddNode(name, talk_contents, choices, char_name, char_image, background_image, color, background_color, sound);
            Debug.Log("hello" + i);
            Debug.Log("display " + tree.tree.Count);

        }
       

        return tree;
    }



}
