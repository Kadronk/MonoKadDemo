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
        private float _shootCd = 0.5f;
        private float _throwForce = 15.0f;
        private float _shootTimer;

        public override void Update() {
            MouseState mouseState = Mouse.GetState();

            if (_shootTimer <= 0.0f && mouseState.LeftButton == ButtonState.Pressed) {
                if (Physics3D.Raycast(GameObject.Position, _camera.ViewForward, float.PositiveInfinity, out RayHit hit)) {
                    Can hitCan = hit.Rigidbody.GameObject.GetBehaviour<Can>();
                    if (hitCan != null)
                        hitCan.OnGunHit(_camera.ViewForward, hit);
                }
                
                // GameObject boxGo = KadGame.Instance.AddGameObject(new PhysicBoxObject(_camera.GameObject.Position));
                // boxGo.GetBehaviour<DynamicbodyBox>().AddForce(_camera.ViewForward * _throwForce);

                _shootTimer = _shootCd;
            }

            if (_shootTimer > 0.0f)
                _shootTimer -= Time.Delta;
        }
    }
}
