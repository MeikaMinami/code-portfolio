using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static CharacterData;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> CharaLists;//�S�ẴL�����f�[�^���i�[���郊�X�g

    void Start ()
    {
        //Resources�t�H���_���̂��ׂĂ�CharacterData �����[�h
        CharaLists = new List<CharacterData>(Resources.LoadAll<CharacterData>(""));
    }

    /// <summary>
    /// �L����ID�����Ƀ^�C�v���擾
    /// </summary>
    /// <param name="charaId"></param>
    public CharaType GetCharaType(int charaId)
    {
        //�w�肵��ID��CharacterData ������
        CharacterData data = CharaLists.FirstOrDefault(c => c.GetCharaId() == charaId);

        return data.GetCharaType();
    }

    /// <summary>
    /// �L����ID�����ɑŏ���ύX�@�����F(�L����ID, �ŏ���ύX�������l)
    /// </summary>
    /// <param name="charaId"></param>
    /// <param name="newSelectId"></param>
    public void SetSelectId(int charaId, int newSelectId)
    {
        //�w�肵��ID��CharacterData������
        CharacterData data = CharaLists.FirstOrDefault(c => c.GetCharaId() == charaId);

        data.SetSelectId(newSelectId);  //�V�����ŏ����Z�b�g
    }
}
