using Microsoft.Xna.Framework;
using MonoKad.Physics;

namespace MonoKad.Components
{
    public class RigidbodyBox : Behaviour
    {
        public Vector3 Size { init => _size = value; }
        
        private Vector3 _size = new Vector3(0.5f, 1.5f, 0.5f); //TODO: taille changeable
        
        public override void Awake() {
            Physics3D.AddBox(GameObject.Position, GameObject.Rotation, _size, 1.0f);
        }
    }
}