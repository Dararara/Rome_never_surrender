using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginFightClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public EnemyInfoShower infoShower;
    public string story_part_id;
    public void OnClick()
    {
        infoShower.Show();
        //SceneManager.LoadScene("FightScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
