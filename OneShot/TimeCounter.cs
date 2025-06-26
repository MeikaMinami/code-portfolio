using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    public float countdownMinutes = 5.0f;  //５分間タイマー

    private float countdownSeconds;   //秒数

    [SerializeField]
    private TextMeshProUGUI TimertextMeshPro;  //タイマーの残り時間を表示するテキストメッシュプロ

    [SerializeField]
    private Image TimerCircle;  //タイマーのだんだん減ってく円

    private SceneLording sceneLording;  //シーンローディングスクリプト

    private bool onceOnly = true;

    private void Start()
    {
        countdownSeconds = countdownMinutes * 60;  //分数を秒数に変更
        sceneLording = GameObject.FindWithTag("SceneManager").GetComponent<SceneLording>();  //タグでシーンローディングスクリプトを取得
    }

    void Update()
    {
        countdownSeconds -= Time.deltaTime;  //時間が経つにつれて、時間が減る
        countdownSeconds = Mathf.Clamp(countdownSeconds, 0, countdownMinutes * 60);  //0未満にならないように制限
        
        var span = new TimeSpan(0, 0, (int)countdownSeconds);  //TimeSpanを使って、秒数をhh:mm:ss変換
                                                               //第一引数が時間(h)、第二引数が分(min)、第三引数が秒(sec)
                                                               //第三引数は強制的にint型にされる
        TimertextMeshPro.SetText(span.ToString(@"mm\:ss"));  //分と秒を表示


        TimerCircle.fillAmount = countdownSeconds / (countdownMinutes * 60);  //残り時間の比率でfillAmountを計算

        if (countdownSeconds <= 0 && onceOnly)
        {
            onceOnly = false;
            // 0秒になったときの処理
            Debug.Log("探索タイマーが終了した");

            //撮影シーンに遷移
            sceneLording.StartLoad();
        }
    }
}
