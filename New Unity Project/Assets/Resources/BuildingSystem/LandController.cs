using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LandController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string hello;
    public Vector2 position;
    public void OnMouseDown()
    {
        if (IsPointerOverUIObject())
        {
            return;
        }
        StartCoroutine(CheckClick());
        
    }
    IEnumerator CheckClick()
    {
        //点击鼠标一般两秒差不多？
        yield return new WaitForSeconds(0.2f);
        if (!Input.GetMouseButton(0))
        {
            Debug.Log(hello);
            BuildingControl.GetInstance().SetPosition(position);
            BuildingPackController packController = GameObject.Find("BuildingPack").GetComponent<BuildingPackController>();
            Debug.Log("show show show");
            packController.is_show = false;
            packController.ClickShow();
        }
    }


    private bool IsPointerOverUIObject()
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    
}
