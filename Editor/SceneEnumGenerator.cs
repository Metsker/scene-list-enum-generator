using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

[InitializeOnLoad]
public static class SceneEnumGenerator
{
    private const string EnumName = "SceneList";
    private const string PrefsKey = "SceneEnumGenerator.Initialized";

    static SceneEnumGenerator()
    {
        EditorBuildSettings.sceneListChanged += EditorBuildSettingsOnSceneListChanged;

        if (EditorPrefs.GetBool(PrefsKey))
            return;
            
        EditorBuildSettingsOnSceneListChanged();
        EditorPrefs.SetBool(PrefsKey, true);
    }
        
    private static void EditorBuildSettingsOnSceneListChanged() =>
        EnumGenerator.Generate(EnumName, InBuildSceneNames().ToArray());

    private static IEnumerable<string> InBuildSceneNames()
    {
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled)
                continue;
                
            yield return Path.GetFileNameWithoutExtension(scene.path);
        }
    }
}