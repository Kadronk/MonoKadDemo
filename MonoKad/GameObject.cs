using Microsoft.Xna.Framework;

namespace MonoKad
{
    public class GameObject
    {
        public Vector3 Position { get => _position; set => _position = value; }
        public Quaternion Rotation { get => _rotation; set => _rotation = value; }

        private Vector3 _position;
        private Quaternion _rotation;
        
        public virtual void Update(GameTime gameTime) { }
        
        public virtual void Draw(GameTime gameTime) { }

        public Matrix GetTransformMatrix() {
            return Matrix.CreateTranslation(_position) * Matrix.CreateFromQuaternion(_rotation);;
        }
    }
}