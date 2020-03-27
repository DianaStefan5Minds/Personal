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
            this.Vorgesetzter = new Vorgesetzter(null);
        }

        public string Name { get; }

        public string Typ { get; protected set; }

        public static int Bestelllimit { get; set; }

        public Vorgesetzter Vorgesetzter { get; private set; }

        public static void SetzeAllgemeinesLimit(int bestelllimit = 20)
        {
            Mitarbeiter.Bestelllimit = bestelllimit;
        }

        public void SetzeVorgesetzten(Vorgesetzter meinVorgesetzter = null)
        {
            this.Vorgesetzter = meinVorgesetzter;
        }

        public bool DarfBestellen(int bestellhöhe)
        {
            var darfBestellen = bestellhöhe <= Mitarbeiter.Bestelllimit;

            return darfBestellen;
        }

        public string gibInfo()
        {
            var stringPersonalart =  "Ich bin Mitarbeiter, ";
            var stringName = string.Format("Name {0}. ", this.Name);
            var stringVorgesetzter = "";
            var stringBestelllimit = string.Format("Mein Bestelllimit ist {0} EUR.", Mitarbeiter.Bestelllimit);

            var hatEinenVorgesetzten = ! this.Vorgesetzter.Name.Equals(null);
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

        public string gibHierarchie()
        {
            var einVorgesetzterexistiert = !this.Vorgesetzter.Name.Equals(null);
            var mitarbeiterImDurchlauf = this;
            var hierarchie = new List<string>();

            while (einVorgesetzterexistiert)
            {
                var seinVorgesetzter = mitarbeiterImDurchlauf.Vorgesetzter;
                hierarchie.Add(seinVorgesetzter.Name);
                mitarbeiterImDurchlauf = seinVorgesetzter;
                einVorgesetzterexistiert = ! mitarbeiterImDurchlauf.Vorgesetzter.Name.Equals(null);
            }

            var listeMitVorgesetzten = string.Join("\nVorgesetzter ", hierarchie);

            return listeMitVorgesetzten;
        }
    }
}
