using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingItemController : MonoBehaviour
{
    public Image itemImage;
    public Image priceImage;
    public Text price_text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPriceText(int price)
    {
        price_text.text = price.ToString();
    }
    public void SetImage(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
    public void SetPriceImage(Sprite sprite)
    {
        priceImage.sprite = sprite;
    }
}
