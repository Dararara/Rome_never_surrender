using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Text text;

    // Update is called once per frame
    void Update()
    {
        BuildingControl.UpdateAll();

        //text.text = DataRecorder.total_money.ToString();
    }
}
