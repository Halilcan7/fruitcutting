#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ModelPostProcessor : AssetPostprocessor
{
    // Bu fonksiyon sadece Editor ortamında çalışacak
    void OnPreprocessModel()
    {
        var modelImporter = (ModelImporter)assetImporter;
        modelImporter.isReadable = true; // Tüm modeller için Read/Write Enabled etkinleştir
    }
}
#endif
