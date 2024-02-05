using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoKad.Components
{
    public class MovementController : Behaviour
    {
        private float _speed = 10.0f;
        private Vector3 _movementInput;
        
        public override void Update() {
            UpdateMovementInput();
            Move(Time.Delta * _speed * _movementInput);
        }

        public void Move(Vector3 vector) {
            GameObject.Position += vector;
        }

        private void UpdateMovementInput() {
            KeyboardState kbState = Keyboard.GetState();
            _movementInput.X = (kbState.IsKeyDown(Keys.Q) ? -1 : 0) + (kbState.IsKeyDown(Keys.D) ? 1 : 0);
            _movementInput.Y = (kbState.IsKeyDown(Keys.Q) ? -1 : 0) + (kbState.IsKeyDown(Keys.D) ? 1 : 0);
            _movementInput.Normalize();
        }
    }
}
