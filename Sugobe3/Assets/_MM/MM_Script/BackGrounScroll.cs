using UnityEngine;
using UnityEngine.UI;

public class BackGrounScroll : MonoBehaviour
{
    [SerializeField] private RawImage Backimage;  //�w�i��RawImage
    [SerializeField] private float _x, _y;  //�w�i���X�N���[�������鑬�x

    void Update()
    {
        //UV���W�𖈃t���[�� _x, _y �̕����ɂ��炷
        Backimage.uvRect = new Rect(Backimage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, Backimage.uvRect.size);
    }
}