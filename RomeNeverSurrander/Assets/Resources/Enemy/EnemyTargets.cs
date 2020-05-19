using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargets
{
    public static List<List<Vector2>> targets = new List<List<Vector2>> {
        new List<Vector2>{new Vector2(31.5f, 6.0f),
            new Vector2(30f, 22f), new Vector2(21.5f, 22.5f), new Vector2(16f, 15f)},
        new List<Vector2>{new Vector2(1.0f, 1.0f), new Vector2(2.0f, 28.0f)}, 
        new List<Vector2>{new Vector2(1.0f, 1.0f), new Vector2(2.0f, 28.0f), new Vector2(28.0f, 28.0f), new Vector2(28.0f, 1.0f),
        new Vector2(1.0f, 1.0f), new Vector2(2.0f, 28.0f), new Vector2(28.0f, 28.0f), new Vector2(28.0f, 1.0f), new Vector2(16f, 15f)},

    };

    public static Vector3 GetIndexOf(int i, int target_id = 1)
    {
        Debug.Log(target_id + " " + i);
        return new Vector3(targets[target_id][i].x, 0, targets[target_id][i].y);
    }
}
