using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour  //最初にマウスで探索シーンのデバッグを行うためのスクリプト
{
    //[SerializeField] GameObject Player;
    [SerializeField] Transform PlayerTransform;

    Vector3 currentCameraPos;  //現在のカメラの位置
    Vector3 pastCameraPos;     //過去のカメラの位置

    Vector3 diff;  //移動距離

    //ToDo プレイヤーが設定したマウス感度を反映させる作業
    public float sensitivity = 1.0f;  //感度

    // Start is called before the first frame update
    void Start()
    {
        //最初のプレイヤーの位置の取得
        pastCameraPos = PlayerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //カメラの移動====================

        //Debug.Log("PlayerPositoin = " + PlayerTransform.position);
        //プレイヤーの現在位置の取得
        currentCameraPos = PlayerTransform.position;

        diff = currentCameraPos - pastCameraPos;  //現在位置と過去の位置の差で移動距離を求める

        //カメラの位置をプレイヤーの移動距離だけ動かす
        transform.position = Vector3.Lerp(transform.position, transform.position + diff, 1.0f);

        pastCameraPos = currentCameraPos;  //現在位置を保存

        //カメラの移動　End===================



        //カメラの回転===============

        //マウスの移動量を取得
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        //X方向に一定以上マウスが移動していれば横回転
        if (Mathf.Abs(mx) > 0.01f)
        {
            mx = mx * sensitivity;  //ここでマウス感度を設定

            //回転軸はワールド座標のY軸
            transform.RotateAround(PlayerTransform.position, Vector3.up, mx);
        }

        // Y方向に一定量移動していれば縦回転
        if (Mathf.Abs(my) > 0.01f)
        {
            my = my * sensitivity;  //ここでマウス感度を設定

            // 回転軸はカメラ自身のX軸
            transform.RotateAround(PlayerTransform.position, transform.right, -my);
        }
        //カメラの回転　End===================
    }
}
