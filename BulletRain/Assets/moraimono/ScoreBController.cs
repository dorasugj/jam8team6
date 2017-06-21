using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBController : MonoBehaviour
{
    private int score = 0;          //得点
    private GameObject ScoreObj;    //得点オブジェクト
    public GameObject BulletObj;    //弾丸オブジェクト

    // Use this for initialization
    void Start()
    {
        //  得点の表示
        ScoreObj = GameObject.Find("ScoreB");
        ScoreObj.GetComponent<Text>().text = score.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        //  弾丸がA側の陣地に入ったら消滅させる
        if (BulletObj.transform.position.x <= -7.0f)
        {
            if (GameObject.Find("Bullet") != null)
            {
                Destroy(this.BulletObj);
                Debug.Log("Destroy");
            }
        }

        //  弾丸が存在しているとき
        if (GameObject.Find("Bullet") != null)
        {
            Debug.Log("Bullet Find!");
        }
        //  消滅したとき
        else
        {
            //  得点を追加する
            score++;
        }

        //  得点の表示
        ScoreObj.GetComponent<Text>().text = score.ToString("F0");
    }
}
