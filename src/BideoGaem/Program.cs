using MonoKad;

namespace BideoGaem
{
    class Program
    {
        static void Main() {
            using (KadGame game = new KadGame()) {
                game.AddGameObject(new PlayerObject());
                game.Run();
            }
        }
    }
}