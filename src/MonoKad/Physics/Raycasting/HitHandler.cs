using System.Numerics;
using System.Runtime.CompilerServices;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.Trees;
using BepuUtilities.Memory;
using MonoKad.Components;

namespace MonoKad.Physics
{
    struct HitHandler : IRayHitHandler
    {
        public Buffer<RayHit> Hits;
        public Dictionary<uint, Rigidbody> AllRigidbodies;
        public int IntersectionCount;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllowTest(CollidableReference collidable) {
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AllowTest(CollidableReference collidable, int childIndex) {
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnRayHit(in RayData ray, ref float maximumT, float t, Vector3 normal, CollidableReference collidable, int childIndex) {
            maximumT = t;
            ref RayHit hit = ref Hits[ray.Id];
            if (t < hit.Distance)
            {
                if (hit.Distance == float.MaxValue)
                     ++IntersectionCount;
                hit.Point = ray.Origin + ray.Direction * t;
                hit.Normal = normal;
                hit.Distance = t;
                hit.CollidablePacked = collidable.Packed;
                hit.HasHit = true;
            }
        }
    }
}