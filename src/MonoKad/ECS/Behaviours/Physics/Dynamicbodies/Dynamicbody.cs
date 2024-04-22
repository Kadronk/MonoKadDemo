using BepuPhysics;
using Microsoft.Xna.Framework;
using MonoKad.Physics;

namespace MonoKad.Components
{
    public class Dynamicbody : Rigidbody
    {
        public float Mass => _mass;
        public Vector3 Velocity {
            get => _bodyRef.Velocity.Linear;
            set => _bodyRef.Velocity.Linear = value.ToNumerics();
        }
        
        protected float _mass = 1.0f; //TODO: masse changeable ?
        protected BodyReference _bodyRef;

        public override void Update() {
            if (_bodyRef.Exists) {
                if (_bodyRef.Awake == false && (_bodyRef.Velocity.Linear != System.Numerics.Vector3.Zero || _bodyRef.Velocity.Angular != System.Numerics.Vector3.Zero))
                    Physics3D.Simulation.Awakener.AwakenBody(_bodyRef.Handle);
                
                BodyPoseToGameObjectTransform(ref _bodyRef.Pose);
            }
        }

        public void AddForce(Vector3 force) {
            _bodyRef.ApplyLinearImpulse(force.ToNumerics());
        }

        public void AddTorque(Vector3 force) {
            _bodyRef.ApplyAngularImpulse(force.ToNumerics());
        }

        public void AddForceAtPosition(Vector3 force, Vector3 worldPosition) {
            _bodyRef.ApplyImpulse(force.ToNumerics(), worldPosition.ToNumerics());
        }
    }
}