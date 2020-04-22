using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Talker : MonoBehaviour
{
    public TalkTree tree;
    public WindowControl wc;
    
    // Start is called before the first frame update
    void Start()
    {
        tree = DataRecorder.LoadTalkTree();
        StartCoroutine(temp());

    }
    IEnumerator temp()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("show the text");
        Debug.Log(tree.tree.Count);
        for(int i = 0; i < tree.tree.Count; i++)
        {
            Debug.Log(tree.tree[i].name);
            Debug.Log(tree.tree[i].contents[0]);
        }
        wc.Talk(tree.tree[0], this);
    }
    public void GetBack(string name, int choice)
    {
        if(choice == -1)
        {
            //这个系统是需要选择来推进的
            //即使只有一个选择。。。
            //只有最后一个才没有选择

            //wc.Talk(null, this);
            Debug.Log("get null");
            if(DataRecorder.next_scene.Length > 0)
            {
                SceneManager.LoadScene(DataRecorder.GetNextScene());
            }
            return;
        }
        Debug.Log(name);
        Debug.Log(tree.GetNode(name).child_name[choice]);
        wc.Talk(tree.GetNode(tree.GetNode(name).child_name[choice]), this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
