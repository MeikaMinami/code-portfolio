using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGrounScroll : MonoBehaviour
{
    [SerializeField] private RawImage image;  //�X�N���[����������RawImage
    [SerializeField] private float _x, _y;  //�X�N���[���̑��x

    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, image.uvRect.size);  //���݂̕\���T�C�Y�Ŗ��t���[���ړ�
    }
}
