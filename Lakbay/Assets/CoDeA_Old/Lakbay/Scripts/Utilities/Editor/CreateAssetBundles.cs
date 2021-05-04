using UnityEditor;
using System.IO;

namespace CoDeA_Old.Lakbay.Utilities {
    // Source: https://docs.unity3d.com/Manual/AssetBundles-Workflow.html
    public class CreateAssetBundles {
        public static string confirmBuildSettings() {
            string path = EditorUtility.SaveFolderPanel("Save Location", "./", "");

            // string assetBundleDirectory = "Assets/AssetBundles";
            string assetBundleDirectory = path;

            if(path.Length == 0) return "";

            if(!Directory.Exists(assetBundleDirectory)) {
                Directory.CreateDirectory(assetBundleDirectory);

            }

            return path;

        }

        public static void build(string path, AssetBundleBuild[] builds, BuildAssetBundleOptions options, BuildTarget platform) {
            BuildPipeline.BuildAssetBundles(
                path,
                builds,
                options, 
                platform
            );

        }

        public static void build(string path, BuildAssetBundleOptions options, BuildTarget platform) {
            CreateAssetBundles.build(path, new AssetBundleBuild[] {}, options, platform);

        }

        public static void build(string path, BuildTarget platform) {
            CreateAssetBundles.build(path, BuildAssetBundleOptions.ChunkBasedCompression, platform);

        }

        public static void build(string path) {
            CreateAssetBundles.build(path, BuildTarget.Android);

        }

        [MenuItem("Asset Bundle Manager/Build For Android")]
        public static void buildForAndroid() {
            string path = CreateAssetBundles.confirmBuildSettings();
            if(path.Length == 0) return;

            CreateAssetBundles.build(path);

        }
        
    }

}
