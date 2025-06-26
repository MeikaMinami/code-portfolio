using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Video;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.Audio;


/// <summary>
/// アドレッサブルアセットシステムを使用してビデオクリップを読み込み、再生および停止するクラス。
/// </summary>
public class MovieLoader : MonoBehaviour
{
    /// <summary>
    /// 赤が守備の時の１ベースヒットの映像
    /// </summary>
    public AssetReference Mv_RedHit;

    /// <summary>
    /// 赤が守備の時の２ベースヒットの映像
    /// </summary>
    public AssetReference Mv_Red2Hit;

    /// <summary>
    /// 赤が守備の時の３ベースヒットの映像
    /// </summary>
    public AssetReference Mv_Red3Hit;

    /// <summary>
    /// 青が守備の時の１ベースヒットの映像
    /// </summary>
    public AssetReference Mv_BlueHit;

    /// <summary>
    /// 青が守備の時の２ベースヒットの映像
    /// </summary>
    public AssetReference Mv_Blue2Hit;

    /// <summary>
    /// 青が守備の時の３ベースヒットの映像
    /// </summary>
    public AssetReference Mv_Blue3Hit;

    /// <summary>
    /// 赤が守備の時のファウルの映像
    /// </summary>
    public AssetReference Mv_RedFoul;

    /// <summary>
    /// 青が守備の時のファウルの映像
    /// </summary>
    public AssetReference Mv_BlueFoul;

    /// <summary>
    /// 赤が守備の時のボールの映像
    /// </summary>
    public AssetReference Mv_RedBall;

    /// <summary>
    /// 青が守備の時のボールの映像
    /// </summary>
    public AssetReference Mv_BlueBall;

    /// <summary>
    /// 赤が守備の時のストライクの映像
    /// </summary>
    public AssetReference Mv_RedStrike;

    /// <summary>
    /// 青が守備の時のストライクの映像
    /// </summary>
    public AssetReference Mv_BlueStrike;

    /// <summary>
    /// 赤が守備の時のフォアボールの映像
    /// </summary>
    public AssetReference Mv_RedFourBall;

    /// <summary>
    /// 青が守備の時のフォアボールの映像
    /// </summary>
    public AssetReference Mv_BlueFourBall;

    /// <summary>
    /// 赤が守備の時のフライの映像
    /// </summary>
    public AssetReference Mv_RedFly;

    /// <summary>
    /// 青が守備の時のフライの映像
    /// </summary>
    public AssetReference Mv_BlueFly;

    /// <summary>
    /// 赤が守備の時のチェンジの映像
    /// </summary>
    public AssetReference Mv_RedChange;

    /// <summary>
    /// 青が守備の時のチェンジの映像
    /// </summary>
    public AssetReference Mv_BlueChange;

    /// <summary>
    /// 赤が守備の時のホームランの映像
    /// </summary>
    public AssetReference Mv_RedHomeRun;

    /// <summary>
    /// 青が守備の時のホームランの映像
    /// </summary>
    public AssetReference Mv_BlueHomeRun;


    AudioSource audioSource;
    private VideoPlayer Video_Play;
    private AsyncOperationHandle<VideoClip> Mv_handle;
    private bool IsVideoLoaded = false;
    public bool IsVideoPlaying = false; // 再生中かどうかを判定するフラグ

    [SerializeField]
    private RawImage Mv_Display;

    void Awake()
    {
        AssetsManager.GetInstance().SetVideoLoader(this);
    }

    void Start()
    {
        //初期化
        InitializeVideoPlayer();
    }
    void Update()
    {
        //既存のAudiosourceを取得・設定
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            Video_Play.audioOutputMode = VideoAudioOutputMode.AudioSource;
            Video_Play.SetTargetAudioSource(0, audioSource);
        }
    }



    /// <summary>
    /// 初期化（VideoLoaderクラス内で初期化してる）<para>※Start関数内の為Awake注意</para>
    /// </summary>
    private void InitializeVideoPlayer()
    {
        Video_Play = gameObject.AddComponent<VideoPlayer>();

        Video_Play.renderMode = VideoRenderMode.RenderTexture;
        Video_Play.targetTexture = new RenderTexture(1920, 1080, 0);
        Mv_Display.texture = Video_Play.targetTexture;
        Video_Play.loopPointReached += OnVideoEnd; // 再生終了時のイベントを登録
    }

    private void ClearVideo()
    {
        Video_Play.Stop();
        Mv_Display.texture = null;
        Mv_Display.gameObject.SetActive(false);
        Video_Play.clip = null;

        if (IsVideoLoaded)
        {
            Addressables.Release(Mv_handle);
            IsVideoLoaded = false;
        }

        IsVideoPlaying = false; // 再生中フラグをリセット
        ResetVideo();
    }

    /// <summary>
    /// 動画再生の関数
    /// <para>※Start関数内の為Awake注意</para>
    /// </summary>
    /// <returns>Mv_Display.[変数名]</returns>
    public void Play_Mv(AssetReference asset)
    {
        if (IsVideoPlaying) return; // 再生中の場合は処理をスキップ

        if (!IsVideoLoaded)
        {
            Mv_Display.gameObject.SetActive(true);
            LoadAndPlayMv(asset);
        }
    }

    private void ResetVideo()
    {
        if (Video_Play.targetTexture != null)
        {
            Video_Play.targetTexture.Release();
        }
        Video_Play.targetTexture = new RenderTexture(1920, 1080, 0);
        Mv_Display.texture = Video_Play.targetTexture;
    }



    private void LoadAndPlayMv(AssetReference asset)
    {
        Mv_handle = Addressables.LoadAssetAsync<VideoClip>(asset);
        Mv_handle.Completed += OnVideoLoaded;
    }

    private void OnVideoLoaded(AsyncOperationHandle<VideoClip> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Video_Play.clip = handle.Result;
            Video_Play.time = 0;
            Video_Play.Play();
            IsVideoLoaded = true;
            IsVideoPlaying = true; // 再生中フラグを設定

        }
        else
        {
            Debug.LogError("Failed to load video.");
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        ClearVideo(); // 再生終了時にビデオをクリア
    }
}
