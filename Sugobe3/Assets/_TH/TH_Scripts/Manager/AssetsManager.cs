using System;
using UnityEngine.Audio;

public class AssetsManager
{
    private MovieLoader InstVL;

    private ModelLoader InstML;

    private AudioLoader InstAL;

    private AudioMixer InstAM;

    private static AssetsManager AMInstance;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    private AssetsManager()
    {
        Console.WriteLine("AssetsManager����������܂���");
        //����������
    }
    #region SeterGeter�ꗗ
    //==============================================�@�@Seter�ꗗ�@�@======================================================

    public void SetVideoLoader(MovieLoader Video)
    {
        InstVL = Video;
    }
    public void SetModelLoader(ModelLoader Model)
    {
        InstML = Model;
    }

    public void SetAudioLoader(AudioLoader Aoudio)
    {
        InstAL = Aoudio;
    }

    public void SetAudioMixer(AudioMixer AM)
    {
        InstAM = AM;
    }
    //==============================================�@�@Seter�ꗗ�@�@======================================================

    //==============================================�@�@Geter�ꗗ�@�@======================================================

    public MovieLoader _VideoLoader
    {
        get
        {
            return InstVL;
        }

    }
    public ModelLoader _ModelLoader
    {
        get
        {
            return InstML;
        }

    }

    public AudioLoader _AudioLoader
    {
        get
        {
            return InstAL;
        }
    }

    public AudioMixer _AudioMixer
    {
        get
        {
            return InstAM;
        }
    }

    //==============================================�@�@Geter�ꗗ�@�@======================================================
    #endregion

    public static AssetsManager GetInstance()
    {
        if (AMInstance == null)
            AMInstance = new AssetsManager();

        return AMInstance;
    }
}
