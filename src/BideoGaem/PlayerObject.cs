using Microsoft.Xna.Framework;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class PlayerObject : GameObject
    {
        public PlayerObject() {
            Position = new Vector3(0.0f, 0.0f, 20.0f);
            Rotation = Quaternion.CreateFromAxisAngle(new Vector3(0.0f, 0.0f, -1.0f), 0.0f);
            
            AddBehaviour<MovementController>();
            AddBehaviour<Camera>();
        }
    }   
}