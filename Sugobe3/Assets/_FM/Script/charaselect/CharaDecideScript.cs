using UnityEngine;

public class CharaDecideScript : MonoBehaviour
{
    [SerializeField] character1P chara1P;
    [SerializeField] character2P chara2P;
    void Update()
    {
        if (chara1P.decide1P && chara2P.decide2P)
        {
            if (BaseBallManager.GetInstance()._BBR.GetIsOmote())
            {
                ModeManeger.BatterMode();
                ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                    ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                     ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_1P);
            }
            else if (!BaseBallManager.GetInstance()._BBR.GetIsOmote())
            {
                ModeManeger.BatterMode();
                ScreenManager.GetInstance()._MainManager.SetMainScreen(true, ScreenManager.GetInstance()._MainManager.Score,
                    ScreenManager.GetInstance()._MainManager.BatterGame, ScreenManager.GetInstance()._MainManager.TargetNum,
                     ScreenManager.GetInstance()._MainManager.Batter_Cam, ScreenManager.GetInstance()._MainManager.Pos_Attack_2P);
            }
        }
    }
}
