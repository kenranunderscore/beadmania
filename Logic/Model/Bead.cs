namespace beadmania.Logic.Model
{
    using System;
    using System.Drawing;

    public sealed class Bead : PropertyChangedNotifier, IEquatable<Bead>
    {
        private Color color;
        private string identifier;

        public string Identifier
        {
            get { return identifier; }
            set { SetProperty(ref identifier, value); }
        }

        public Color Color
        {
            get { return color; }
            set { SetProperty(ref color, value); }
        }

        public bool Equals(Bead other)
        {
            if (other == null)
                return false;

            return Identifier == other.Identifier && Color.ToArgb() == other.Color.ToArgb();
        }

        public override bool Equals(object obj)
        {
            var bead = obj as Bead;
            return Equals(bead);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 486187739 + (Identifier ?? string.Empty).GetHashCode();
                hash = hash * 486187739 + Color.GetHashCode();
                return hash;
            }
        }

        public Bead Clone() => new Bead { Color = Color, Identifier = Identifier };
    }
}