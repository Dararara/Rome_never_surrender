using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SenceSwitch : MonoBehaviour
{
    public string targetScene;
    public string targetDialog = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchScene()
    {
        DataRecorder.kill_num = 0;
        DataRecorder.PlayMode = 1;
        DataRecorder.total_money = 100;
        SceneManager.LoadScene(targetScene);
    }
}
