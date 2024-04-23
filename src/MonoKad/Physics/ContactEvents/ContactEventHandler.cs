using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.CollisionDetection;
using BepuUtilities.Collections;
using BepuUtilities.Memory;
using Microsoft.Xna.Framework;
using MonoKad.Components;

namespace MonoKad.Physics
{
    class ContactEventHandler : IContactEventHandler
    {
        public Simulation Simulation;
        public BufferPool Pool;

        public ContactEventHandler(Simulation simulation, BufferPool pool)
        {
            Simulation = simulation;
            Pool = pool;
        }

        public void OnContactAdded<TManifold>(CollidableReference eventSource, CollidablePair pair, ref TManifold contactManifold,
            Vector3 contactOffset, Vector3 contactNormal, float depth, int featureId, int contactIndex, int workerIndex) where TManifold : unmanaged, IContactManifold<TManifold> {
            Rigidbody rbA = Physics3D.Rigidbodies[pair.A.Packed];
            Rigidbody rbB = Physics3D.Rigidbodies[pair.B.Packed];
            rbA.InvokeContactedAdded(rbB);
            rbB.InvokeContactedAdded(rbA);
        }

        public void Dispose()
        {
            //In the demo we won't actually call this, since it's going to persist until the demo dies. At that point, the buffer pool will be dropped and all its allocations will be cleaned up anyway.
        }
    }
}
