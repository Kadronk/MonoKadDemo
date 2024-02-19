using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoKad.Components
{
    public class PlayerMovement : Behaviour
    {
        private float _eyeHeight = 1.7f;
        private float _gravity = -9.81f;

        private float _groundAccel = 2.0f;
        private float _groundMaxSpeed = 1.0f;
        private float _groundFriction = 1.0f;
        private float _turnSensibility = 0.1f;
        
        private Vector2 _movementInput;
        private Vector2 _mouseDelta;

        private Vector3 _velocity = Vector3.Zero;
        private bool _isGrounded = true;
        
        public override void Update() {
            UpdateInput();
            
            MoveInputToVelocity();
            TurnInputToRotation();
            
            ApplyGravity();
            if (_isGrounded)
                ApplyGroundFriction();
            ApplyMovement();
        }

        public void MoveInputToVelocity() {
            if (_movementInput == Vector2.Zero)
                return;
            
            Vector3 moveVec = Vector3.Transform(new Vector3(_movementInput.X, 0.0f, _movementInput.Y), GameObject.Rotation);
            moveVec.Y = 0.0f;
            moveVec.Normalize();

            if (_velocity.LengthSquared() < _groundMaxSpeed)
                _velocity += Time.Delta * _groundAccel * moveVec;
        }

        void TurnInputToRotation() {
            Vector2 turnVec = Time.Delta * _turnSensibility * _mouseDelta;
            GameObject.Rotation = Quaternion.CreateFromAxisAngle(Vector3.Up, turnVec.X) * GameObject.Rotation;
            GameObject.Rotation = Quaternion.CreateFromAxisAngle(GameObject.Right, turnVec.Y) * GameObject.Rotation; //Positive angle = counterclockwise!!!
        }

        private void UpdateInput() {
            KeyboardState kbState = Keyboard.GetState();
            _movementInput.X = (kbState.IsKeyDown(Keys.Q) ? -1.0f : 0.0f) + (kbState.IsKeyDown(Keys.D) ? 1.0f : 0.0f);
            _movementInput.Y = (kbState.IsKeyDown(Keys.S) ? -1.0f : 0.0f) + (kbState.IsKeyDown(Keys.Z) ? 1.0f : 0.0f);
            if (_movementInput.X != 0.0f || _movementInput.Y != 0.0f)
                _movementInput.Normalize();

            Point currentMousePos = Mouse.GetState().Position;
            Rectangle windowBounds = KadGame.Instance.GraphicsDevice.Viewport.Bounds;
            _mouseDelta.X = currentMousePos.X - windowBounds.Center.X;
            _mouseDelta.Y = currentMousePos.Y - windowBounds.Center.Y;
            
            Mouse.SetPosition(KadGame.Instance.GraphicsDevice.Viewport.Width / 2, KadGame.Instance.GraphicsDevice.Viewport.Height / 2);
        }

        void ApplyGravity() {
            _velocity.Y += _gravity * Time.Delta;
        }

        void ApplyGroundFriction() {
            if (_velocity == Vector3.Zero) return;
            
            float signX = MathF.Sign(_velocity.X);
            float signZ = MathF.Sign(_velocity.Z);
            
            _velocity -= _groundFriction * Time.Delta * Vector3.Normalize(_velocity);
            
            if (MathF.Sign(_velocity.X) != signX)
                _velocity.X = 0.0f;
            if (MathF.Sign(_velocity.Z) != signZ)
                _velocity.Z = 0.0f;
        }

        void ApplyMovement() {
            GameObject.Position += _velocity;

            if (GameObject.Position.Y < _eyeHeight && _velocity.Y < 0.0f) {
                GameObject.SetPositionY(_eyeHeight);
                _velocity.Y = 0.0f;
            }
            _isGrounded = GameObject.Position.Y <= _eyeHeight;
        }
    } 
}