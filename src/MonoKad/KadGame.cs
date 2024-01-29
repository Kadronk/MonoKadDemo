using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoKad
{
    public class KadGame : Game
    {
        public static KadGame Instance;
        
        private GraphicsDeviceManager _graphics;
        private BasicEffect _basicEffect; // TODO idem
        
        private FunnyTriangle _triangle;
        private Camera _cam;

        public KadGame() {
            Instance = this;
            
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

            _cam = new Camera();
            _cam.Position = new Vector3(0.0f, 0.0f, 20.0f);
            _triangle = new FunnyTriangle(_basicEffect);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _cam.Update(gameTime);
            _triangle.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _basicEffect.Projection = _cam.ProjectionMatrix;
            _basicEffect.View = _cam.ViewMatrix;
            
            _triangle.Draw(gameTime);
            
            base.Draw(gameTime);
        }
    }
}