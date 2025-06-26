using UnityEngine;
using static CharacterData;

public class Ability : MonoBehaviour
{

    int[] values = new int[9];

    public int[,] TypeZone(CharaType type)
    {
        if (type == CharaType.Power)
        {
            // ������UI�p�Ƀ^�C�v�𑗐M�I
            return PowerRefresh();
        }
        else if (type == CharaType.Speed)
        {
            // ������UI�p�Ƀ^�C�v�𑗐M�I
            return SpeedRefresh();
        }
        else
        {
            // ������UI�p�Ƀ^�C�v�𑗐M�I
            return BalanceRefreash();
        }
    }


    int[,] PowerRefresh()
    {
        //�Q�̂����ꂩ��2�ǉ�
        for (int i = 0; i < 2; i++)
        {
            values[i] = 2;
        }

        //8��2�ǉ�
        for (int i = 2; i < 4; i++)
        {
            values[i] = 8;
        }


        //13��2�ǉ�
        for (int i = 4; i < 6; i++)
        {
            values[i] = 13;
        }


        //15��1�ǉ�
        for (int i = 6; i < 7; i++)
        {
            values[i] = 15;
        }

        //16��2�ǉ�
        for (int i = 7; i < 8; i++)
        {
            values[i] = 16;
        }

        //20��2�ǉ�
        for (int i = 8; i < 9; i++)
        {
            values[i] = 20;
        }



        //�z�u���V���b�t���i�t�B�b�V���[�E�C�G�[�c�̃A���S���Y���j
        Shuffle(values);


        //�񎟌��z��ɕϊ�
        int[,] array = To2DArray(values, 3, 3);

        Debug.Log("�p���[�^�C�v�̃X�g���C�N�]�[���̍X�V");
        string debugMessage = "�X�g���C�N�]�[��:\n"; // �f�o�b�O�p���b�Z�[�W
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                debugMessage += array[i, j].ToString("00") + " "; // 2���ŕ\��
            }
            debugMessage += "\n"; // ���s��ǉ�
        }
        Debug.Log(debugMessage);
        Debug.Log("�o�b�^�[�͗��I�����Ă�������");

        return array;
    }

    int[,] SpeedRefresh()
    {
        //�P�C�Q�C�S�̂����ꂩ���U�ǉ�
        for (int i = 0; i < 6; i++)
        {
            values[i] = Random.Range(0, 2) switch
            {
                0 => 2,
                1 => 4,
            };
        }

        //21�i�{�[���j���P�ǉ�
        for (int i = 6; i < 7; i++)
        {
            values[i] = 21;
        }

        //�P�`�S�̂����ꂩ�Q��ǉ�
        for (int i = 7; i < 9; i++)
        {
            values[i] = Random.Range(0, 3) switch
            {
                0 => 2,
                1 => 3,
                2 => 4
            };
        }

        //�z����V���b�t��
        Shuffle(values);


        //�񎟌��z��ɕϊ�
        int[,] array = To2DArray(values, 3, 3);

        Debug.Log("�X�s�[�h�^�C�v�̃X�g���C�N�]�[���̍X�V");
        string debugMessage = "�X�g���C�N�]�[��:\n"; // �f�o�b�O�p���b�Z�[�W
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                debugMessage += array[i, j].ToString("00") + " "; // 2���ŕ\��
            }
            debugMessage += "\n"; // ���s��ǉ�
        }
        Debug.Log(debugMessage);
        Debug.Log("�o�b�^�[�͗��I�����Ă�������");

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

        if (SelectZone == 1)  //�]���̃o�����X�^�A�X�F�P�̂X�̕���
        {
            //�Q�`�S�̂����ꂩ���S�ǉ�
            for (int i = 0; i < 4; i++)
            {
                values[i] = Random.Range(0, 3) switch
                {
                    0 => 2,
                    1 => 3,
                    2 => 4
                };
            }

            //�T���P�ǉ�
            for (int i = 4; i < 5; i++)
            {
                values[i] = 5;
            }

            //21�i�{�[���j���R�ǉ�
            for (int i = 5; i < 9; i++)
            {
                values[i] = 21;
            }

            //�z����V���b�t��
            Shuffle(values);


            //�񎟌��z��ɕϊ�
            int[,] array = To2DArray(values, 3, 3);

            Debug.Log("�]���̃o�����X�^�C�v�̃X�g���C�N�]�[���̍X�V");
            string debugMessage = "�X�g���C�N�]�[��:\n"; // �f�o�b�O�p���b�Z�[�W
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    debugMessage += array[i, j].ToString("00") + " "; // 2���ŕ\��
                }
                debugMessage += "\n"; // ���s��ǉ�
            }
            Debug.Log(debugMessage);
            Debug.Log("�o�b�^�[�͗��I�����Ă�������");

            return array;
        }

        else  //NEW�o�����X�^�A�X�F�P�̂P�̕���
        {
            //�P���T�ǉ�
            for (int i = 0; i < 5; i++)
            {
                values[i] = 1;
            }

            //�U���R�ǉ�
            for (int i = 5; i < 8; i++)
            {
                values[i] = 6;
            }

            //�P�S���P�ǉ�
            for (int i = 8; i < 9; i++)
            {
                values[i] = 14;
            }

            //�z����V���b�t��
            Shuffle(values);


            //�񎟌��z��ɕϊ�
            int[,] array = To2DArray(values, 3, 3);

            Debug.Log("NEW�o�����X�^�C�v�̃X�g���C�N�]�[���̍X�V");
            string debugMessage = "�X�g���C�N�]�[��:\n"; // �f�o�b�O�p���b�Z�[�W
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    debugMessage += array[i, j].ToString("00") + " "; // 2���ŕ\��
                }
                debugMessage += "\n"; // ���s��ǉ�
            }
            Debug.Log(debugMessage);
            Debug.Log("�o�b�^�[�͗��I�����Ă�������");

            return array;
        }


    }

    //�t�B�b�V���[�E�C�G�[�c�̃V���b�t��
    void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }
    }


    //�񎟌��z��ɕϊ����郁�\�b�h
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