using Microsoft.Xna.Framework;

namespace MonoKad
{
    public class Time
    {
        public static float Delta => KadGame.Instance.Time._delta;
        
        private float _delta;
        
        public void Update(GameTime gameTime) {
            _delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
