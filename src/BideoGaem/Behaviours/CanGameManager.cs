using MonoKad.Components;

namespace BideoGaem
{
    public class CanGameManager : Behaviour
    {
        public static CanGameManager Instance;

        public float TpYLevel => _tpYLevel;

        private int _currentScore;
        private float _tpYLevel = -40.0f;
        
        public override void Awake()
        {
            Instance = this;
        }

        public void OnCanShot()
        {
            _currentScore++;
            Console.WriteLine($"Current score: {_currentScore}");
        }
        
        public void OnCanLanded()
        {
            if (_currentScore == 0) return; // hides the fact that this method is fired every frame the can is on the ground :)
            
            Console.WriteLine($"FINAL SCORE: {_currentScore}!");
            _currentScore = 0;
        }
    }
}