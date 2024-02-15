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
                    game.AddGameObject(new FunnyTriangleObject(Vector3.Zero, "RainbowCube.fbx"));
                    game.AddGameObject(new FunnyTriangleObject(Vector3.Right * 8.0f, "RainbowCubeTriangulated.fbx"));
                };
                
                game.Run();
            }
        }
    }
}