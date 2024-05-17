using Microsoft.Xna.Framework;
using MonoKad;

namespace BideoGaem
{
    class Program
    {
        static void Main() {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--> Type \"0\" to play on the cube juggling scene.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("- \"Shoot\" the cube (left click) to launch it up and increase your score.\n- Your score gets reset if you let the cube touch the ground or fall into the pit.\n- Get the highest score! (score is displayed on the console window)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--> Type \"1\" to play on the sandbox scene.");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("- Left click to launch the cube you're looking at.\n- Right click to spawn more cubes.\n- Have fun breaking the physics engine!");
            bool loadSandbox = Console.Read() == '1';
            
            using (KadGame game = new KadGame()) {
                game.Initialized += () =>
                {
                    CanGameManager canManager = game.AddGameObject(new GameObject()).AddBehaviour<CanGameManager>();
                    canManager.SandboxMode = loadSandbox;
                    
                    game.AddGameObject(new PlayerObject());
                    game.AddGameObject(new ColosseumObject());
                    game.AddGameObject(new DaedalusObject(new Vector3(5.0f, 1.0f, 0.0f)));
                    canManager.InitCans();
                    
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("--> Use Alt+Tab if the game window is out of focus.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                };
                KadGame.Instance.ScreenClearColor = Color.Crimson;
                
                game.Run();
            }
        }
    }
}