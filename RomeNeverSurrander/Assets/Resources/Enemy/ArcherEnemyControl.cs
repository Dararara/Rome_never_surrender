using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemyControl : EnemyControl
{
    public float shoot_range = 10;
    public void SeekTarget()
    {
        //如果去世了,就不再瞄准了
        if(shoot_target != null && !shoot_target.GetComponent<Alive_Item>().IsAlive())
        {
            shoot_target = null;
        }
        else
        {
            //int layerMash = 1 << 8;
            if(TargetHelper.collider_exist(transform, shoot_target))
            {
                Debug.Log("ohhhhh");
                shoot_target = null;
                return;
            }
        }
        //挨个检测tag为building的物体的距离
        try
        {
            shoot_target = TargetHelper.SearchTargets(GameObject.FindGameObjectsWithTag("Building"), transform, shoot_range);
        }
        catch
        {

        }
        

    }

    public float shoot_count_down = 10;
    public float shoot_interval = 10;
    public GameObject bullet_prefab;
    public GameObject shoot_point;
    public void ShootTarget()
    {
        if(shoot_count_down > 0)
        {
            shoot_count_down -= Time.deltaTime;
        }
        if(rotate_y > 30f)
        {
            return;
        }
        if(shoot_target != null && shoot_count_down <= 0)
        {

            StartCoroutine(shoot());
        }
    }
    IEnumerator shoot()
    {
        animator.SetTrigger("Shoot");
        shoot_count_down = shoot_interval;
        yield return new WaitForSeconds(1.5f);
        Debug.Log("huhuhu");
        GameObject bullet = Instantiate(bullet_prefab, shoot_point.transform.position, shoot_point.transform.rotation);
        bullet.GetComponent<Bullet>().SetTarget(shoot_target);
    }
    

    public GameObject shoot_target;
    public override void Update()
    {
        if (health < 0)
        {
            return;
        }
        SeekTarget();
        ShootTarget();
        if(shoot_target == null)
        {
            //如果没有射击目标,就继续移动
            animator.SetFloat("speed", 1);
            KeepMoving();
        }
        else
        {
            animator.SetFloat("speed", 0);
            LockOnTarget();
        }
        
    }
    public float rotate_y = 180f;

    void LockOnTarget()
    {
        //更新角度
        if (shoot_target != null)
        {
            transform.rotation = lock_on(ref shoot_target, transform, ref rotate_y);
        }
    }
    public static Quaternion lock_on(ref GameObject shoot_target, Transform transform, ref float rotate_y)
    {
        Vector3 dir = shoot_target.transform.position - transform.position;
        rotate_y = Mathf.Abs(Quaternion.LookRotation(dir).eulerAngles.y - transform.rotation.eulerAngles.y);

        Vector3 rotate = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime).eulerAngles;
        return Quaternion.Euler(0, rotate.y, 0);
    }
}
