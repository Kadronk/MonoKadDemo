using System.Numerics;
using BepuPhysics.Collidables;
using MonoKad.Physics;

namespace MonoKad.Components
{
    public class StaticbodyMesh : Staticbody
    {
        public Mesh Mesh => _mesh;

        private Mesh _mesh;
        private TypedIndex _bepuMeshIndex;

        public void InitMesh(Mesh mesh) {
            _mesh = mesh;
            if (_bepuMeshIndex.Exists) // kinda useless
                Physics3D.RemoveShape(_bepuMeshIndex);
            _staticRef = Physics3D.AddMeshStatic(this, out _bepuMeshIndex);
        }

        public override void Update() {
            _staticRef.Pose.Position = Vector3.Zero;
            
            GameObjectTransformToBodyPose(ref _staticRef.Pose);
        }
    }
}