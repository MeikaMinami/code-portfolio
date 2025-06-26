using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// �A�h���b�T�u���A�Z�b�g�V�X�e�����g�p���ăv���n�u��ǂݍ��݁A�\������N���X�B
/// </summary>
public class ImageLoader : MonoBehaviour
{
    /// <summary>
    /// �X�R�A�\���p�̃L�����o�X�v���n�u
    /// </summary>
    public AssetReference Canvas_Score;
    /// <summary>
    /// �{�[���A�C�R���̃v���n�u
    /// </summary>
    public AssetReference[] Im_Ball;

    private AsyncOperationHandle<GameObject> Im_handle;


    private GameObject loadedCanvas;

    // �v���n�u�̃L���b�V��
    private Dictionary<AssetReference, GameObject> prefabCache = new Dictionary<AssetReference, GameObject>();

    void Start()
    {
        // ������
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
        /// �������iImageLoader�N���X���ŏ��������Ă�j
        /// </summary>
        private void InitializeImageLoader()
        {
            // Canvas_Score�����[�h���ăC���X�^���X��
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
        /// �v���n�u�ǂݍ��݂ƕ\���̊֐�
        /// </summary>
        /// <param name="assets">�ǂݍ��ރA�Z�b�g�̎Q�Ɣz��</param>
        public void LoadAndDisplayPrefabs(AssetReference[] assets)
        {
            foreach (var asset in assets)
            {
                LoadAndDisplayPrefab(asset);
            }
        }

        /// <summary>
        /// �v���n�u�ǂݍ��݂ƕ\���̊֐�
        /// </summary>
        /// <param name="asset">�ǂݍ��ރA�Z�b�g�̎Q��</param>
        private void LoadAndDisplayPrefab(AssetReference asset)
        {
            // �L���b�V���Ƀv���n�u�����݂��邩�m�F
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
                        prefabCache[asset] = handle.Result; // �L���b�V���ɒǉ�
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
        /// �摜��ݒ肷��֐�
        /// </summary>
        /// <param name="sprite">�ݒ肷��X�v���C�g</param>
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
