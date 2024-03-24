using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditorInternal;

namespace Unity.Editor
{
    internal class HotFixEditorCompilerHelper
    {
        [MenuItem("Tools/Build/HotFix Editor Compiler Remove", false, 100)]
        static void RemoveEditor()
        {
            var path = "Assets/Hotfix/Unity.HotFix.asmdef";
            HotFixAssemblyDefinitionHelper.RemoveEditor(path);
        }

        [MenuItem("Tools/Build/HotFix Editor Compiler Add", false, 100)]
        static void AddEditor()
        {
            var path = "Assets/Hotfix/Unity.HotFix.asmdef";
            HotFixAssemblyDefinitionHelper.AddEditor(path);
        }
    }

    public class HotFixAssemblyDefinitionHelper
    {
        sealed class AssemblyDefinitionInfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string rootNamespace { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> references { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> includePlatforms { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> excludePlatforms { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool allowUnsafeCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool overrideReferences { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> precompiledReferences { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool autoReferenced { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> defineConstraints { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<string> versionDefines { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public bool noEngineReferences { get; set; }
        }


        internal static void AddEditor(string path)
        {
            AssemblyDefinitionAsset assemblyDefinitionAsset = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(path);
            AssemblyDefinitionInfo info = JsonConvert.DeserializeObject<AssemblyDefinitionInfo>(assemblyDefinitionAsset.text);
            bool isEditor = info.excludePlatforms.Any(m => m == "Editor");
            if (!isEditor)
            {
                info.excludePlatforms.Add("Editor");
                System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(info, Formatting.Indented));
                AssetDatabase.ImportAsset(path);
            }
        }


        internal static void RemoveEditor(string path)
        {
            AssemblyDefinitionAsset assemblyDefinitionAsset = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(path);
            AssemblyDefinitionInfo info = JsonConvert.DeserializeObject<AssemblyDefinitionInfo>(assemblyDefinitionAsset.text);
            bool isEditor = info.excludePlatforms.Any(m => m == "Editor");
            if (isEditor)
            {
                info.excludePlatforms.Remove("Editor");
            }

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(info, Formatting.Indented));
            AssetDatabase.ImportAsset(path);
        }
    }
}