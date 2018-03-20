using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {

    public static void DestroyChilds(Transform a_transform)
    {
        for (int i = a_transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(a_transform.GetChild(i).gameObject);
        }
    }

    public static bool RandomBool(System.Random r, int probability)
    {
        int v = r.Next(0, 100);
        bool result = v < probability;
        return result;
    }

    public static float RandomFloat(System.Random r, float min, float max)
    {
        return (float)r.NextDouble() * (max - min) + min;
    }

    public static int SignWithZero(float a_value)
    {
        return a_value == 0 ? 0 : (int)Mathf.Sign(a_value);
    }

}
