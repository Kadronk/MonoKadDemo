using Assimp;

namespace MonoKad
{
    public class AssetLoader
    {
        private const string PATH_GAME_DATA = "GameData";

        private Dictionary<string, object> _loadedAssets = new Dictionary<string, object>();

        public void LoadFromGameDataFolder() {
            if (Directory.Exists(PATH_GAME_DATA) == false)
                Directory.CreateDirectory(PATH_GAME_DATA);
            
            string[] allAssetsPath = Directory.GetFiles(PATH_GAME_DATA);
            AssimpContext ctx = new AssimpContext();

            foreach (string path in allAssetsPath) {
                // 3D Object
                if (ctx.IsImportFormatSupported(Path.GetExtension(path))) {
                    Scene assimpScene = ctx.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.SortByPrimitiveType/* | PostProcessSteps.JoinIdenticalVertices*/);
                    if (assimpScene != null) {
                        _loadedAssets.Add(path.Remove(0, PATH_GAME_DATA.Length+1), new Mesh(assimpScene.Meshes[0]));
                    }
                }
            }
        }

        public static T GetAsset<T>(string key) {
            AssetLoader self = KadGame.Instance.AssetLoader;
            if (self._loadedAssets.ContainsKey(key) == false)
                throw new Exception($"There is no loaded asset with key \"{key}\"");
            if (self._loadedAssets[key] == null) {
                self._loadedAssets.Remove(key);
                throw new Exception($"The asset at key \"{key}\" is null");
            }
            if (self._loadedAssets[key] is T castedAsset == false)
                throw new Exception($"The asset at key \"{key}\" is not a {typeof(T)}");
            return castedAsset;
        }

        // oooo that's a stinky. TODO: maybe a base "KadAsset" class, or an interface that says a class can be stored in the AssetLoader
        public static void AddAsset(string key, object asset) { 
            KadGame.Instance.AssetLoader._loadedAssets.Add(key, asset);
        }
    }   
}
