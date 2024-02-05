using Microsoft.Xna.Framework;

namespace MonoKad.Components
{
    public class Camera : Behaviour
    {
        public Matrix ProjectionMatrix => _projectionMatrix;
        public Matrix ViewMatrix => _viewMatrix;
        
        private Matrix _projectionMatrix;
        private Matrix _viewMatrix = new Matrix(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

        public Camera() {
            //_projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), KadGame.Instance.GraphicsDevice.DisplayMode.AspectRatio, 1.0f, 1000.0f);
            //UpdateViewMatrix();
            KadGame.Instance.CurrentCamera = this;
        }

        public override void Update() {
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), KadGame.Instance.GraphicsDevice.DisplayMode.AspectRatio, 1.0f, 1000.0f); // TEMPORARY !!!!!! TODO: after the to-do in Component is done put this back in initialization
            UpdateViewMatrix();
        }

        // TODO: cache transform forward, transform up, transform right
        void UpdateViewMatrix() {
            Vector3 forward = Vector3.Transform(Vector3.Backward, GameObject.Rotation); //WARNING: i had to put backward here, i need to know if positive Z is really forward or not
            Vector3 right = Vector3.Transform(Vector3.Right, GameObject.Rotation);
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
            _viewMatrix.M41 = -Vector3.Dot(right, GameObject.Position);
            _viewMatrix.M42 = -Vector3.Dot(up, GameObject.Position);
            _viewMatrix.M43 = -Vector3.Dot(forward, GameObject.Position);
            //constant components are set in the constructor
        }
    }   
}