namespace Mitarbeiter
{
    public class Vorgesetzter : Mitarbeiter
    {
        public Vorgesetzter(string name) : base(name)
        {
            this.eigenesBestelllimit = Bestelllimit;
        }

        private int eigenesBestelllimit;

        public void setzeBestelllimit(int eigenesBestelllimit)
        {
           this.eigenesBestelllimit = eigenesBestelllimit;
        }

        public override bool DarfBestellen(int bestellhöhe)
        {
            var darfBestellen = bestellhöhe <= this.eigenesBestelllimit;

            return darfBestellen;
        }

        public override string gibInfo()
        {
            var stringPersonalart =  "Ich bin Vorgesetzter, ";
            var stringName = string.Format("Name {0}. ", this.Name);
            var stringVorgesetzter = " ";
            var stringBestelllimit = string.Format("Mein Bestelllimit ist {0} EUR.", this.eigenesBestelllimit.ToString());

            var hatEinenVorgesetzten = ! this.Vorgesetzter.Equals(null);

            if (hatEinenVorgesetzten)
            {
                stringVorgesetzter = string.Format("Mein Vorgesetzter ist {0}. ", this.Vorgesetzter.Name);
            }

            return(stringPersonalart + stringName + stringVorgesetzter + stringBestelllimit);
        }
    }
}
