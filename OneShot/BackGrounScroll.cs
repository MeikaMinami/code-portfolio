using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGrounScroll : MonoBehaviour
{
    [SerializeField] private RawImage image;  //スクロールさせたいRawImage
    [SerializeField] private float _x, _y;  //スクロールの速度

    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, image.uvRect.size);  //現在の表示サイズで毎フレーム移動
    }
}
