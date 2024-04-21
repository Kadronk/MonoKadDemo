using Microsoft.Xna.Framework.Input;
using MonoKad.Components;
using MonoKad.Physics;

namespace BideoGaem
{
    public class PlayerGun : Behaviour
    {
        public Camera Camera { set => _camera = value; }
        
        private Camera _camera;

        public override void Update() {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed) {
                Physics3D.Raycast(GameObject.Position, _camera.ViewForward, float.PositiveInfinity);
            }
        }
    }
}
