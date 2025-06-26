using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// アドレッサブルアセットシステムを使用して3Dモデルを読み込み、表示および削除するクラス。
/// </summary>
public class ModelLoader : MonoBehaviour
{
    /// <summary>
    /// バッターのモデル（空振り）
    /// </summary>
    public AssetReference Model_Stadium;
    /// <summary>
    /// バッターのモデル（空振り）
    /// </summary>
    public AssetReference Model_Btter;

    /// <summary>
    /// ピッチャーのモデル
    /// </summary>
    public AssetReference Model_Pitcher;

    private GameObject loadedModel;
    private AsyncOperationHandle<GameObject> modelHandle;
    private bool IsModelLoading = false;

    void Awake()
    {
        AssetsManager.GetInstance().SetModelLoader(this);
    }

    void Start()
    {
        // 初期化
        InitializeModelLoader();
    }



    /// <summary>
    /// 初期化（ModelLoaderクラス内で初期化してる）
    /// </summary>
    private void InitializeModelLoader()
    {
        // 必要な初期化処理をここに追加
    }

    /// <summary>
    /// モデルを削除する関数
    /// </summary>
    public void ClearModel()
    {
        if (loadedModel != null)
        {
            Destroy(loadedModel);
            loadedModel = null;
        }

        if (IsModelLoading)
        {
            Addressables.Release(modelHandle);
            IsModelLoading = false;
        }
    }

    /// <summary>
    /// モデルを読み込み、表示する関数
    /// </summary>
    /// <param name="asset">読み込むモデルのアセットリファレンス</param>
    public void LoadModel(AssetReference asset)
    {
        if (IsModelLoading) return; // モデルが既に読み込まれている場合は処理をスキップ

        modelHandle = Addressables.LoadAssetAsync<GameObject>(asset);
        modelHandle.Completed += OnModelLoaded;
    }

    private void OnModelLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            loadedModel = Instantiate(handle.Result, transform);
        }
        else
        {
            Debug.LogError("Failed to load model.");
        }
    }
}


/*

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

/// <summary>
/// アドレッサブルアセットシステムを使用して3Dモデルを読み込み、表示および削除するクラス。
/// </summary>
public class ModelLoader : MonoBehaviour
{
    /// <summary>
    /// スタジアムのモデル
    /// </summary>
    public AssetReference Model_Stadium;

    /// <summary>
    /// バッターのモデル
    /// </summary>
    public AssetReference Model_Btter;

    /// <summary>
    /// ピッチャーのモデル
    /// </summary>
    public AssetReference Model_Pitcher;

    private Dictionary<string, List<GameObject>> LoadedModels = new ();
    private AsyncOperationHandle<GameObject> modelHandle;
    private bool IsModelLoading = false;

    void Awake()
    {
        AssetsManager.GetInstance().SetModelLoader(this);
    }

    void Start()
    {
        // 初期化
        InitializeModelLoader();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsModelLoading)
        {
            IsModelLoading = true;
            LoadModel(Model_Stadium, "Building");
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !IsModelLoading)
        {
            ClearModels("Building");
        }
    }

    /// <summary>
    /// 初期化（ModelLoaderクラス内で初期化してる）
    /// </summary>
    private void InitializeModelLoader()
    {
        // 必要な初期化処理をここに追加
    }

    public void ClearModels(string category)
    {
        if (LoadedModels.ContainsKey(category))
        {
            foreach (var model in LoadedModels[category])
            {
                Destroy(model);
            }
            LoadedModels[category].Clear();
        }

        if (IsModelLoading)
        {
            Addressables.Release(modelHandle);
            IsModelLoading = false;
        }
    }

    /// <summary>
    /// モデルを読み込み、表示する関数
    /// </summary>
    /// <param name="asset">読み込むモデルのアセットリファレンス</param>
    /// <param name="category">モデルのカテゴリ（Character, Building, Other）</param>
    public void LoadModel(AssetReference asset, string category)
    {
        if (IsModelLoading) return; // モデルが既に読み込まれている場合は処理をスキップ

        modelHandle = Addressables.LoadAssetAsync<GameObject>(asset);
        modelHandle.Completed += handle => OnModelLoaded(handle, category);
    }

    private void OnModelLoaded(AsyncOperationHandle<GameObject> handle, string category)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var model = Instantiate(handle.Result, transform);
            if (!LoadedModels.ContainsKey(category))
            {
                LoadedModels[category] = new List<GameObject>();
            }
            LoadedModels[category].Add(model);
        }
        else
        {
            Debug.LogError("Failed to load model.");
        }
        IsModelLoading = false;
    }
}

*/