using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// アドレッサブルアセットシステムを使用してプレハブを読み込み、表示するクラス。
/// </summary>
public class ImageLoader : MonoBehaviour
{
    /// <summary>
    /// スコア表示用のキャンバスプレハブ
    /// </summary>
    public AssetReference Canvas_Score;
    /// <summary>
    /// ボールアイコンのプレハブ
    /// </summary>
    public AssetReference[] Im_Ball;

    private AsyncOperationHandle<GameObject> Im_handle;


    private GameObject loadedCanvas;

    // プレハブのキャッシュ
    private Dictionary<AssetReference, GameObject> prefabCache = new Dictionary<AssetReference, GameObject>();

    void Start()
    {
        // 初期化
        //InitializeImageLoader();
    }

    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadAndDisplayPrefabs(new[] { Im_Ball[0] });
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadAndDisplayPrefabs(new[] { Im_Ball[1] });
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadAndDisplayPrefabs(new[] { Im_Ball[2] });
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ClearImages();
        }
*/        // ==========================TEST===========================
        if (Input.GetKeyDown(KeyCode.F))
        {
            BaseBallManager.GetInstance()._BBR.AddFoul();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            BaseBallManager.GetInstance()._BBR.AddBall();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            BaseBallManager.GetInstance()._BBR.AddStrike();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            BaseBallManager.GetInstance()._BBR.AddOut();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //BaseBallManager.GetInstance()._BaseBall.();
        }
        // ==========================TEST===========================

    }

    /*    /// <summary>
        /// 初期化（ImageLoaderクラス内で初期化してる）
        /// </summary>
        private void InitializeImageLoader()
        {
            // Canvas_Scoreをロードしてインスタンス化
            Canvas_Score.LoadAssetAsync<GameObject>().Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    loadedCanvas = Instantiate(handle.Result, transform);
                }
                else
                {
                    Debug.LogError("Failed to load Canvas_Score prefab.");
                }
            };
        }

        public void ClearImages()
        {
            foreach (Transform child in loadedCanvas.transform)
            {
                //Destroy(child.gameObject);
                child.gameObject.SetActive(false);
            }

            IsPrefabLoaded = false;
        }

        /// <summary>
        /// プレハブ読み込みと表示の関数
        /// </summary>
        /// <param name="assets">読み込むアセットの参照配列</param>
        public void LoadAndDisplayPrefabs(AssetReference[] assets)
        {
            foreach (var asset in assets)
            {
                LoadAndDisplayPrefab(asset);
            }
        }

        /// <summary>
        /// プレハブ読み込みと表示の関数
        /// </summary>
        /// <param name="asset">読み込むアセットの参照</param>
        private void LoadAndDisplayPrefab(AssetReference asset)
        {
            // キャッシュにプレハブが存在するか確認
            if (prefabCache.ContainsKey(asset))
            {
                GameObject loadedPrefab = Instantiate(prefabCache[asset], loadedCanvas.transform);
                Image loadedImage = loadedPrefab.GetComponent<Image>();

                if (loadedImage == null)
                {
                    loadedImage = loadedPrefab.AddComponent<Image>();
                }

                IsPrefabLoaded = true;
            }
            else
            {
                Im_handle = Addressables.LoadAssetAsync<GameObject>(asset);
                Im_handle.Completed += handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        prefabCache[asset] = handle.Result; // キャッシュに追加
                        GameObject loadedPrefab = Instantiate(handle.Result, loadedCanvas.transform);
                        Image loadedImage = loadedPrefab.GetComponent<Image>();

                        if (loadedImage == null)
                        {
                            loadedImage = loadedPrefab.AddComponent<Image>();
                        }

                        IsPrefabLoaded = true;
                    }
                    else
                    {
                        Debug.LogError("Failed to load prefab.");
                    }
                };
            }
        }

        /// <summary>
        /// 画像を設定する関数
        /// </summary>
        /// <param name="sprite">設定するスプライト</param>
        public void SetImage(Sprite sprite)
        {
            foreach (Transform child in loadedCanvas.transform)
            {
                Image image = child.GetComponent<Image>();
                if (image != null)
                {
                    image.sprite = sprite;
                }
            }
        }*/
}
