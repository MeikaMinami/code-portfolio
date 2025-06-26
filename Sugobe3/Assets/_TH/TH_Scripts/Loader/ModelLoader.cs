using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// �A�h���b�T�u���A�Z�b�g�V�X�e�����g�p����3D���f����ǂݍ��݁A�\������э폜����N���X�B
/// </summary>
public class ModelLoader : MonoBehaviour
{
    /// <summary>
    /// �o�b�^�[�̃��f���i��U��j
    /// </summary>
    public AssetReference Model_Stadium;
    /// <summary>
    /// �o�b�^�[�̃��f���i��U��j
    /// </summary>
    public AssetReference Model_Btter;

    /// <summary>
    /// �s�b�`���[�̃��f��
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
        // ������
        InitializeModelLoader();
    }



    /// <summary>
    /// �������iModelLoader�N���X���ŏ��������Ă�j
    /// </summary>
    private void InitializeModelLoader()
    {
        // �K�v�ȏ����������������ɒǉ�
    }

    /// <summary>
    /// ���f�����폜����֐�
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
    /// ���f����ǂݍ��݁A�\������֐�
    /// </summary>
    /// <param name="asset">�ǂݍ��ރ��f���̃A�Z�b�g���t�@�����X</param>
    public void LoadModel(AssetReference asset)
    {
        if (IsModelLoading) return; // ���f�������ɓǂݍ��܂�Ă���ꍇ�͏������X�L�b�v

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
/// �A�h���b�T�u���A�Z�b�g�V�X�e�����g�p����3D���f����ǂݍ��݁A�\������э폜����N���X�B
/// </summary>
public class ModelLoader : MonoBehaviour
{
    /// <summary>
    /// �X�^�W�A���̃��f��
    /// </summary>
    public AssetReference Model_Stadium;

    /// <summary>
    /// �o�b�^�[�̃��f��
    /// </summary>
    public AssetReference Model_Btter;

    /// <summary>
    /// �s�b�`���[�̃��f��
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
        // ������
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
    /// �������iModelLoader�N���X���ŏ��������Ă�j
    /// </summary>
    private void InitializeModelLoader()
    {
        // �K�v�ȏ����������������ɒǉ�
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
    /// ���f����ǂݍ��݁A�\������֐�
    /// </summary>
    /// <param name="asset">�ǂݍ��ރ��f���̃A�Z�b�g���t�@�����X</param>
    /// <param name="category">���f���̃J�e�S���iCharacter, Building, Other�j</param>
    public void LoadModel(AssetReference asset, string category)
    {
        if (IsModelLoading) return; // ���f�������ɓǂݍ��܂�Ă���ꍇ�͏������X�L�b�v

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