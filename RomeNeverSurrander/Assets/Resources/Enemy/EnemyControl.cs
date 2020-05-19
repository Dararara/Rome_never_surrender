using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour, Alive_Item
{
    public Animator animator;
    public bool IsAlive()
    {
        return !die;
    }
    public Vector3 target;
    public int target_index;
    // Start is called before the first frame update
    public virtual void Start()
    {
        target_index = 0;
        target = EnemyTargets.GetIndexOf(target_index, target_id);
        die = false;
        Debug.Log(target_id);
        animator = GetComponent<Animator>();
    }
    public float speed = 2f;
    public float health = 10f;
    public bool die;
    public int target_id = 0;
    public virtual void GetHit(float demage)
    {
        if (die) return;
        health -= demage;
        Debug.Log("health remain: " + health);
        if(health < 0)
        {
            die = true;
            DataRecorder.kill_num++;
            Debug.Log("awsl");
            animator.SetTrigger("Die");
            Destroy(gameObject, 2f);
        }
        else
        {
            animator.SetTrigger("GetHit");
        }
    }
    public float attack_view;
    // Update is called once per frame
    public virtual void Update()
    {
        //如果没血了,那就返回
        if (health < 0)
        {
            return;
        }
        //跑完之后可能就去世了,要用奇怪的方式判定自己有没有停下来...
        KeepMoving();
    }

    public void KeepMoving()
    {
        
        //向目标前进
        
        Vector3 direct = target - transform.position;
        direct.y = 0;
        float dis = direct.magnitude;
        transform.position += direct.normalized * speed * Time.deltaTime * DataRecorder.play_speed;
        gameObject.transform.LookAt(direct + transform.position);
        //更新目标
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
    }

    public IEnumerator Victory() {
        animator.SetTrigger("victory");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

}
