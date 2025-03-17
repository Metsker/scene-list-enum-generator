using System.IO;
using UnityEditor;

public static class EnumGenerator
{
    private const string Namespace = "CodeGen";
    private const string ProjectSubfolder = "_Project";

    public static void Generate(string enumName, string[] enumEntries)
    {
        string basePath = AssetDatabase.IsValidFolder($"Assets/{ProjectSubfolder}") ? $"Assets/{ProjectSubfolder}" : "Assets";
        string fullPath = $"{basePath}/{Namespace}";
            
        if (!AssetDatabase.IsValidFolder(fullPath))
            AssetDatabase.CreateFolder(basePath, Namespace);
        
        string filePath = $"{fullPath}/{enumName}.g.cs";

        using (StreamWriter streamWriter = new (filePath))
        {
            streamWriter.WriteLine("namespace " + Namespace);
            streamWriter.WriteLine("{");
            streamWriter.WriteLine("    public enum " + enumName);
            streamWriter.WriteLine("    {");
            for (int i = 0; i < enumEntries.Length; i++)
            {
                string entry = enumEntries[i];
                streamWriter.WriteLine($"\t    {entry.Replace(" ", string.Empty)} = {i},");
            }
            streamWriter.WriteLine("    }");
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
