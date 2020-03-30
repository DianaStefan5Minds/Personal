namespace Mitarbeiter
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    using Microsoft.Win32.SafeHandles;

    public class Mitarbeiter
    {
        public Mitarbeiter(string name)
        {
            this.Name = name;
            this.Vorgesetzter = null;
        }

        protected string Name { get; }

        protected static int Bestelllimit { get; private set; }

        protected Vorgesetzter Vorgesetzter { get; private set; }

        public static void SetzeAllgemeinesLimit(int bestelllimit = 20)
        {
            Mitarbeiter.Bestelllimit = bestelllimit;
        }

        public void SetzeVorgesetzten(Vorgesetzter meinVorgesetzter = null)
        {
            this.Vorgesetzter = meinVorgesetzter;
        }

        public virtual bool DarfBestellen(int bestellhöhe)
        {
            var darfBestellen = bestellhöhe <= Bestelllimit;

            return darfBestellen;
        }

        public virtual string gibInfo()
        {
            var stringPersonalart =  "Ich bin Mitarbeiter, ";
            var stringName = string.Format("Name {0}. ", this.Name);
            var stringVorgesetzter = "";
            var stringBestelllimit = string.Format("Mein Bestelllimit ist {0} EUR.", Mitarbeiter.Bestelllimit.ToString());

            var hatEinenVorgesetzten = ! this.Vorgesetzter.Equals(null);
            var istFreierMitarbeiter = ! hatEinenVorgesetzten;

            if (hatEinenVorgesetzten)
            {
                stringVorgesetzter = string.Format("Mein Vorgesetzter ist {0}. ", this.Vorgesetzter.Name);
            }

            if (istFreierMitarbeiter)
            {
                stringPersonalart = "Ich bin freier Mitarbeiter.";
            }

            return(stringPersonalart + stringName + stringVorgesetzter + stringBestelllimit);
        }

        public virtual string gibHierarchie()
        {
            var einVorgesetzterexistiert = !this.Vorgesetzter.Equals(null);
            var mitarbeiterImDurchlauf = this;
            var hierarchie = new List<string>();

            while (einVorgesetzterexistiert)
            {
                var seinVorgesetzter = mitarbeiterImDurchlauf.Vorgesetzter;
                hierarchie.Add(seinVorgesetzter.Name);
                mitarbeiterImDurchlauf = seinVorgesetzter;
                einVorgesetzterexistiert = ! mitarbeiterImDurchlauf.Vorgesetzter.Equals(null);
            }

            var listeMitVorgesetzten = string.Join("\nVorgesetzter ", hierarchie);

            return listeMitVorgesetzten;
        }
    }
}
