namespace beadmania.Logic.Model
{
    using System;
    using System.Drawing;

    public sealed class Bead : IEquatable<Bead>
    {
        public string Description { get; set; }

        public Color Color { get; set; }

        public bool Equals(Bead other)
        {
            if (other == null)
                return false;

            return Description == other.Description && Color.ToArgb() == other.Color.ToArgb();
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
                hash = hash * 486187739 + (Description ?? string.Empty).GetHashCode();
                hash = hash * 486187739 + Color.GetHashCode();
                return hash;
            }
        }

        public Bead Clone() => new Bead { Color = Color, Description = Description };
    }
}