using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static CharacterData;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterData> CharaLists;//全てのキャラデータを格納するリスト

    void Start ()
    {
        //Resourcesフォルダ内のすべてのCharacterData をロード
        CharaLists = new List<CharacterData>(Resources.LoadAll<CharacterData>(""));
    }

    /// <summary>
    /// キャラIDを元にタイプを取得
    /// </summary>
    /// <param name="charaId"></param>
    public CharaType GetCharaType(int charaId)
    {
        //指定したIDのCharacterData を検索
        CharacterData data = CharaLists.FirstOrDefault(c => c.GetCharaId() == charaId);

        return data.GetCharaType();
    }

    /// <summary>
    /// キャラIDを元に打順を変更　引数：(キャラID, 打順を変更したい値)
    /// </summary>
    /// <param name="charaId"></param>
    /// <param name="newSelectId"></param>
    public void SetSelectId(int charaId, int newSelectId)
    {
        //指定したIDのCharacterDataを検索
        CharacterData data = CharaLists.FirstOrDefault(c => c.GetCharaId() == charaId);

        data.SetSelectId(newSelectId);  //新しい打順をセット
    }
}
