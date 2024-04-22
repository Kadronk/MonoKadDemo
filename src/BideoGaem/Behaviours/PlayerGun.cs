using Microsoft.Xna.Framework.Input;
using MonoKad;
using MonoKad.Components;
using MonoKad.Physics;

namespace BideoGaem
{
    public class PlayerGun : Behaviour
    {
        public Camera Camera { set => _camera = value; }
        
        private Camera _camera;
        private float _throwForce = 15.0f;

        public override void Update() {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed) {
                if (Physics3D.Raycast(GameObject.Position, _camera.ViewForward, float.PositiveInfinity, out RayHit hit)) {
                    Can hitCan = hit.Rigidbody.GameObject.GetBehaviour<Can>();
                    if (hitCan != null)
                        hitCan.OnGunHit(_camera.ViewForward, hit);
                }
                
                // GameObject boxGo = KadGame.Instance.AddGameObject(new PhysicBoxObject(_camera.GameObject.Position));
                // boxGo.GetBehaviour<DynamicbodyBox>().AddForce(_camera.ViewForward * _throwForce);
            }
        }
    }
}
