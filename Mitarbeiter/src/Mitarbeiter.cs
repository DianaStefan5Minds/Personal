namespace Mitarbeiter
{
    using System.Collections.Generic;

    using Microsoft.Win32.SafeHandles;

    public class Mitarbeiter
    {
        public Mitarbeiter(string name)
        {
            this.Name = name;
            Mitarbeiter.Bestelllimit = SetzeAllgemeinesLimit();
            this.Vorgesetzter = SetzeVorgesetzten();
            this.Typ = "Mitarbeiter";
        }

        public string Name { get; }

        public string Typ { get; protected set; }

        public static int Bestelllimit { get; set; }

        public Vorgesetzter Vorgesetzter { get; private set; }

        public static int SetzeAllgemeinesLimit(int bestelllimit = 20)
        {
            return bestelllimit;
        }

        public Vorgesetzter SetzeVorgesetzten(Vorgesetzter meinVorgesetzter = null)
        {
            this.Vorgesetzter = meinVorgesetzter;

            return Vorgesetzter;
        }

        public bool DarfBestellen(int bestellhöhe)
        {
            var mitarbeiterÜberschreitetBestelllimitNicht = bestellhöhe <= Mitarbeiter.Bestelllimit;

            return mitarbeiterÜberschreitetBestelllimitNicht;
        }

        public string gibInfo()
        {
            var stringPersonalart = string.Format("Ich bin {0}, ", this.Typ);
            var stringName = string.Format("Name {0}. ", this.Name);
            var stringVorgesetzter = string.Format("Mein Vorgesetzter ist {0}. ", this.Vorgesetzter.Name);
            var stringBestelllimit = string.Format("Mein Bestelllimit ist {0} EUR.", Mitarbeiter.Bestelllimit);

            var hatKeinenVorgesetzten = this.Vorgesetzter.Equals(null);
            var istSelbstVorgesetzter = this.GetType().Equals("Vorgestzter");
            var istFreierMitarbeiter = hatKeinenVorgesetzten && !istSelbstVorgesetzter;

            if (hatKeinenVorgesetzten)
            {
                stringVorgesetzter = "";
            }

            if (istFreierMitarbeiter)
            {
                stringPersonalart = "Ich bin freier Mitarbeiter.";
            }

            return(stringPersonalart + stringName + stringVorgesetzter + stringBestelllimit);
        }

        public string gibHierarchie()
        {
            var einVorgesetzterexistiert = !this.Vorgesetzter.Equals(null);
            var mitarbeiterImDurchlauf = this;
            var hierarchie = new List<string>();

            while (einVorgesetzterexistiert)
            {
                var seinVorgesetzter = mitarbeiterImDurchlauf.Vorgesetzter;
                hierarchie.Add(seinVorgesetzter.Name);
                mitarbeiterImDurchlauf = seinVorgesetzter;
                einVorgesetzterexistiert = !mitarbeiterImDurchlauf.Vorgesetzter.Equals(null);
            }

            var listeMitVorgesetzten = string.Join("\nVorgesetzter ", hierarchie);

            return listeMitVorgesetzten;
        }
    }
}
