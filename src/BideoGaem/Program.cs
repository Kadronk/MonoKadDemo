using Microsoft.Xna.Framework;
using MonoKad;

namespace BideoGaem
{
    class Program
    {
        static void Main() {
            using (KadGame game = new KadGame()) {
                game.Initialized += () => {
                    game.AddGameObject(new PlayerObject());
                    game.AddGameObject(new ColosseumObject());
                    game.AddGameObject(new FunnyTriangleObject(Vector3.Up));
                };
                KadGame.Instance.ScreenClearColor = Color.Crimson; //Firebrick is okay too
                
                game.Run();
            }
        }
    }
}