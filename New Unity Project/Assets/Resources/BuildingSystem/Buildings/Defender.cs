﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Building
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject shoot_target;
    public float attack_range = 3f;
    // Update is called once per frame

    void SeekTarget()
    {
        if(shoot_target != null)
        {
            RaycastHit hit;
            //int layerMash = 1 << 8;
            if (Physics.Raycast(transform.position, shoot_target.transform.position - transform.position, out hit, Vector3.Distance(transform.position, shoot_target.transform.position), LayerMask.GetMask("Collider")))
            {
                //Debug.Log("碰撞对象：" + hit.collider.name);
                shoot_target = null;
                return;
            }
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortest = attack_range * 10f;
        GameObject prob_target = null;
        for (int i = 0; i < enemies.Length; i++)
        {
            float dis = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (dis < shortest)
            {
                shortest = dis;
                prob_target = enemies[i];
            }
        }
        if (shortest < attack_range)
        {
            shoot_target = prob_target;
        }

        if (shoot_target != null)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, shoot_target.transform.position - transform.position, out hit, Vector3.Distance(transform.position, shoot_target.transform.position), LayerMask.GetMask("Collider"))) { 
                //Debug.Log("碰撞对象：" + hit.collider.name);
                shoot_target = null;
                return;
            }
        }
    }


    float rotate_y = 180f;
    void LockOnTarget()
    {
        if (shoot_target != null)
        {
            Vector3 dir = shoot_target.transform.position - transform.position;
            rotate_y = Mathf.Abs(Quaternion.LookRotation(dir).eulerAngles.y - transform.rotation.eulerAngles.y);

            Vector3 rotate = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime).eulerAngles;
            transform.rotation = Quaternion.Euler(0, rotate.y, 0);
        }
    }


    public GameObject bullet_prefab;
    public GameObject shoot_point;
    public float shoot_count_down;
    public float shoot_interval = 3f;
    bool InAttackRange()
    {

        return rotate_y < 20f;
    }
    void ShootTarget()
    {
       
        if (shoot_count_down > 0)
        {
            shoot_count_down -= Time.deltaTime;
        }
        if (!InAttackRange())
        {
            return;
        }

        if (shoot_target != null && shoot_count_down <= 0.1f)
        {
            //shoot!
            shoot_count_down = shoot_interval;
            Debug.Log("shoot");
            GameObject bullet = Instantiate(bullet_prefab, shoot_point.transform.position, shoot_point.transform.rotation);
            bullet.GetComponent<Bullet>().SetTarget(shoot_target);
        }
    }

    void Update()
    {
        SeekTarget();
        LockOnTarget();
        ShootTarget();
    }
}