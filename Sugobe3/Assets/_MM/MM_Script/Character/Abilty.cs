using UnityEngine;
using static CharacterData;

public class Ability : MonoBehaviour
{

    int[] values = new int[9];

    public int[,] TypeZone(CharaType type)
    {
        if (type == CharaType.Power)
        {
            // ここにUI用にタイプを送信！
            return PowerRefresh();
        }
        else if (type == CharaType.Speed)
        {
            // ここにUI用にタイプを送信！
            return SpeedRefresh();
        }
        else
        {
            // ここにUI用にタイプを送信！
            return BalanceRefreash();
        }
    }


    int[,] PowerRefresh()
    {
        //２のいずれかを2つ追加
        for (int i = 0; i < 2; i++)
        {
            values[i] = 2;
        }

        //8を2つ追加
        for (int i = 2; i < 4; i++)
        {
            values[i] = 8;
        }


        //13を2つ追加
        for (int i = 4; i < 6; i++)
        {
            values[i] = 13;
        }


        //15を1つ追加
        for (int i = 6; i < 7; i++)
        {
            values[i] = 15;
        }

        //16を2つ追加
        for (int i = 7; i < 8; i++)
        {
            values[i] = 16;
        }

        //20を2つ追加
        for (int i = 8; i < 9; i++)
        {
            values[i] = 20;
        }



        //配置をシャッフル（フィッシャー・イエーツのアルゴリズム）
        Shuffle(values);


        //二次元配列に変換
        int[,] array = To2DArray(values, 3, 3);

        Debug.Log("パワータイプのストライクゾーンの更新");
        string debugMessage = "ストライクゾーン:\n"; // デバッグ用メッセージ
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                debugMessage += array[i, j].ToString("00") + " "; // 2桁で表示
            }
            debugMessage += "\n"; // 改行を追加
        }
        Debug.Log(debugMessage);
        Debug.Log("バッターは列を選択してください");

        return array;
    }

    int[,] SpeedRefresh()
    {
        //１，２，４のいずれかを６つ追加
        for (int i = 0; i < 6; i++)
        {
            values[i] = Random.Range(0, 2) switch
            {
                0 => 2,
                1 => 4,
            };
        }

        //21（ボール）を１つ追加
        for (int i = 6; i < 7; i++)
        {
            values[i] = 21;
        }

        //１～４のいずれか２つを追加
        for (int i = 7; i < 9; i++)
        {
            values[i] = Random.Range(0, 3) switch
            {
                0 => 2,
                1 => 3,
                2 => 4
            };
        }

        //配列をシャッフル
        Shuffle(values);


        //二次元配列に変換
        int[,] array = To2DArray(values, 3, 3);

        Debug.Log("スピードタイプのストライクゾーンの更新");
        string debugMessage = "ストライクゾーン:\n"; // デバッグ用メッセージ
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                debugMessage += array[i, j].ToString("00") + " "; // 2桁で表示
            }
            debugMessage += "\n"; // 改行を追加
        }
        Debug.Log(debugMessage);
        Debug.Log("バッターは列を選択してください");

        return array;
    }

    int[,] BalanceRefreash()
    {
        int SelectZone;

        SelectZone = Random.Range(0, 10) switch
        {
            0 => 1,
            1 => 1,
            2 => 1,
            3 => 1,
            4 => 1,
            5 => 1,
            6 => 1,
            7 => 1,
            8 => 1,
            9 => 2
        };

        if (SelectZone == 1)  //従来のバランス型、９：１の９の部分
        {
            //２～４のいずれかを４つ追加
            for (int i = 0; i < 4; i++)
            {
                values[i] = Random.Range(0, 3) switch
                {
                    0 => 2,
                    1 => 3,
                    2 => 4
                };
            }

            //５を１つ追加
            for (int i = 4; i < 5; i++)
            {
                values[i] = 5;
            }

            //21（ボール）を３つ追加
            for (int i = 5; i < 9; i++)
            {
                values[i] = 21;
            }

            //配列をシャッフル
            Shuffle(values);


            //二次元配列に変換
            int[,] array = To2DArray(values, 3, 3);

            Debug.Log("従来のバランスタイプのストライクゾーンの更新");
            string debugMessage = "ストライクゾーン:\n"; // デバッグ用メッセージ
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    debugMessage += array[i, j].ToString("00") + " "; // 2桁で表示
                }
                debugMessage += "\n"; // 改行を追加
            }
            Debug.Log(debugMessage);
            Debug.Log("バッターは列を選択してください");

            return array;
        }

        else  //NEWバランス型、９：１の１の部分
        {
            //１を５つ追加
            for (int i = 0; i < 5; i++)
            {
                values[i] = 1;
            }

            //６を３つ追加
            for (int i = 5; i < 8; i++)
            {
                values[i] = 6;
            }

            //１４を１つ追加
            for (int i = 8; i < 9; i++)
            {
                values[i] = 14;
            }

            //配列をシャッフル
            Shuffle(values);


            //二次元配列に変換
            int[,] array = To2DArray(values, 3, 3);

            Debug.Log("NEWバランスタイプのストライクゾーンの更新");
            string debugMessage = "ストライクゾーン:\n"; // デバッグ用メッセージ
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    debugMessage += array[i, j].ToString("00") + " "; // 2桁で表示
                }
                debugMessage += "\n"; // 改行を追加
            }
            Debug.Log(debugMessage);
            Debug.Log("バッターは列を選択してください");

            return array;
        }


    }

    //フィッシャー・イエーツのシャッフル
    void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }
    }


    //二次元配列に変換するメソッド
    int[,] To2DArray(int[] values, int rows, int cols)
    {
        int[,] array = new int[rows, cols];
        int index = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = values[index++];
            }
        }
        return array;
    }
}