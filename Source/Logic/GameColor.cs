namespace Jrpg.Game.Logic
{
    public struct GameColor
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameColor(byte r, byte g, byte b)
            : this()
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = byte.MaxValue;
        }

        public GameColor(byte r, byte g, byte b, byte a)
            : this(r, g, b)
        {
            this.A = a;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }
    }
}
