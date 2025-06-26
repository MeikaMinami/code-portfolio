using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    public List<AssetReference> materialReferences;

    void Start()
    {
        foreach (var materialReference in materialReferences)
        {
            if (materialReference != null)
            {
                materialReference.LoadAssetAsync<Material>().Completed += OnMaterialLoaded;
            }
        }
    }

    private void OnMaterialLoaded(AsyncOperationHandle<Material> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Material material = obj.Result;
            if (material != null)
            {
                // ラフネスマップの処理
                Texture2D roughnessMap = material.GetTexture("_SpecGlossMap") as Texture2D;
                if (roughnessMap != null)
                {
                    if (roughnessMap.isReadable)
                    {
                        Color[] pixels = roughnessMap.GetPixels();
                        for (int i = 0; i < pixels.Length; i++)
                        {
                            pixels[i].r = 1.0f - pixels[i].r; // 赤チャンネルの値を反転
                        }
                        Texture2D newRoughnessMap = new Texture2D(roughnessMap.width, roughnessMap.height);
                        newRoughnessMap.SetPixels(pixels);
                        Debug.Log(pixels[0].r);
                        Debug.Log(pixels[1].r);
                        newRoughnessMap.Apply();

                        material.SetTexture("_SpecGlossMap", newRoughnessMap);
                        Debug.Log("Roughness map red channel values have been inverted.");
                    }
                    else
                    {
                        Debug.LogError("The roughness map texture is not readable. Please enable 'Read/Write Enabled' in the texture import settings.");
                    }
                }
                else
                {
                    Debug.LogWarning("Roughness map not found. Only inverting roughness value.");
                }

                // ラフネスの値を反転
                float roughness = material.GetFloat("_Glossiness");
                material.SetFloat("_Glossiness", 1.0f - roughness);
                Debug.Log("Roughness value has been inverted.");
            }
        }
        else
        {
            Debug.LogError("Failed to load material.");
        }
    }
}

