namespace beadmania.Logic.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;
    using System.Xml.Linq;
    using beadmania.Logic.Extensions;

    public class BeadPalette : PropertyChangedNotifier
    {
        private readonly HashSet<Bead> beads = new HashSet<Bead>();

        public BeadPalette(string name)
        {
            Name = name;
        }

        //TODO: Validation
        public static BeadPalette FromXml(XDocument xml)
        {
            string name = xml.Root.Attribute(nameof(Name)).Value;
            BeadPalette palette = new BeadPalette(name);
            var beads = xml
                .Descendants(nameof(Bead))
                .Select(x => new Bead
                {
                    Identifier = x.Element(nameof(Bead.Identifier)).Value,
                    Color = ((Color)ColorConverter.ConvertFromString(x.Element(nameof(Bead.Color)).Value)).ToDrawingColor()
                });
            foreach (var bead in beads)
            {
                palette.beads.Add(bead);
            }
            return palette;
        }

        public string Name { get; set; }

        public IEnumerable<Bead> Beads => beads;

        public bool Add(Bead bead)
        {
            if (!beads.Any(b => b.Identifier == bead.Identifier))
            {
                beads.Add(bead);
                return true;
            }

            return false;
        }

        public void Remove(Bead bead)
        {
            beads.Remove(bead);
        }

        public XDocument ToXml()
        {
            return new XDocument(
                new XElement(nameof(BeadPalette),
                    new XAttribute(nameof(Name), Name),
                    beads.Select(b =>
                        new XElement(nameof(Bead),
                            new XElement(nameof(Bead.Identifier), b.Identifier),
                            new XElement(nameof(Bead.Color), b.Color.ToMediaColor().ToHexCode())))));
        }

        public BeadPalette Clone()
        {
            BeadPalette clone = new BeadPalette(Name);
            foreach (var bead in beads)
            {
                clone.beads.Add(bead.Clone());
            }
            return clone;
        }
    }
}