using System.Collections.Generic;

namespace beadmania.Logic.Model
{
    public class BeadPalette
    {
        private readonly HashSet<Bead> beads = new HashSet<Bead>();

        public IEnumerable<Bead> Beads => beads;

        public void Add(Bead bead)
        {
            beads.Add(bead);
        }
    }
}