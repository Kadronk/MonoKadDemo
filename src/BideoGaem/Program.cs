using MonoKad;

namespace BideoGaem
{
    class Program
    {
        static void Main() {
            using (KadGame game = new KadGame()) {
                game.Initialized += () => {
                    game.AddGameObject(new PlayerObject());
                    game.AddGameObject(new FunnyTriangleObject());
                };
                
                game.Run();
            }
        }
    }
}