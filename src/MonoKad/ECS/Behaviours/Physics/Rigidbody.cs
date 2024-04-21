using BepuPhysics;

namespace MonoKad.Components
{
    public abstract class Rigidbody : Behaviour
    {
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
