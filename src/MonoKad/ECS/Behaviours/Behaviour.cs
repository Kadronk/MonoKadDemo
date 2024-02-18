namespace MonoKad.Components
{
    public abstract class Behaviour : Component
    {
        public bool Enabled { get => _enabled; set => _enabled = value; }
        
        private bool _enabled = true;
        
        public virtual void Update() { }
    }
}
