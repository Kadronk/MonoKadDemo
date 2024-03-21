using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoKad.Components
{
    public class PlayerMovement : Behaviour
    {
        public Vector3 HorizontalVelocity {
            get => new Vector3(_velocity.X, 0.0f, _velocity.Z);
            set {
                _velocity.X = value.X;
                _velocity.Z = value.Z;
            }
        }
        
        private float _eyeHeight = 1.7f;
        private float _gravity = -9.81f;

        private float _groundAccel = 250.0f;
        private float _groundMaxSpeed = 15.0f;
        private float _groundFriction = 100.0f;
        private float _turnSensibility = 0.1f;

        private float _jumpForce = 6.0f;
        
        private Vector2 _movementInput;
        private Vector2 _mouseDelta;
        private bool _wantsToJump;

        private Vector3 _velocity = Vector3.Zero;
        private bool _isGrounded = true;
        
        public override void Update() {
            UpdateInput();
            
            TurnInputToRotation();
            MoveInputToVelocity();
            
            ApplyGravity();
            if (_isGrounded) {
                ApplyGroundFriction();
                if (_wantsToJump)
                    Jump();
            }
            HorizontalVelocity = VectorEx.ClampMagnitude(HorizontalVelocity, _groundMaxSpeed);
            ApplyMovement();
            Console.WriteLine(GameObject.Rotation.Length());
        }

        public void MoveInputToVelocity() {
            if (_movementInput == Vector2.Zero)
                return;
            
            Vector3 moveVec = Vector3.Transform(new Vector3(_movementInput.X, 0.0f, _movementInput.Y), GameObject.Rotation);
            moveVec.Y = 0.0f;
            moveVec.Normalize();

            _velocity += _groundAccel * Time.Delta * moveVec;
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

            _wantsToJump = kbState.IsKeyDown(Keys.Space);
            
            Mouse.SetPosition(KadGame.Instance.GraphicsDevice.Viewport.Width / 2, KadGame.Instance.GraphicsDevice.Viewport.Height / 2);
        }

        void ApplyGravity() {
            _velocity.Y += _gravity * Time.Delta;
        }

        void ApplyGroundFriction() {
            if (_velocity == Vector3.Zero) return;
            
            // float signX = MathF.Sign(_velocity.X);
            // float signZ = MathF.Sign(_velocity.Z);

            Vector3 normalizedVel = Vector3.Normalize(_velocity);
            _velocity.X -= _groundFriction * Time.Delta * normalizedVel.X;
            _velocity.Z -= _groundFriction * Time.Delta * normalizedVel.Z;

            if (HorizontalVelocity.LengthSquared() < 0.03f * 0.03f) {
                _velocity.X = 0.0f;
                _velocity.Z = 0.0f;
            }
            // if (MathF.Sign(_velocity.X) != signX)
            //     _velocity.X = 0.0f;
            // if (MathF.Sign(_velocity.Z) != signZ)
            //     _velocity.Z = 0.0f;
        }

        void ApplyMovement() {
            GameObject.Position += _velocity * Time.Delta;

            if (GameObject.Position.Y < _eyeHeight && _velocity.Y < 0.0f) {
                GameObject.SetPositionY(_eyeHeight);
                _velocity.Y = 0.0f;
            }
            _isGrounded = GameObject.Position.Y <= _eyeHeight;
        }

        void Jump() {
            _velocity.Y = _jumpForce;
        }
    } 
}