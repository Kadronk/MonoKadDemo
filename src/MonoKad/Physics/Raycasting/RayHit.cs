using System.Numerics;
using MonoKad.Components;

namespace MonoKad.Physics
{
    public struct RayHit
    {
        public Rigidbody Rigidbody => Physics3D.Rigidbodies[CollidablePacked];

        public Vector3 Point;
        public Vector3 Normal;
        public float Distance;
        public uint CollidablePacked;
        public bool HasHit;
    }
}