using Microsoft.Xna.Framework;
using MonoKad.Components;

namespace MonoKad
{
    public class GameObject
    {
        public Vector3 Position {
            get => _position;
            set {
                _position = value;
                UpdateTransformMatrix();
            }
        }
        public Quaternion Rotation {
            get => _rotation;
            set {
                _rotation = Quaternion.Normalize(value);
                _forward = Vector3.Transform(Vector3.Forward, value);
                _right = Vector3.Transform(Vector3.Right, value);
                _up = Vector3.Transform(Vector3.Up, value);
                UpdateTransformMatrix();
                Rotated?.Invoke();
            }
        }
        public Vector3 Forward => _forward;
        public Vector3 Right => _right;
        public Vector3 Up => _up;
        public Matrix TransformMatrix => _transformMatrix;

        // Transform
        private Vector3 _position = Vector3.Zero;
        private Quaternion _rotation = Quaternion.Identity;
        private Vector3 _forward = Vector3.Forward;
        private Vector3 _right = Vector3.Right;
        private Vector3 _up = Vector3.Up;
        private Matrix _transformMatrix = Matrix.Identity;

        private List<Behaviour> _behaviours = new List<Behaviour>(); //Hashset ?
        private List<Renderer> _renderers = new List<Renderer>(); //Hashset ?
        
        public event KadGame.SimpleDelegate Rotated;

        public void Update() {
            foreach (Behaviour c in _behaviours) {
                if (c.Enabled)
                    c.Update();
            }
        }

        public void Draw() {
            foreach (Renderer r in _renderers) {
                r.Draw();
            }
        }

        void UpdateTransformMatrix() {
            _transformMatrix = Matrix.CreateScale(1.0f) * Matrix.CreateFromQuaternion(_rotation) * Matrix.CreateTranslation(_position);
        }

        public void SetPositionX(float x) {
            _position.X = x;
        }
        
        public void SetPositionY(float y) {
            _position.Y = y;
        }
        
        public void SetPositionZ(float z) {
            _position.Z = z;
        }

        public T AddBehaviour<T>() where T : Behaviour, new() {
            T bhvr = new T() { GameObject = this };
            _behaviours.Add(bhvr);
            bhvr.Awake();
            return bhvr;
        }

        public T AddRenderer<T>() where T : Renderer, new() {
            T rndr = new T() { GameObject = this };
            _renderers.Add(rndr);
            rndr.Awake();
            return rndr;
        }
    }
}