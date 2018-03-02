using UnityEngine;
using System.Collections;

public class SideWalls : MonoBehaviour
{
    [SerializeField]
    PLAYER m_playerMarkPoint;
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<Collider2D>().CompareTag("Ball"))
        {
            GameManager.Score(m_playerMarkPoint);
            GameManager.RestartRoundStatic();
        }
    }
}