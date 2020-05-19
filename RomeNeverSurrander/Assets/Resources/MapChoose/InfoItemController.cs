using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoItemController : MonoBehaviour
{
    public Image image;
    public Image image_3d;
    public TextMeshProUGUI text;
    public float a;
    public Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar.value = 1;
    }
    public void SetContent(string str)
    {
        text.text = str;
    }
    public void SetImage(string path)
    {
        Debug.Log(path);
        image.sprite = Resources.Load<Sprite>(path);
        
    }
    public void Set3DImage(string path)
    {
        image_3d.sprite = Resources.Load<Sprite>(path);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
