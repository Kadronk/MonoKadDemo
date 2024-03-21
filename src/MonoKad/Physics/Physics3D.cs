using Microsoft.Xna.Framework;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.Constraints;
using BepuUtilities;
using BepuUtilities.Memory;

namespace MonoKad.Physics
{
    public class Physics3D : IDisposable
    {
        private Simulation _simulation;
        private BufferPool _bufferPool;
        private ThreadDispatcher _threadDispatcher;

        internal Physics3D() {
            _bufferPool = new BufferPool();
            
            int targetThreadCount = int.Max(1, Environment.ProcessorCount > 4 ? Environment.ProcessorCount - 2 : Environment.ProcessorCount - 1); //copied from BepuPhysics demos
            _threadDispatcher = new ThreadDispatcher(targetThreadCount);

            NarrowPhaseCallbacks narrowPhaseCallbacks = new NarrowPhaseCallbacks(new SpringSettings(1.0f, 0.5f));
            PoseIntegratorCallbacks poseIntegratorCallbacks = new PoseIntegratorCallbacks(new System.Numerics.Vector3(0.0f, -9.81f, 0.0f));
            _simulation = Simulation.Create(_bufferPool, narrowPhaseCallbacks, poseIntegratorCallbacks, new SolveDescription(4, 1));
        }

        /// <summary> Physics is tied to framerate?? stinky maybe?? </summary>
        internal void Update() {
            _simulation.Timestep(Time.Delta);
        }

        public static void AddBox(Vector3 position, Quaternion rotation, Vector3 size, float mass) {
            Simulation simulation = KadGame.Instance.Physics._simulation;
            Box newBox = new Box(size.X, size.Y, size.Z);
            TypedIndex index = simulation.Shapes.Add(newBox);
            BodyHandle importantHandle = simulation.Bodies.Add(BodyDescription.CreateDynamic(new RigidPose(position.ToNumerics(), rotation.ToNumerics()), newBox.ComputeInertia(mass), index, 0.01f));
        }
        
        public void Dispose() {
            _simulation.Dispose();
            _bufferPool.Clear();
            _threadDispatcher.Dispose();
        }
    }
}
