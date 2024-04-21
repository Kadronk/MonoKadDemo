using Microsoft.Xna.Framework;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class PlayerObject : GameObject
    {
        public PlayerObject() {
            Position = new Vector3(0.0f, 0.0f, -20.0f);
            Rotation = Quaternion.Identity;
            
            Camera cam = AddBehaviour<Camera>();
            AddBehaviour<PlayerMovement>();
            // AddBehaviour<NoClipMovement>();
            PlayerGun gun = AddBehaviour<PlayerGun>();
            gun.Camera = cam;
        }
    }   
}