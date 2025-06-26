using UnityEngine;

/// <summary>
/// キャラクターの基本データを保持する ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject //これが基底クラス
{
    public int CharaId;  //Id

    public Sprite charaSprite;  //キャラクターのスプライト画像

    public int SelectedId;  //デバック用public打順

    public int Souryoku = 0;  //キャラクターの走力


    /// <summary>
    /// キャラクターのタイプ（パワー型、スピード型、バランス型）
    /// </summary>
    [System.Serializable]
    public enum CharaType
    {
        Power,
        Speed,
        Balance,
    };

    //キャラクターのタイプをInspector上で編集
    [SerializeField]
    public CharaType CharacterType;

    /// <summary>
    /// 打順をセットできる
    /// </summary>
    /// <param name="order"></param>
    public void SetSelectId(int order)
    {
        SelectedId = order;
    }

    /// <summary>
    /// キャラクターのタイプを取得できる
    /// </summary>
    public CharaType GetCharaType()
    {
        return CharacterType;
    }

    /// <summary>
    /// キャラクターのIDを取得する
    /// </summary>
    public int GetCharaId()
    {
        return CharaId;
    }
}