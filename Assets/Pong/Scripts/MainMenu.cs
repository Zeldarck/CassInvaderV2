using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu {
    int m_score = 3;
    [SerializeField]
    InputField m_scoreInput;

    void Start () {
        SetScoreMax();
        m_scoreInput.text = m_score + "";
    }

    void Update () {
        m_scoreInput.text = m_score + "";
    }

    public void Increment()
    {
        m_score++;
        SetScoreMax();
    }

    public void Decrement()
    {
        m_score--;
        m_score = Mathf.Max(m_score, 1);
        SetScoreMax();
    }

    public void UpdateScore(string a_value)
    {
        m_score = a_value == "" ? 0 : int.Parse(a_value);
        m_score = Mathf.Max(m_score, 1);
        SetScoreMax();
    }

    public void WithObject(bool a_value)
    {
        GameManager.m_withObject = a_value;
    }

    public void SetDifficulty(int a_difficulty)
    {
        GameManager.m_difficulty = (DIFFICULTY)a_difficulty;
    }


    public void SetScoreMax()
    {
        GameManager.m_scoreMax = m_score;
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
