using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoKad.Components;

namespace MonoKad
{
    public class KadGame : Game
    {
        public static KadGame Instance;
        
        public Time Time => _time;
        public BasicEffect BasicEffect => _basicEffect;
        public Camera CurrentCamera { set => _currentCamera = value; }
        
        private Time _time;
        
        private GraphicsDeviceManager _graphics;
        private BasicEffect _basicEffect;
        private Camera _currentCamera;

        private List<GameObject> _gameObjects = new List<GameObject>();
        private HashSet<GameObject> _gameObjectsToAdd = new HashSet<GameObject>();
        private HashSet<GameObject> _gameObjectsToDestroy = new HashSet<GameObject>();

        public KadGame() {
            Instance = this;
            _time = new Time();
            
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
            
            _basicEffect = new BasicEffect(GraphicsDevice);
            _basicEffect.Alpha = 1.0f;
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.LightingEnabled = false;
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _time.Update(gameTime);

            foreach (GameObject go in _gameObjects) {
                go.Update();
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _basicEffect.Projection = _currentCamera.ProjectionMatrix;
            _basicEffect.View = _currentCamera.ViewMatrix;

            foreach (GameObject go in _gameObjects) {
                go.Draw();
            }
            
            base.Draw(gameTime);
            
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
        }

        public void AddGameObject(GameObject gameObject) { // TEMPORARY !!!!!!!!!!!!!
            _gameObjects.Add(gameObject);
        }
    }
}