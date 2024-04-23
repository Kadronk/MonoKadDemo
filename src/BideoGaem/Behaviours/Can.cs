using Microsoft.Xna.Framework;
using MonoKad;
using MonoKad.Components;
using MonoKad.Physics;

namespace BideoGaem
{
    public class Can : Behaviour
    {
        private float _maxKbHorizontal = 6.0f;
        private float _minKbVertical = 10.0f;
        private float _maxKbVertical = 25.0f;
        private float _spinStrength = 0.01f;
        private Dynamicbody _dBody;

        public override void Update() {
            if (GameObject.Position.Y < CanGameManager.Instance.TpYLevel) {
                CanGameManager.Instance.OnCanLanded();
                _dBody.Position = Vector3.Up;
                _dBody.Velocity = Vector3.Zero;
            }
        }

        public void SetDynamicBody(Dynamicbody dBody) {
            if (_dBody != null)
                _dBody.ContactAdded -= OnCollision;
            _dBody = dBody;
            _dBody.ContactAdded += OnCollision;
        }
        
        public void OnGunHit(Vector3 srcDirection, RayHit hit) {
            _dBody.AddForceAtPosition( srcDirection * _spinStrength, hit.Point);
            _dBody.Velocity = GetRandomKnockback();
            
            CanGameManager.Instance.OnCanShot();
        }

        void OnCollision(Rigidbody other) {
            if (other.GameObject.GetType() == typeof(ColosseumObject))
                CanGameManager.Instance.OnCanLanded();
        }

        Vector3 GetRandomKnockback() {
            Random rnd = KadGame.Instance.Random;
            
            float x = rnd.NextSingle() * _maxKbHorizontal;
            if (rnd.Next(2) == 0)
                x *= -1.0f;
            
            float z = rnd.NextSingle() * _maxKbHorizontal;
            if (rnd.Next(2) == 0)
                z *= -1.0f;

            float y = KadGame.Instance.Random.NextSingle() * (_maxKbHorizontal - _maxKbHorizontal) + _minKbVertical;
            
            return new Vector3(x, y, z);
        }
    }
}