using Microsoft.Xna.Framework;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class FunnyTriangleObject : GameObject
    {
        public FunnyTriangleObject() {
            Position = Vector3.Zero;
            Rotation = Quaternion.Identity;
        
            AddRenderer<TriangleRenderer>();
        }
    }   
}