using UnityEngine;

/// <summary>
/// �L�����N�^�[�̊�{�f�[�^��ێ����� ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject //���ꂪ���N���X
{
    public int CharaId;  //Id

    public Sprite charaSprite;  //�L�����N�^�[�̃X�v���C�g�摜

    public int SelectedId;  //�f�o�b�N�ppublic�ŏ�

    public int Souryoku = 0;  //�L�����N�^�[�̑���


    /// <summary>
    /// �L�����N�^�[�̃^�C�v�i�p���[�^�A�X�s�[�h�^�A�o�����X�^�j
    /// </summary>
    [System.Serializable]
    public enum CharaType
    {
        Power,
        Speed,
        Balance,
    };

    //�L�����N�^�[�̃^�C�v��Inspector��ŕҏW
    [SerializeField]
    public CharaType CharacterType;

    /// <summary>
    /// �ŏ����Z�b�g�ł���
    /// </summary>
    /// <param name="order"></param>
    public void SetSelectId(int order)
    {
        SelectedId = order;
    }

    /// <summary>
    /// �L�����N�^�[�̃^�C�v���擾�ł���
    /// </summary>
    public CharaType GetCharaType()
    {
        return CharacterType;
    }

    /// <summary>
    /// �L�����N�^�[��ID���擾����
    /// </summary>
    public int GetCharaId()
    {
        return CharaId;
    }
}