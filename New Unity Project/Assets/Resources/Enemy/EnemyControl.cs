using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Vector3 target;
    public int target_index;
    // Start is called before the first frame update
    void Start()
    {
        target_index = 0;
        target = EnemyTargets.GetIndexOf(target_index);
        die = false;
    }
    public float speed = 2f;
    public float health = 1f;
    public bool die;
    public void GetHit(float demage)
    {
        if (die) return;
        health -= demage;
        Debug.Log("health remain: " + health);
        if(health < 0)
        {
            die = true;
            DataRecorder.kill_num++;
            Debug.Log("awsl");
            GetComponent<Animator>().SetTrigger("Die");
            Destroy(gameObject, 2f);
        }
        else
        {
            GetComponent<Animator>().SetTrigger("GetHit");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(health < 0)
        {
            return;
        }
        Vector3 direct = target - transform.position;
        direct.y = 0;
        float dis = direct.magnitude;
        transform.position += direct.normalized * speed * Time.deltaTime;
        gameObject.transform.LookAt(direct + transform.position);
        if (dis < 0.2f)
        {
            //gameObject.GetComponent<Animator>().SetTrigger("StopForAWhile");
            target_index++;
            if(target_index >= EnemyTargets.targets.Count)
            {
                Destroy(gameObject);
                return;
            }
            target = EnemyTargets.GetIndexOf(target_index);
        }
    }
}
