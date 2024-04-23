using BepuPhysics;
using Microsoft.Xna.Framework;

namespace MonoKad.Components
{
    public abstract class Rigidbody : Behaviour
    {
        public delegate void ContactDelegate(Rigidbody other);
        public event ContactDelegate ContactAdded;

        internal void InvokeContactedAdded(Rigidbody other) {
            ContactAdded?.Invoke(other);
        }
        
        protected void BodyPoseToGameObjectTransform(ref RigidPose pose) {
            GameObject.Position = pose.Position;
            GameObject.Rotation = pose.Orientation;
        }

        protected void GameObjectTransformToBodyPose(ref RigidPose pose) {
            pose.Position = GameObject.Position.ToNumerics();
            pose.Orientation = GameObject.Rotation.ToNumerics();
        }
    }
}
