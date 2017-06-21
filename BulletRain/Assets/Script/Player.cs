//************************************************/
//* @file  :Player.cs
//* @brief :プレイヤー関連
//* @date  :2017/06/17
//* @author:S.Katou
//************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Player : MonoBehaviour
{

    //発射する弾
    [SerializeField]
    private GameObject m_bullet;

    //プレイヤーの番号
    [SerializeField]
    private int m_playerNumber;

    //プレイヤーの番号
    [SerializeField]
    private float m_spd;

    //上に移動するかどうか
    [SerializeField]
    private bool m_isMoveUp;

    //移動するかどうか
    //撃った直後少し動けなくなる
    private bool m_isMoved = true;

    //弾の発射速度
    [SerializeField]
    private float m_bulletSpd = 0.5f;

    //弾の発射間隔のカウント
    private float m_shootCount;

    //音再生用
    private AudioSource m_audio;


    public ScoreController m_score;
    public Score2Controller m_score2;

    public TimeController m_time;
    public void Score()
    {
        m_score.ScorePuls();
    }
    public void Score2()
    {
        m_score2.ScorePuls();
    }

    // Use this for initialization
    void Start()
    {
        m_audio = GetComponent<AudioSource>();

        //時間のカウント
        this.UpdateAsObservable()
            .Subscribe(x =>
            {
                m_shootCount += Time.deltaTime;
                //撃ってから少しして動ける
                if (m_shootCount > 0.2f)
                {
                    m_isMoved = true;
                }

                if ((m_time.IsEnd()))
                {
                    Destroy(gameObject);
                }
            });

        //弾を発射
        this.UpdateAsObservable()
            .Where(x => Input.GetButtonDown("Shoot" + m_playerNumber.ToString()))
            .Where(x => m_shootCount > 0.3f)
            .Subscribe(x =>
            {
                //弾の生成
                GameObject unit = Instantiate(m_bullet) as GameObject;

                //プレイヤーの番号で弾の位置、速度を反転
                int bulletNum = m_playerNumber % 2 == 0 ? -1 : 1;

                //弾の位置変
                unit.transform.position = this.transform.position + new Vector3(bulletNum * 1.5f, 0, 0);

                //弾の向きを合わせる
                unit.transform.rotation = Quaternion.AngleAxis(-90.0f * bulletNum,new Vector3(0.0f,0.0f,1.0f));

                //弾の速度設定
                Bullet bullet = unit.GetComponent<Bullet>();
                bullet.SetSpd((float)bulletNum* m_bulletSpd);


                //発射カウントをリセット
                m_shootCount = 0.0f;

                //少し動けない
                m_isMoved = false;

                m_audio.Play();
            });


        //上に移動する
        this.UpdateAsObservable()
            .Where(x => m_isMoveUp == true)
            .Where(x => m_isMoved == true)
            .Subscribe(x =>
            {
                transform.Translate(new Vector2(0, m_spd));

                if (transform.position.y > 3.9)
                {
                    m_isMoveUp = false;
                }
            });

        //下に移動する
        this.UpdateAsObservable()
            .Where(x => m_isMoveUp == false)
            .Where(x => m_isMoved == true)
            .Subscribe(x =>
            {
                transform.Translate(new Vector2(0, -m_spd));

                if (transform.position.y < -3.9)
                {
                    m_isMoveUp = true;
                }
            });
    }
}
