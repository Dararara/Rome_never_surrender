using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargets
{
    public static List<Vector2> targets = new List<Vector2>{new Vector2(31.5f, 6.0f),
        new Vector2(30f, 22f), new Vector2(21.5f, 22.5f), new Vector2(16f, 15f)};
    public static Vector3 GetIndexOf(int i)
    {
        return new Vector3(targets[i].x, 0, targets[i].y);
    }
}
