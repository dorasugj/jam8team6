using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public int score = 0;           //得点
    private GameObject ScoreObj;    //得点オブジェクト

    // Use this for initialization
    void Start () {
        //  得点の表示
        ScoreObj = GameObject.Find("ScoreA");
        ScoreObj.GetComponent<Text>().text = Getscore().ToString("F0");
    }

    // Update is called once per frame
    void Update () {
        //  得点の表示
        ScoreObj.GetComponent<Text>().text = Getscore().ToString("F0");
    }

    //  得点の取得
    public int Getscore()
    {
        return score;
    }

    public void ScorePuls()
    {
        score++;
    }
}
