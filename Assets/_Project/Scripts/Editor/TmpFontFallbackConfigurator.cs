using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Ensures that TextMeshPro can resolve Korean glyphs even when a text object
/// uses the default Latin font asset.
/// </summary>
[InitializeOnLoad]
internal static class TmpFontFallbackConfigurator
{
    private const string KoreanFallbackPath =
        "Assets/_Project/Font/NotoSansKR-VariableFont_wght SDF.asset";

    static TmpFontFallbackConfigurator()
    {
        EditorApplication.delayCall += EnsureKoreanFallback;
    }

    private static void EnsureKoreanFallback()
    {
        TMP_FontAsset koreanFallback =
            AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(KoreanFallbackPath);

        if (koreanFallback == null)
        {
            Debug.LogWarning(
                $"TMP Korean fallback font was not found at '{KoreanFallbackPath}'.");
            return;
        }

        List<TMP_FontAsset> fallbackFonts =
            TMP_Settings.fallbackFontAssets ?? new List<TMP_FontAsset>();

        if (fallbackFonts.Contains(koreanFallback))
        {
            return;
        }

        fallbackFonts = new List<TMP_FontAsset>(fallbackFonts)
        {
            koreanFallback
        };

        TMP_Settings.fallbackFontAssets = fallbackFonts;
        EditorUtility.SetDirty(TMP_Settings.instance);
        AssetDatabase.SaveAssets();

        Debug.Log("Registered Noto Sans KR as the global TextMeshPro fallback font.");
    }
}
