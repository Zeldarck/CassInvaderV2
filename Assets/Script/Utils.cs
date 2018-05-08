﻿using System.Collections;
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

    public static Vector2 ClampVector(Vector2 a_vector, Vector2 a_vectorMin, Vector2 a_vectorMax)
    {
        a_vector.Normalize();
        a_vectorMin.Normalize();
        a_vectorMax.Normalize();
        Vector2 res = a_vector;
        float current = (Vector2.SignedAngle(new Vector2(1,0), a_vector) + 360) % 360;
        float min = (Vector2.SignedAngle(new Vector2(1,0), a_vectorMin) + 360) % 360;
        float max = (Vector2.SignedAngle(new Vector2(1, 0), a_vectorMax) + 360) % 360;

        //see if there is a mean to avoid to calculate angle before know that
        if(min > max)
        {
            current = Vector2.SignedAngle(new Vector2(1, 0), a_vector) ;
            min = Vector2.SignedAngle(new Vector2(1, 0), a_vectorMin) ;
            max = Vector2.SignedAngle(new Vector2(1, 0), a_vectorMax) ;
        }

        if (current > max)
        {
            res = a_vectorMax;
        }else if (current < min)
        {
            res = a_vectorMin;
        }

         return res;
        //Seem to work only with 0 - 180 ?
        //return Vector2.Min(a_vectorMax, Vector2.Max(a_vectorMin, a_vector));

    }

    public static Vector2 ClampVector(Vector2 a_vector, float a_degreMin, float a_degreMax)
    {
        return ClampVector(a_vector, new Vector2(Mathf.Cos(Mathf.Deg2Rad * a_degreMin), Mathf.Sin(Mathf.Deg2Rad * a_degreMin)), new Vector2(Mathf.Cos(Mathf.Deg2Rad * a_degreMax), Mathf.Sin(Mathf.Deg2Rad * a_degreMax)));
    }



}
