//************************************************/
//* @file  :BackColor.cs
//* @brief :背景色関連
//* @date  :2017/06/17
//* @author:S.Katou
//************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BackColor : MonoBehaviour {

    SpriteRenderer m_sr;

    private int m_redScore = 0;
    private int m_blueScore = 0;
    public ScoreController m_score;
    public Score2Controller m_score2;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start () {
        m_sr = GetComponent<SpriteRenderer>();

        this.UpdateAsObservable()
            .Subscribe(x =>
            {
                float red;
                float green;
                float blue;

                m_redScore = m_score2.Getscore();
                m_blueScore = m_score.Getscore();
                if (m_blueScore > m_redScore)
                {
                    red = (float)m_redScore / (float)m_blueScore;
                    blue = 1.0f;
                    green = red;
                }
                else if (m_blueScore < m_redScore)
                {
                    red = 1.0f;
                    blue = (float)m_blueScore / (float)m_redScore;
                    green = blue;
                }
                else
                {
                    red = 1.0f;
                    blue = 1.0f;
                    green = 1.0f;
                }

                m_sr.color = new Color(red, green, blue, 1.0f);
            });
	}
}
