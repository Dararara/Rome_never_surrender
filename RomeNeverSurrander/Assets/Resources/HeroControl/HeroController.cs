using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataRecorder.kill_num = 0;
    }
    float speed = 2.0f;
    public Vector3 forward;
    public Animator animator;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        /*
        if(DataRecorder.kill_num > 5)
        {
            DataRecorder.kill_num = 0;
            DataRecorder.PlayMode = 1;
            DataRecorder.total_money = 100;

            SceneManager.LoadScene("MapChooseScene");
        }*/


        if(DataRecorder.PlayMode == 1)
        {




            float x_input = variableJoystick.Horizontal * speed * Time.deltaTime;
            float y_input = variableJoystick.Vertical * speed * Time.deltaTime;
            if (x_input != 0 || y_input != 0)
            {
                forward = new Vector3(x_input, 0.0f, y_input);

                forward = Matrix4x4.Rotate(Quaternion.Euler(0, DataRecorder.view_angle, 0)).MultiplyVector(forward);
                animator.SetBool("Walking", true);
                animator.speed = 1.0f;
                gameObject.transform.LookAt(forward + gameObject.transform.position);
                gameObject.transform.position += forward;
            }
            else
            {
                animator.SetBool("Walking", false);
            }

            
            
            
        }

    }
    public void Rotate()
    {
        DataRecorder.view_angle -= 20.0f;
        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.LookAt(gameObject.transform);
    }

    public void Attack()
    {
        animator.SetTrigger("NormalHit");
        StartCoroutine(HitEnemy());
    }
    public float attack_damage = 5f;
    public float attack_range = 2f;
    public float attack_view = 30f;
    public float damage_interval = 0.1f;
    IEnumerator HitEnemy()
    {
        yield return new WaitForSeconds(damage_interval);
        Collider[] colliderArr = Physics.OverlapSphere(gameObject.transform.position, attack_range, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < colliderArr.Length; i++)
        {
            Vector3 v3 = colliderArr[i].gameObject.transform.position - gameObject.transform.position;
            float angle = Vector3.Angle(v3, gameObject.transform.forward);
            if (angle < attack_view)
            {
                Debug.Log("attack");
                colliderArr[i].gameObject.GetComponent<EnemyControl>().GetHit(attack_damage);

            }
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        
    }
    public void OnDown()
    {
        Debug.Log("down");
        return;
    }
    public void OnUp()
    {
        Debug.Log("On");
    }


}
