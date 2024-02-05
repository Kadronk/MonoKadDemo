namespace MonoKad.Components
{
    public abstract class Component
    {
        public GameObject GameObject { get => _gameObject; init => _gameObject = value; }

        private GameObject _gameObject;
        
        //TODO: GameObject is set after the constructor is done, so i either need an Awake()/Initialize() or a way to pass the gameObject as constructor argument
    }
}