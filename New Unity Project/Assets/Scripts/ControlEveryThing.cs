using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEveryThing : MonoBehaviour
{
    public Talker talker;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject window;
    public GameObject background;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            background.SetActive(true);
            window.GetComponent<WindowControl>().WakeUp(talker);
            window.GetComponent<WindowControl>().Talk(talker.tree.tree[0], talker);
        }
    }
}
