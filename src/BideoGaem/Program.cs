﻿using Microsoft.Xna.Framework;
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
                    game.AddGameObject(new DaedalusObject(new Vector3(5.0f, 1.0f, 0.0f)));
                    game.AddGameObject(new PhysicBoxObject(new Vector3(0.0f, 5.0f, 0.0f)));
                };
                KadGame.Instance.ScreenClearColor = Color.Crimson; //Firebrick is okay too
                
                game.Run();
            }
        }
    }
}