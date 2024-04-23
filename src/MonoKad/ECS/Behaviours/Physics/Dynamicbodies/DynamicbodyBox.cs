using Microsoft.Xna.Framework;
using MonoKad.Physics;

namespace MonoKad.Components
{
    public class DynamicbodyBox : Dynamicbody
    {
        public Vector3 Size => _size;
        
        private Vector3 _size = new Vector3(1.0f, 1.0f, 1.0f); //TODO: taille changeable
        
        public override void Awake() {
            _bodyRef = Physics3D.AddBox(this);
        }
    }
}