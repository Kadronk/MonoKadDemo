using Microsoft.Xna.Framework;
using MonoKad;

namespace BideoGaem
{
    class Program
    {
        static void Main() {
            using (KadGame game = new KadGame()) {
                game.Initialized += () =>
                {
                    game.AddGameObject(new GameObject()).AddBehaviour<CanGameManager>();
                    game.AddGameObject(new PlayerObject());
                    game.AddGameObject(new ColosseumObject());
                    game.AddGameObject(new DaedalusObject(new Vector3(5.0f, 1.0f, 0.0f)));
                    for (int i = 0; i < 1; i++) {
                        for (int j = 0; j < 1; j++) {
                            for (int k = 0; k < 1; k++) {
                                game.AddGameObject(new PhysicBoxObject(new Vector3(j * 1.05f, i * 1.05f, k * 1.05f)));
                            }
                        }
                    }
                };
                KadGame.Instance.ScreenClearColor = Color.Crimson; //Firebrick is okay too
                
                game.Run();
            }
        }
    }
}