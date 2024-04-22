using MonoKad.Components;

namespace BideoGaem
{
    public class CanGameManager : Behaviour
    {
        public static CanGameManager Instance;

        private int _currentScore;
        
        public override void Awake()
        {
            Instance = this;
        }

        public void OnCanHit()
        {
            _currentScore++;
            Console.WriteLine($"Current score! {_currentScore}!");
        }
    }
}