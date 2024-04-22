using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoKad.Components;

namespace MonoKad
{
    public class KadGame : Game
    {
        public static KadGame Instance;

        public AssetLoader AssetLoader => _assetLoader;
        public Random Random => _random;
        public Camera CurrentCamera { get => _currentCamera; set => _currentCamera = value; }
        public Color ScreenClearColor { set => _screenClearColor = value; }
        
        internal Time Time => _time;
        internal Physics.Physics3D Physics => _physics;
        
        private GraphicsDeviceManager _graphics;
        private Time _time;
        private AssetLoader _assetLoader;
        private Physics.Physics3D _physics;
        private Random _random = new Random();
        
        private Camera _currentCamera;
        private Color _screenClearColor = Color.CornflowerBlue;

        private List<GameObject> _gameObjects = new List<GameObject>();
        private HashSet<GameObject> _gameObjectsToAdd = new HashSet<GameObject>();
        private HashSet<GameObject> _gameObjectsToDestroy = new HashSet<GameObject>();

        public delegate void SimpleDelegate();
        public event SimpleDelegate Initialized;
        
        public KadGame() {
            Instance = this;
            _time = new Time();
            _assetLoader = new AssetLoader();
            _physics = new Physics.Physics3D();
            
            _graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void Initialize() {
            base.Initialize();
            
            _assetLoader.LoadFromGameDataFolder();

            BasicEffect basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1.0f;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
            
            Initialized?.Invoke();
        }

        protected override void Update(GameTime gameTime)
        {
            // Update gameObjects collection
            if (_gameObjectsToAdd.Count > 0) {
                foreach (GameObject go in _gameObjectsToAdd) {
                    _gameObjects.Add(go);
                }
                _gameObjectsToAdd.Clear();
            }
            if (_gameObjectsToDestroy.Count > 0) {
                for (int i = _gameObjects.Count - 1; i >= 0; i--) {
                    if (_gameObjectsToDestroy.Contains(_gameObjects[i]))
                        _gameObjects.RemoveAt(i);
                }
                _gameObjectsToDestroy.Clear();
            }
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _time.Update(gameTime);
            _physics.Update();

            foreach (GameObject go in _gameObjects) {
                go.Update();
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_screenClearColor);

            foreach (GameObject go in _gameObjects) {
                go.Draw();
            }
            
            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing) {
                _assetLoader.UnloadAll();
                _physics.Dispose();
            }
        }

        public GameObject AddGameObject(GameObject gameObject) {
            _gameObjectsToAdd.Add(gameObject);
            return gameObject;
        }

        public void DestroyGameObject(GameObject gameObject) {
            _gameObjectsToDestroy.Add(gameObject);
        }
    }
}