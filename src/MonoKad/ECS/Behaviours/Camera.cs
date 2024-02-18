using Microsoft.Xna.Framework;

namespace MonoKad.Components
{
    public class Camera : Behaviour
    {
        public Matrix ProjectionMatrix => _projectionMatrix;
        public Matrix ViewMatrix => _viewMatrix;
        
        private Matrix _projectionMatrix;
        private Matrix _viewMatrix = new Matrix(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);
        private Vector3 _viewForward;

        public override void Awake() {
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), KadGame.Instance.GraphicsDevice.DisplayMode.AspectRatio, 1.0f, 1000.0f);
            UpdateViewMatrix();
            UpdateViewForward();
            KadGame.Instance.CurrentCamera = this;
            GameObject.Rotated += UpdateViewMatrix;
            //GameObject.Positioned += UpdateViewMatrix;
            GameObject.Rotated += UpdateViewForward;
        }
        
        void UpdateViewMatrix() {
            Vector3 forward = GameObject.Forward;
            Vector3 right = GameObject.Right;
            Vector3 up = GameObject.Up; 
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
            // Constant components are set in the constructor
            // Note in case a cross product is needed again : Since Z- is forward in XNA, cross product is "right x forward" to keep Y+ as up.
        }

        public void UpdateViewForward() {
            _viewForward = -GameObject.Forward;
        }
    }   
}