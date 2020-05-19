using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetHelper: MonoBehaviour
{
    public static bool collider_exist(Transform transform, GameObject shoot_target)
    {
        if (shoot_target == null) return false;
        if (transform == null) return false;
        //检测transform和shoot target之间有没有障碍物
        RaycastHit hit;
        //int layerMash = 1 << 8;
        if (Physics.Raycast(transform.position, shoot_target.transform.position - transform.position, out hit, Vector3.Distance(transform.position, shoot_target.transform.position) - 3, LayerMask.GetMask("Collider")))
        {
            Debug.Log(transform.position);
            Debug.Log(shoot_target.transform.position);
            //Debug.Log("碰撞对象：" + hit.collider.name);
            return true;
        }

        return false;
    }
    public static float big_float = 9999999.9f;
    public static GameObject SearchTargets(GameObject[] targets, Transform transform, float max_distance)
    {
        //搜寻离transform距离最近的带有对应tag的gameobject
        //如果超过距离,就直接返回null
        //只搜寻带Alive_Item的对象
        //顺便还会帮忙检查一下碰撞
        float shortest = max_distance;//只有范围内的才考虑
        GameObject prob_target = null;
        for(int i = 0; i < targets.Length; i++)
        {
            
            float dis = Vector3.Distance(transform.position, targets[i].transform.position);
            if(dis < shortest && targets[i].GetComponent<Alive_Item>().IsAlive() && !collider_exist(transform, targets[i]))
            {
                Debug.Log("ohhh");
                shortest = dis;
                
                prob_target = targets[i];
                Debug.Log(prob_target);
            }
        }
        return prob_target;
    }
    public static bool IsPointerOverUIObject()
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
