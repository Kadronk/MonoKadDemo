using BepuPhysics;
using MonoKad.Physics;

namespace MonoKad.Components
{
    public class Dynamicbody : Rigidbody
    {
        public float Mass => _mass;
        
        protected float _mass = 1.0f; //TODO: masse changeable ?
        protected BodyReference _bodyRef;

        public override void Update() {
            if (_bodyRef.Exists) {
                if (_bodyRef.Awake == false && (_bodyRef.Velocity.Linear != System.Numerics.Vector3.Zero || _bodyRef.Velocity.Angular != System.Numerics.Vector3.Zero))
                    Physics3D.Simulation.Awakener.AwakenBody(_bodyRef.Handle);
                
                BodyPoseToGameObjectTransform(ref _bodyRef.Pose);
            }
        }
    }
}