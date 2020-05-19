using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float speed = 5f;
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    // Update is called once per frame
    public GameObject bulletExplosion;
    public AliveItem alive;
    void TrackEnemy()
    {
        if(target == null)
        {
            //Destroy(Instantiate(bulletExplosion, transform.position, transform.rotation), 1f);
            Destroy(gameObject);
            return;
        }
        float dis = Vector3.Distance(target.transform.position, transform.position);
        if(dis < 2f)
        {
            

            target.TryGetComponent(out EnemyControl enemy);
            if (enemy != null)
            {
                enemy.GetHit(10);
            }
            else
            {
                target.TryGetComponent(out Building building);
                if(building != null)
                {
                    building.GetHit(10);
                }
            }


            Destroy(Instantiate(bulletExplosion, transform.position, transform.rotation), 1f);
            Destroy(gameObject);
            return;
        }
        else
        {
            Vector3 dir = target.transform.position - transform.position;
            dir = dir.normalized;
            dir.y = 0;
            transform.position += dir * Time.deltaTime * speed;
        }
    }
    void LockOnTarget()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;

            Vector3 rotate = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime).eulerAngles;
            transform.rotation = Quaternion.Euler(-90.0f, rotate.y, 0);
        }
    }
    void Update()
    {
        TrackEnemy();
        LockOnTarget();
    }
}
