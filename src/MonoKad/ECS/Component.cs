namespace MonoKad.Components
{
    public abstract class Component
    {
        public GameObject GameObject { get => _gameObject; init => _gameObject = value; }

        private GameObject _gameObject;
        
        public virtual void Awake() { }
    }
}