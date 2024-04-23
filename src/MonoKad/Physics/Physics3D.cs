using Microsoft.Xna.Framework;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.Constraints;
using BepuUtilities;
using BepuUtilities.Memory;
using MonoKad.Components;

namespace MonoKad.Physics
{
    public class Physics3D : IDisposable
    {
        public static Simulation Simulation => s_instance._simulation;
        public static Dictionary<uint, Rigidbody> Rigidbodies => s_instance._rigidbodies;

        private static Physics3D s_instance;
        
        private Simulation _simulation;
        private BufferPool _bufferPool;
        private ThreadDispatcher _threadDispatcher;

        private Dictionary<uint, Rigidbody> _rigidbodies = new Dictionary<uint, Rigidbody>();
        private ContactEventHandler _contactEventHandler;
        private ContactEvents _contactEvents;

        internal Physics3D() {
            s_instance = this;
            
            _bufferPool = new BufferPool();
            
            int targetThreadCount = int.Max(1, Environment.ProcessorCount > 4 ? Environment.ProcessorCount - 2 : Environment.ProcessorCount - 1); //copied from BepuPhysics demos
            _threadDispatcher = new ThreadDispatcher(targetThreadCount);
            
            _contactEvents = new ContactEvents(_threadDispatcher, _bufferPool, 640); // MAGIC NUMBER TIED TO THE NUMBER OF INITIAL CUBES, WATCH OUT

            NarrowPhaseCallbacks narrowPhaseCallbacks = new NarrowPhaseCallbacks(_contactEvents, new SpringSettings(35.0f, 0.0f));
            PoseIntegratorCallbacks poseIntegratorCallbacks = new PoseIntegratorCallbacks(new System.Numerics.Vector3(0.0f, -9.81f, 0.0f), 0.0f, 0.0f);
            _simulation = Simulation.Create(_bufferPool, narrowPhaseCallbacks, poseIntegratorCallbacks, new SolveDescription(2, 4));
            
            _contactEventHandler = new ContactEventHandler(_simulation, _bufferPool);
        }

        /// <summary> Physics is tied to framerate?? stinky maybe?? </summary>
        internal void Update() {
            if (Time.Delta > 0.0f)
                _simulation.Timestep(Time.Delta, _threadDispatcher);
        }
        
        internal static BodyReference AddBox(DynamicbodyBox rbBox) {
            Box newBox = new Box(rbBox.Size.X, rbBox.Size.Y, rbBox.Size.Z);
            
            TypedIndex boxIndex = s_instance._simulation.Shapes.Add(newBox);
            BodyDescription bodyDescription = BodyDescription.CreateDynamic(new RigidPose(rbBox.GameObject.Position.ToNumerics(), rbBox.GameObject.Rotation.ToNumerics()), newBox.ComputeInertia(rbBox.Mass), new CollidableDescription(boxIndex, ContinuousDetection.Continuous()), 0.01f);
            
            BodyHandle handle = s_instance._simulation.Bodies.Add(bodyDescription);
            BodyReference reference = s_instance._simulation.Bodies[handle];
            s_instance._rigidbodies.Add(reference.CollidableReference.Packed, rbBox);
            
            s_instance._contactEvents.Register(reference.CollidableReference, s_instance._contactEventHandler);
            
            return reference;
        }

        internal static StaticReference AddMeshStatic(StaticbodyMesh rbMesh, out TypedIndex meshIndex) {
            Mesh mesh = rbMesh.Mesh;
            s_instance._bufferPool.Take(mesh.TriangleCount, out Buffer<Triangle> triangles);
            
            for (int i = 0; i < mesh.TriangleCount; i++) {
                // Winding order must be reversed: BepuPhysics triangle normal direction is reverse of renderers
                ref Triangle triangle = ref triangles[i]; 
                triangle.C = mesh.Vertices[mesh.VertexIndices[i * 3]].ToNumerics();
                triangle.B = mesh.Vertices[mesh.VertexIndices[i * 3 + 1]].ToNumerics();
                triangle.A = mesh.Vertices[mesh.VertexIndices[i * 3 + 2]].ToNumerics();
            }
            
            BepuPhysics.Collidables.Mesh newMesh = new BepuPhysics.Collidables.Mesh(triangles, System.Numerics.Vector3.One, s_instance._bufferPool);
            meshIndex = s_instance._simulation.Shapes.Add(newMesh);
            StaticDescription staticDescription = new StaticDescription(rbMesh.GameObject.Position.ToNumerics(), rbMesh.GameObject.Rotation.ToNumerics(), meshIndex, ContinuousDetection.Discrete);

            StaticHandle handle = s_instance._simulation.Statics.Add(staticDescription);
            StaticReference reference = s_instance._simulation.Statics[handle];
            s_instance._rigidbodies.Add(reference.CollidableReference.Packed, rbMesh);
            
            s_instance._contactEvents.Register(reference.CollidableReference, s_instance._contactEventHandler);
            
            return reference;
        }

        internal static void RemoveShape(TypedIndex typedIndex) {
            s_instance._simulation.Shapes.RemoveAndDispose(typedIndex, s_instance._bufferPool);
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, float distance, out RayHit hit) {
            s_instance._bufferPool.Take(1, out Buffer<RayHit> results);
            for (int i = 0; i < results.Length; i++) {
                results[i].Distance = float.MaxValue;
                results[i].HasHit = false;
            }
            
            HitHandler hitHandler = new HitHandler { Hits = results };

            direction.Normalize();
            s_instance._simulation.RayCast(origin.ToNumerics(), direction.ToNumerics(), distance, ref hitHandler);

            hit = hitHandler.Hits[0];
            return hitHandler.Hits[0].HasHit;
        }
        
        public void Dispose() {
            _simulation.Dispose();
            _bufferPool.Clear();
            _threadDispatcher.Dispose();
        }
    }
}
