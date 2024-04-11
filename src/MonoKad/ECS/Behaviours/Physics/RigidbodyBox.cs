using System.Diagnostics;
using BepuPhysics;
using Microsoft.Xna.Framework;
using MonoKad.Physics;

namespace MonoKad.Components
{
    public class RigidbodyBox : Behaviour
    {
        private Vector3 _size = new Vector3(1.0f, 1.0f, 1.0f); //TODO: taille changeable
        private float _mass = 1.0f; //pareil

        private BodyHandle _bepuHandle;
        private Random _random = new Random();
        
        public override void Awake() {
            _bepuHandle = Physics3D.AddBox(GameObject.Position, GameObject.Rotation, _size, _mass);
        }

        public override void Update() {
            BodyReference body = Physics3D.Simulation.Bodies[_bepuHandle];
            
            if (body.Awake == false && (body.Velocity.Linear != System.Numerics.Vector3.Zero || body.Velocity.Angular != System.Numerics.Vector3.Zero))
                Physics3D.Simulation.Awakener.AwakenBody(_bepuHandle);
            
            GameObject.Position = body.Pose.Position;
            GameObject.Rotation = body.Pose.Orientation;

            if (GameObject.Position.Y < 0.0f) {
                body.ApplyLinearImpulse(new System.Numerics.Vector3(_random.NextSingle() * 2.0f - 1.0f, 1f, _random.NextSingle() * 2.0f - 1.0f));
            }
        }
    }
}