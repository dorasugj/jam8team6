//************************************************/
//* @file  :Bullet.cs
//* @brief :弾関連
//* @date  :2017/06/17
//* @author:S.Katou
//************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Bullet : MonoBehaviour
{
    //移動量
    private float m_spd;

        //移動用
    private Rigidbody2D m_rb;

    private AudioSource m_audio;


    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        m_audio = GetComponent<AudioSource>();

        m_rb = GetComponent<Rigidbody2D>();

        //移動
        this.UpdateAsObservable()
            .Subscribe(x =>
            {
                m_rb.velocity = new Vector2(m_spd, 0);
            });

        //画面端に出たら消滅
        this.UpdateAsObservable()
            .Where(x => this.transform.position.x < -10 || this.transform.position.x > 10)
            .Subscribe(x =>
            {
                var obj = GameObject.Find("Player1") as GameObject;

                if (this.transform.position.x < 0)
                {
                    if (obj!=null)
                    {
                        //左に得点加算
                        obj.GetComponent<Player>().Score2();
                    }
                }
                else
                {
                    if (obj != null)
                    {
                        //右に得点加算
                        obj.GetComponent<Player>().Score();
                    }
                }

                m_audio.Play();
                Destroy(gameObject);
            });
    }


    /// <summary>
    /// 当たったときの処理
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        //弾と当たったら消滅
        if (collision.transform.tag == "Bullet")
        {
            m_audio.Play();
            Destroy(this.gameObject);
        }
        //プレイヤーと当たったら消滅
        else if (collision.transform.tag == "Player")
        {
            m_audio.Play();
            Destroy(this.gameObject);
        }
    }


    /// <summary>
    /// プレイヤーから設定される用
    /// </summary>
    /// <param name="spd">速度</param>
    public void SetSpd(float spd)
    {
        m_spd = spd;
    }
}
