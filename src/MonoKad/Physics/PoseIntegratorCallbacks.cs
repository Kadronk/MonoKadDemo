using System.Numerics;
using BepuPhysics;
using BepuUtilities;

namespace MonoKad.Physics
{
    public struct PoseIntegratorCallbacks : IPoseIntegratorCallbacks
    {
        public AngularIntegrationMode AngularIntegrationMode => AngularIntegrationMode.Nonconserving;
        public bool AllowSubstepsForUnconstrainedBodies => false;
        public bool IntegrateVelocityForKinematics => false;
        
        public Vector3 Gravity;
        public float LinearDamping;
        public float AngularDamping;

        public PoseIntegratorCallbacks(Vector3 gravity, float linearDamping = .03f, float angularDamping = .03f) : this()
        {
            Gravity = gravity;
            LinearDamping = linearDamping;
            AngularDamping = angularDamping;
        }

        public void Initialize(Simulation simulation) { }

        public void PrepareForIntegration(float dt) {
            //If physics delta time is fixed, delta-timed gravity and damping can be cached here
        }

        public void IntegrateVelocity(Vector<int> bodyIndices, Vector3Wide position, QuaternionWide orientation, BodyInertiaWide localInertia, Vector<int> integrationMask, int workerIndex, Vector<float> dt, ref BodyVelocityWide velocity) {
            velocity.Linear = (velocity.Linear + Vector3Wide.Broadcast(Gravity * dt[0])) * (LinearDamping * dt);
            velocity.Angular = velocity.Angular * (AngularDamping * dt);
        }
    }
}
