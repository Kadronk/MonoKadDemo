using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoKad
{
    public class Camera : GameObject
    {
        public Matrix ProjectionMatrix => _projectionMatrix;
        public Matrix ViewMatrix => _viewMatrix;
        
        private Matrix _projectionMatrix;
        private Matrix _viewMatrix;

        public Camera() {
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), KadGame.Instance.GraphicsDevice.DisplayMode.AspectRatio, 1.0f, 1000.0f);
            UpdateViewMatrix();
        }

        public override void Update(GameTime gameTime) {
            Vector3 newPos = Position;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                newPos.X -= 10.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                newPos.X += 10.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                newPos.Y += 10.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                newPos.Y -= 10.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus)) {
                newPos.Z += 10.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus)) {
                newPos.Z -= 10.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            Position = newPos;
            
            UpdateViewMatrix();
        }

        void UpdateViewMatrix() {
            _viewMatrix = Matrix.CreateTranslation(Position) * Matrix.CreateFromQuaternion(Rotation);
        }
    }   
}