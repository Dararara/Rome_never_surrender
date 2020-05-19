using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oruga : EnemyControl
{
    public AudioSource audioSource;
    public override void Start()
    {
        base.Start();
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audioSource.Stop();
        attack_view = 90;
        relife = 12;
    }
    private void OnDestroy()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
        
    }
    public float max_health = 100;
    public int relife = 12;
    public override void GetHit(float damage)
    {
        if (die) return;
        health -= damage;
        if(health < 0)
        {
            if(relife > 0)
            {
                relife--;
                health = max_health;
                animator.SetTrigger("Die");
                animator.SetTrigger("Relife");

            }
            else
            {
                die = true;
                health = max_health;
                animator.SetTrigger("Die");
                Destroy(gameObject, 2f);
            }
        }
        else
        {
            animator.SetTrigger("GetHit");
        }
    }
    public override void Update()
    {
        base.Update();
        //团长会对面前的建筑物造成大量伤害
        //没有谁能让团长停下来


        Collider[] collider_array = Physics.OverlapSphere(gameObject.transform.position, 1.0f, LayerMask.GetMask("Building"));
        for(int i = 0; i < collider_array.Length; i++)
        {
            Vector3 v3 = collider_array[i].gameObject.transform.position - gameObject.transform.position;
            float angle = Vector3.Angle(v3, gameObject.transform.forward);
            if(angle < attack_view)
            {
                collider_array[i].gameObject.GetComponent<Building>().GetHit(10.0f);
                
            }
        }
    }
}
