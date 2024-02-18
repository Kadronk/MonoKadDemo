using System.Text;
using System.Text.Json;
using Assimp;
using Microsoft.Xna.Framework.Graphics;

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
            StringBuilder sb = new StringBuilder();
            AssimpContext assimpCtx = new AssimpContext();
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions() { IncludeFields = true };

            foreach (string path in allAssetsPath) {
                if (assimpCtx.IsImportFormatSupported(Path.GetExtension(path)))
                    Load3DModel(path, assimpCtx, sb);
                else if (Path.GetExtension(path) == ".png")
                    LoadTexture(path);
                else if (Path.GetExtension(path) == ".mat")
                    LoadMaterial(path, jsonOptions);
            }
        }

        public static T GetAsset<T>(string key) {
            if (KadGame.Instance.AssetLoader._loadedAssets.ContainsKey(key) == false)
                throw new Exception($"There is no loaded asset with key \"{key}\"");
            return InternalGetAsset<T>(key);
        }

        /// <summary> Unlike <see cref="GetAsset{T}"/>, this asset getter skips the key check.</summary>
        static T InternalGetAsset<T>(string key) {
            AssetLoader self = KadGame.Instance.AssetLoader;
            if (self._loadedAssets[key] == null) {
                self._loadedAssets.Remove(key);
                throw new Exception($"The asset at key \"{key}\" is null");
            }
            if (self._loadedAssets[key] is T castedAsset == false)
                throw new Exception($"The asset at key \"{key}\" is not a {typeof(T)}");
            return castedAsset;
        }

        public static bool TryGetAsset<T>(string key, out T asset) where T : class {
            AssetLoader self = KadGame.Instance.AssetLoader;
            if (self._loadedAssets.ContainsKey(key)) {
                asset = InternalGetAsset<T>(key);
                return true;
            }
            asset = null;
            return false;
        }
        
        void AddAsset(string key, object asset, bool trimGameDataDir = false) {
            if (trimGameDataDir && key.StartsWith(PATH_GAME_DATA))
                key = TrimGameDataDirectory(key);
            KadGame.Instance.AssetLoader._loadedAssets.Add(key, asset);
        }

        public void UnloadAll() {
            AssetLoader self = KadGame.Instance.AssetLoader;
            foreach (object asset in self._loadedAssets.Values) {
                if (asset is IDisposable disposable) {
                    disposable.Dispose();
                }
            }
            self._loadedAssets.Clear();
        }

        static string TrimGameDataDirectory(string originalPath) {
            return originalPath.Remove(0, PATH_GAME_DATA.Length + 1);
        }

        void Load3DModel(string path, AssimpContext ctx, StringBuilder sb) {
            Scene assimpScene = ctx.ImportFile(path, 
                PostProcessSteps.Triangulate
                | PostProcessSteps.JoinIdenticalVertices
                | PostProcessSteps.FlipUVs
                | PostProcessSteps.OptimizeMeshes);
            if (assimpScene == null)
                return;

            sb.Clear();
            sb.Append(path);
            sb.Append('/');
            foreach (Assimp.Mesh assimpMesh in assimpScene.Meshes) { //TODO: consider nodes and all that stuff
                sb.Append(assimpMesh.Name);
                AddAsset(sb.ToString(), new Mesh(assimpMesh), true);
                Console.WriteLine($"Loaded: {sb.ToString()}");
                sb.Remove(path.Length + 1, assimpMesh.Name.Length);
            }
        }

        void LoadTexture(string path) {
            if (_loadedAssets.ContainsKey(TrimGameDataDirectory(path))) return; //proof that this is starting to get STINKY. TODO if needed: class for a single file. c# objects will be "subassets"
            
            Texture2D texture = Texture2D.FromFile(KadGame.Instance.GraphicsDevice, path);
            AddAsset(path, texture, true);
        }

        void LoadMaterial(string path, JsonSerializerOptions jsonOptions) {
            BasicEffectOverrides effectOverrides = JsonSerializer.Deserialize<BasicEffectOverrides>(File.ReadAllText(path), jsonOptions);
            BasicEffect effect = new BasicEffect(KadGame.Instance.GraphicsDevice);
            
            Console.WriteLine($"Loading: {path} - Texture = {effectOverrides.TexturePath} - Lighting = {effectOverrides.LightingEnabled}");

            effect.DiffuseColor = effectOverrides.DiffuseColor;
            effect.SpecularPower = effectOverrides.SpecularPower;
            effect.SpecularColor = effectOverrides.SpecularColor;
            
            if (string.IsNullOrEmpty(effectOverrides.TexturePath) == false) {
                Texture2D texture = null;
                if (TryGetAsset(effectOverrides.TexturePath, out texture) == false) {
                    LoadTexture(Path.Combine(PATH_GAME_DATA, effectOverrides.TexturePath));
                    texture = InternalGetAsset<Texture2D>(effectOverrides.TexturePath);
                }
                effect.TextureEnabled = true;
                effect.Texture = texture;
            }

            if (effectOverrides.LightingEnabled) {
                effect.LightingEnabled = true;
                effect.AmbientLightColor = effectOverrides.AmbientLightColor;
                for (int i = 0; i < effectOverrides.Lights.Length; i++) {
                    DirectionalLight light = effect.DirectionalLight0;
                    
                    if (i == 1) light = effect.DirectionalLight1;
                    else if (i == 2) light = effect.DirectionalLight2;
                    else if (i >= 3) break;

                    light.Enabled = effectOverrides.Lights[i].Enabled;
                    light.Direction = effectOverrides.Lights[i].Direction;
                    light.DiffuseColor = effectOverrides.Lights[i].DiffuseColor;
                    light.SpecularColor = effectOverrides.Lights[i].SpecularColor;
                }
            }
            
            AddAsset(path, effect, true);
        }
    }   
}
