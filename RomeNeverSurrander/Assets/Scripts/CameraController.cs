using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera main_camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float speed = 10.0f;
    Vector3 last_position, current_position;
    private float time_offset = 0.1f, timer;
    public GameObject hero;
    public static Vector3 forward = new Vector3(0, 5.0f, -5);
    // Update is called once per frame
    void Update()
    {
        if(DataRecorder.PlayMode == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                timer = 0;
                last_position = Input.mousePosition;
                current_position = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                current_position = Input.mousePosition;
                timer += Time.deltaTime;
                if (timer > time_offset)
                {
                    Vector3 delta_position = current_position - last_position;
                    Vector3 forward = new Vector3(gameObject.transform.forward.x, 0.0f, gameObject.transform.forward.z).normalized;
                    Vector3 right = new Vector3(gameObject.transform.right.x, 0.0f, gameObject.transform.right.z).normalized;
                    gameObject.transform.position -= forward * delta_position.y * speed / Screen.height;
                    gameObject.transform.position -= right * delta_position.x * speed / Screen.width;
                    last_position = current_position;

                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                timer = 0;

            }
        }

        else if(DataRecorder.PlayMode == 1)
        {
            //Debug.Log(forward);
            forward = new Vector3(0, 5.0f, -5);
            forward = Matrix4x4.Rotate(Quaternion.Euler(0, DataRecorder.view_angle, 0)).MultiplyVector(forward);
            transform.position = hero.transform.position + forward; 
        }
        

        /*
        float vertical_input = Input.GetAxis("Vertical");
        if (vertical_input != 0)
        {

            Vector3 forward = new Vector3(gameObject.transform.forward.x, 0.0f, gameObject.transform.forward.z).normalized;
            gameObject.transform.position += forward * Time.deltaTime * vertical_input * speed;
        }
        float horizontal_input = Input.GetAxis("Horizontal");
        if(horizontal_input != 0)
        {
            Vector3 forward = new Vector3(gameObject.transform.forward.x, 0.0f, gameObject.transform.forward.z).normalized;
            gameObject.transform.position += gameObject.transform.right * Time.deltaTime * horizontal_input * speed;
        }
          */  

        
    }
}
