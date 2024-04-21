using Microsoft.Xna.Framework;
using MonoKad.Physics;

namespace MonoKad.Components
{
    public class DynamicbodyBox : Dynamicbody
    {
        public Vector3 Size => _size;
        
        private Vector3 _size = new Vector3(1.0f, 1.0f, 1.0f); //TODO: taille changeable

        // private Random _random = new Random();
        
        public override void Awake() {
            _bodyRef = Physics3D.AddBox(this);
        }
        
        // public override void Update() {
        //     base.Update();
        //
        //     if (GameObject.Position.Y < 0.0f) {
        //         _bodyRef.ApplyLinearImpulse(new System.Numerics.Vector3(_random.NextSingle() * 2.0f - 1.0f, 1f, _random.NextSingle() * 2.0f - 1.0f));
        //     }
        // }
    }
}