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
        private Matrix _viewMatrix = new Matrix(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

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
            if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                Rotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.ToRadians(50.0f * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E)) {
                Rotation *= Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.ToRadians(-50.0f * (float)gameTime.ElapsedGameTime.TotalSeconds));
            }
            Position = newPos;
            
            UpdateViewMatrix();
        }

        // TODO: cache transform forward, transform up, transform right
        void UpdateViewMatrix() {
            Vector3 forward = Vector3.Transform(Vector3.Backward, Rotation); //TODO: warning i had to put backward here, i need to know if positive Z is really forward or not
            Vector3 right = Vector3.Transform(Vector3.Right, Rotation);
            Vector3 up = Vector3.Cross(forward, right);
            _viewMatrix.M11 = right.X;
            _viewMatrix.M12 = up.X;
            _viewMatrix.M13 = forward.X;
            _viewMatrix.M21 = right.Y;
            _viewMatrix.M22 = up.Y;
            _viewMatrix.M23 = forward.Y;
            _viewMatrix.M31 = right.Z;
            _viewMatrix.M32 = up.Z;
            _viewMatrix.M33 = forward.Z;
            _viewMatrix.M41 = -Vector3.Dot(right, Position);
            _viewMatrix.M42 = -Vector3.Dot(up, Position);
            _viewMatrix.M43 = -Vector3.Dot(forward, Position);
            //constant components are set in the constructor
        }
    }   
}