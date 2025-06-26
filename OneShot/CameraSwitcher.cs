using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineMixingCamera mixingCamera;
    public int TansakuCameraIndex = 0;  // 探索用カメラのIndex（最初のカメラ）
    public int GojyasuCameraIndex = 1;  // ゴージャス会話用カメラのIndex（2つ目のカメラ）
    public int HachiCameraIndex = 2;    // ハチ会話用カメラのIndex（2つ目のカメラ）
    public int InuCameraIndex = 3;      // イヌ会話用カメラのIndex（2つ目のカメラ）
    public int BanbiCameraIndex = 4;    // バンビ会話用カメラのIndex（2つ目のカメラ）
    public int TomasCameraIndex = 5;    // トーマス会話用カメラのIndex（2つ目のカメラ）
    public int UshiCameraIndex = 6;     // ウシ会話用カメラのIndex（2つ目のカメラ）

    /// <summary>
    /// 1:ゴージャス、２：はち、：３：いぬ、４：ばんび、５：トーマス、６：うし
    /// </summary>

    private void Start()
    {
        // 最初は探索用カメラ100%、会話用カメラ0%
        mixingCamera.SetWeight(TansakuCameraIndex, 1);  //探索用カメラ
        mixingCamera.SetWeight(GojyasuCameraIndex, 0);  //ゴージャス用カメラ
        mixingCamera.SetWeight(HachiCameraIndex, 0);    //ハチ用カメラ
        mixingCamera.SetWeight(InuCameraIndex, 0);      //犬用カメラ
        mixingCamera.SetWeight(BanbiCameraIndex, 0);　　//バンビ用カメラ
        mixingCamera.SetWeight(TomasCameraIndex, 0);    //トーマス用カメラ
        mixingCamera.SetWeight(UshiCameraIndex, 0);     //ウシ用カメラ
    }

    public void StartConversation(int cameraID)
    {
        // 指定された会話カメラへスムーズに切り替え
        StartCoroutine(SmoothTransition(cameraID, 1));
    }

    public void EndConversation()
    {
        // 探索カメラへスムーズに戻す
        StartCoroutine(SmoothTransition(0, 1));
    }

    private IEnumerator SmoothTransition(int targetIndex, float duration)  //コルーチンでウェイトを増やして滑らかに切り替える
    {
        float elapsedTime = 0f;

        Dictionary<int, float> startWeights = new Dictionary<int, float>();

        // すべてのカメラの初期ウェイトを取得
        for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
        {
            startWeights[i] = mixingCamera.GetWeight(i);
        }


        //指定された時間(duration)でカメラのウェイトを徐々に切り替える
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float blend = elapsedTime / duration;

            // すべてのカメラのウェイトを線系補間(Lerp)で更新
            for (int i = 0; i < mixingCamera.ChildCameras.Length; i++)
            {
                float targetWeight = (i == targetIndex) ? 1 : 0;  //対象カメラだけを１，それ以外は０に
                mixingCamera.SetWeight(i, Mathf.Lerp(startWeights[i], targetWeight, blend));
            }

            yield return null;
        }
    }
}
