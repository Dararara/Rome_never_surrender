using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyControl : EnemyControl
{
  
    public override void GetHit(float demage)
    {
        if (die) return;
        health -= demage;
        Debug.Log("health remain: " + health);
        if (health < 0)
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
    public override void Update()
    {
        if (health < 0)
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
            if (target_index >= EnemyTargets.targets[target_id].Count)
            {
                die = true;
                health = -1;
                StartCoroutine(Victory());
                return;
            }
            target = EnemyTargets.GetIndexOf(target_index, target_id);
        }
        else
        {
            //会检测和building的碰撞,对building进行自杀式袭击
            Collider[] collider_array = Physics.OverlapSphere(gameObject.transform.position, 1.0f, LayerMask.GetMask("Building"));
            for(int i = 0; i < collider_array.Length; i++)
            {
                Vector3 v3 = collider_array[i].gameObject.transform.position - gameObject.transform.position;
                float angle = Vector3.Angle(v3, gameObject.transform.forward);
                if(angle < 90.0f)
                {
                    Debug.Log("attack!!!");
                    collider_array[i].gameObject.GetComponent<Building>().GetDamage(10.0f);
                    die = true;
                    health = -1;
                    StartCoroutine(Victory());
                    return;
                }
            }
        }

    }
    IEnumerator Victory()
    {
        GetComponent<Animator>().SetTrigger("victory");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
