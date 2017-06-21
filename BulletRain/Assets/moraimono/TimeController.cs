using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

    float timeCount = 10.0f;    //制限時間
    GameObject timeObj;            //時間オブジェクト

	// Use this for initialization
	void Start () {
        //  始めの時間の表示
        timeObj = GameObject.Find("Time");
        timeObj.GetComponent<Text>().text = timeCount.ToString();

	}

	// Update is called once per frame
	void Update () {
        //  制限時間を減らす
        if (timeCount >= 0.0f)
        {
            timeCount -= 0.015f;
        }
        //  現在の制限時間を表示
        timeObj.GetComponent<Text>().text = timeCount.ToString("F0");
    }

    public bool IsEnd()
    {
        return !(timeCount >= 0.0f);
    }
}
