using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwitch : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public BuildingPackController buildingPack;
    public BuildingInfoPanelController buildingInfoPanel;

    public GameObject Hero_panel;
    public GameObject building_panel;
    public void OnClick()
    {
        if(DataRecorder.PlayMode == 1)
        {
            
            StartCoroutine(CameraUp());
        }
        else if(DataRecorder.PlayMode == 0)
        {
            TerrainInfo.ChangePlaneState(false);
            building_panel.SetActive(false);
            StartCoroutine(CameraDown());

            
        }
    }
    IEnumerator CameraUp()
    {
        GameObject camera_obj = GameObject.Find("Main Camera") as GameObject;
        Camera camera = camera_obj.GetComponent<Camera>();
        

        Hero_panel.SetActive(false);
       
        building_panel.SetActive(true);
        CanvasGroup canvas = building_panel.GetComponent<CanvasGroup>();
        canvas.alpha = 0f; 
        buildingPack.is_show = true;
        buildingInfoPanel.is_show = true;
        buildingPack.ClickHide();
        buildingInfoPanel.QuickHide();

        
        

        DataRecorder.PlayMode = 0;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            
            camera_obj.transform.position += Vector3.up * 5f/ 50f;
        }
        canvas.alpha = 1f;
    }
    
    IEnumerator CameraDown()
    {

        GameObject camera_obj = GameObject.Find("Main Camera") as GameObject;
        Camera camera = camera_obj.GetComponent<Camera>();
        camera.cullingMask &= ~(1 << 4);
        GameObject hero = GameObject.Find("Hero");
        DataRecorder.PlayMode = 0;
        CameraController.forward = new Vector3(0, 5.0f, -5);
        CameraController.forward = Matrix4x4.Rotate(Quaternion.Euler(0, DataRecorder.view_angle, 0)).MultiplyVector(CameraController.forward);
        Vector3 dis = hero.transform.position + CameraController.forward - camera_obj.transform.position;
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);

            camera_obj.transform.position += dis / 50f;
        }
        DataRecorder.PlayMode = 1;
        Hero_panel.SetActive(true);
        camera.cullingMask |= ~(1 << 4);
    }


}
