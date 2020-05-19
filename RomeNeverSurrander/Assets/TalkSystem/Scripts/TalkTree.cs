using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Node
{
    public string name;
    public List<string> contents;
    public List<string> child_name;
    public int self_id;
    public string char_image;
    public string background_image;
    public float[] color = {255, 255, 255, 255};
    public float[] background_color = { 255, 255, 255, 100};
    public string sound;
   
    public string char_name;
    public Node()
    {
        contents = new List<string> { "hello", "world" };
        child_name = new List<string> { "my", "god", "!" };
        name = "hello";
        self_id = 1;
    }
    public Node(string name, List<string> contents, List<string> child_name, int self_id, string char_name = null,
        string char_image = null, string background_image = null, float[] color = null, float[] background_color = null, string sound = null)
    {
        this.name = name;
        this.contents = contents;
        this.child_name = child_name;
        this.self_id = self_id;
        this.char_image = char_name;
        this.char_image = char_image;
        this.background_image = background_image;
        if(color != null)
        {
            for (int i = 0; i < color.Length && i < 4; i++)
            {
                this.color[i] = Math.Min(255, Math.Max(0, color[i]));
            }
        }
        if(this.char_image == null)
        {
            //如果没图片的话，人物部分应为纯透明
            this.color[3] = 0;
        }
        if (background_color != null)
        {
            for (int i = 0; i < background_color.Length && i < 4; i++)
            {
                this.background_color[i] = Math.Min(255, Math.Max(0, background_color[i]));
            }
        }
        if(this.background_image == null)
        {
            this.background_color[3] = 0;
        }


        this.sound = sound;
    }
}
public class TalkTree
{
    public List<Node> tree;
    public Dictionary<string, Node> tree_dic;
    public TalkTree()
    {
        tree = new List<Node>();
        tree_dic = new Dictionary<string, Node>();
    }

    public void AddNode(string name, List<string> contents, List<string> child_name = null, string char_name = null,
        string char_image = null, string background_image = null,float[] color = null, float[] background_color = null, string sound = null)
    {
        if (tree_dic.ContainsKey(name))
        {
            Debug.Log("key already exist");
            return;
        }
        List<string> new_contents = new List<string>(contents);
        List<string> new_child_name = new List<string>(child_name);
        Node temp_node = new Node(name, new_contents, new_child_name, tree.Count, char_name,
            char_image, background_image, color, background_color, sound);
        tree.Add(temp_node);
        tree_dic.Add(name, temp_node);
    }
    public Node GetNode(string node_name)
    {
        if (tree_dic.ContainsKey(node_name))
        {
            return tree_dic[node_name];
        }
        else
        {
            return null;
        }
        
    }
    static void main()
    {
        Console.WriteLine("Hello World!");
        TalkTree tree = new TalkTree();
        tree.AddNode("a", new List<string> { "I am a", "nice to meet you" }, new List<string> { "b", "c", "d" });
        tree.AddNode("b", new List<string> { "I am b", "nice to meet you" });
        tree.AddNode("c", new List<string> { "I am c", "nice to meet you" });
        tree.AddNode("d", new List<string> { "I am d", "nice to meet you" });
        Console.WriteLine(tree.GetNode("a").contents[0]);

        Console.Read();
    }
}



