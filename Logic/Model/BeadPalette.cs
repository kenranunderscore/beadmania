using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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

        public XDocument ToXml()
        {
            return new XDocument(
                new XElement(nameof(BeadPalette),
                    beads.Select(b =>
                        new XElement(nameof(Bead),
                            new XElement(nameof(Bead.Description), b.Description),
                            new XElement(nameof(Bead.Color), b.Color.ToArgb())))));
        }
    }
}