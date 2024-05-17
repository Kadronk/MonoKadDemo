using Microsoft.Xna.Framework;
using MonoKad;
using MonoKad.Components;

namespace BideoGaem
{
    public class CanGameManager : Behaviour
    {
        public static CanGameManager Instance;

        public float TpYLevel => _tpYLevel;

        public bool SandboxMode;
        private int _currentScore;
        private float _tpYLevel = -40.0f;
        
        public override void Awake()
        {
            Instance = this;
        }

        public void InitCans() {
            int hMin = SandboxMode ? -3 : 0;
            int hMax = SandboxMode ? 3 : 1;
            int height = SandboxMode ? 5 : 1;
            
            for (int i = hMin; i < hMax; i++) {
                for (int j = hMin; j < hMax; j++) {
                    for (int k = 0; k < height; k++) {
                        KadGame.Instance.AddGameObject(new PhysicBoxObject(new Vector3(i * 1.05f, k * 1.05f, j * 1.05f)));
                    }
                }
            }
        }

        public void OnCanShot() {
            if (SandboxMode) return;
            
            _currentScore++;
            Console.WriteLine($"Current score: {_currentScore}");
        }
        
        public void OnCanLanded() {
            if (SandboxMode) return;
            if (_currentScore == 0) return; // hides the fact that this method is fired every frame the can is on the ground :)

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"FINAL SCORE: {_currentScore}!");
            Console.ForegroundColor = ConsoleColor.Gray;
            _currentScore = 0;
        }
    }
}