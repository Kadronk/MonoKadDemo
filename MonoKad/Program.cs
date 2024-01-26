namespace MonoKad
{
    class Program
    {
        static void Main() {
            using (KadGame game = new KadGame()) { game.Run(); }
        }
    }
}

