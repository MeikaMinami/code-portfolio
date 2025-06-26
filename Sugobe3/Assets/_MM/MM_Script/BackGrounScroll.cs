using UnityEngine;
using UnityEngine.UI;

public class BackGrounScroll : MonoBehaviour
{
    [SerializeField] private RawImage Backimage;  //背景のRawImage
    [SerializeField] private float _x, _y;  //背景をスクロールさせる速度

    void Update()
    {
        //UV座標を毎フレーム _x, _y の方向にずらす
        Backimage.uvRect = new Rect(Backimage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, Backimage.uvRect.size);
    }
}