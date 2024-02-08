using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoKad.Components
{
    public class MovementController : Behaviour
    {
        private float _speed = 10.0f;
        private float _turnSensibility = 1.0f;
        private Vector3 _movementInput;
        private Vector2 _turnInput;
        
        public override void Update() {
            UpdateInput();
            Move(Vector3.Transform(Time.Delta * _speed * _movementInput, GameObject.Rotation));
            Turn(Time.Delta * _turnSensibility * _turnInput);
        }

        public void Move(Vector3 vector) {
            GameObject.Position += vector;
        }

        void Turn(Vector2 vector) {
            GameObject.Rotation = Quaternion.CreateFromAxisAngle(Vector3.Up, vector.X) * GameObject.Rotation;
            GameObject.Rotation = Quaternion.CreateFromAxisAngle(GameObject.Right, -vector.Y) * GameObject.Rotation; //Positive angle = counterclockwise!!!
        }

        private void UpdateInput() {
            KeyboardState kbState = Keyboard.GetState();
            _movementInput.X = (kbState.IsKeyDown(Keys.Q) ? -1.0f : 0.0f) + (kbState.IsKeyDown(Keys.D) ? 1.0f : 0.0f);
            _movementInput.Z = (kbState.IsKeyDown(Keys.S) ? -1.0f : 0.0f) + (kbState.IsKeyDown(Keys.Z) ? 1.0f : 0.0f);
            if (_movementInput.LengthSquared() > 0.0f)
                _movementInput.Normalize();
            _turnInput.X = (kbState.IsKeyDown(Keys.Left) ? -1.0f : 0.0f) + (kbState.IsKeyDown(Keys.Right) ? 1.0f : 0.0f);
            _turnInput.Y = (kbState.IsKeyDown(Keys.Down) ? -1.0f : 0.0f) + (kbState.IsKeyDown(Keys.Up) ? 1.0f : 0.0f);
        }
    }
}
