using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    public static float time_limit;
    public float time_uesd;
    public float height = 30.0f;
    public string popup_text;
    public TextMeshPro temp;
    public void SetText(string text)
    {
        popup_text = text;
    }
    // Start is called before the first frame update
    void Start()
    {
        temp = gameObject.GetComponent<TextMeshPro>();
        StartCoroutine(CountDown());
    }
    public float go_up_time = 0.5f, wait_time = 1.0f, dissipate_time = 1.0f;
    IEnumerator CountDown()
    {
        temp.text = popup_text;
        float begin_height = temp.transform.position.y;
        temp.transform.rotation = Quaternion.Euler(new Vector3(0, DataRecorder.view_angle, 0));
        

        for (int i = 0; i < 10; i++)
        {
            temp.transform.position += Vector3.up * (height - begin_height)/10;
            
            yield return new WaitForSeconds(go_up_time / 10);
        }
        yield return new WaitForSeconds(wait_time);
        for(int i = 10; i < 20; i++)
        {
            temp.alpha = (20 - i) * 0.05f;
            yield return new WaitForSeconds(dissipate_time/10);
        }
        Destroy(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
