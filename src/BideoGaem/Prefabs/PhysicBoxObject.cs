using System.Numerics;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class PhysicBoxObject : GameObject
    {
        public PhysicBoxObject() {
            AddBehaviour<RigidbodyBox>();
        }
    }
}