using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    static System.Random m_random = new System.Random();

    public static void DestroyChilds(Transform a_transform)
    {
        for (int i = a_transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(a_transform.GetChild(i).gameObject);
        }
    }

    public static bool RandomBool( int probability, System.Random r = null)
    {
        if (r == null)
        {
            r = m_random;
        }

        int v = r.Next(0, 100);
        bool result = v < probability;
        return result;
    }

    public static float RandomFloat(float min, float max, System.Random r = null)
    {
        if(r == null)
        {
            r = m_random;
        }
        return (float)r.NextDouble() * (max - min) + min;
    }

    public static int SignWithZero(float a_value, float a_epsilon = 0)
    {
        return a_value >= -a_epsilon && a_value <= a_epsilon ? 0 : (int)Mathf.Sign(a_value);
    }

    public static float NextGaussianDouble(float a_mean, float a_stdDev)
    {
        float u, v, S;
        
        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        } while (S >= 1.0f || S == 0f);

        float fac = Mathf.Sqrt((-2.0f * Mathf.Log(S) / S));
        return a_mean + a_stdDev * u * fac;
    }


}
