using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class StatusShower : MonoBehaviour
{
    public TextMeshProUGUI money_text;
    // Start is called before the first frame update
    void Start()
    {
        money = DataRecorder.total_money;
        money_text.text = "Money: " + money.ToString();
    }
    public int money;
    // Update is called once per frame
    void Update()
    {
        if(money == DataRecorder.total_money)
        {
            return;
        }

        money = DataRecorder.total_money;
        money_text.text = "Money: " + money.ToString();
    }
    
}
