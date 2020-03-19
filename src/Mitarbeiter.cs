namespace Personal
{
    using System.Collections.Generic;

    public class Mitarbeiter
    {
        public Mitarbeiter(string name)
        {
            this.Name = name;
            this.Bestelllimit = SetzeAllgemeinesLimit();
            this.Vorgesetzter = SetzeVorgesetzten();
        }

        public string Name { get; }

        public int Bestelllimit { get; protected set; }

        public Vorgesetzter Vorgesetzter { get; private set; }

        public static int SetzeAllgemeinesLimit(int Bestelllimit = 20)
        {
            return Bestelllimit;
        }

        public Vorgesetzter SetzeVorgesetzten(Vorgesetzter meinVorgesetzter = null)
        {
            this.Vorgesetzter = meinVorgesetzter;

            return Vorgesetzter;
        }

        public bool DarfBestellen(int bestellhöhe)
        {
            var mitarbeiterÜberschreitetBestelllimitNicht = bestellhöhe <= this.Bestelllimit;

            return mitarbeiterÜberschreitetBestelllimitNicht;
        }

        public string gibInfo()
        {
            var stringPersonalart = string.Format("Ich bin {0}, ", this.GetType());
            var stringName = string.Format("Name {0}. ", this.Name);
            var stringVorgesetzter = string.Format("Mein Vorgesetzter ist {0}. ", this.Vorgesetzter);
            var stringBestelllimit = string.Format("Mein Bestelllimit ist {0} Euro.", this.Bestelllimit);

            var hatKeinenVorgesetzten = this.Vorgesetzter.Equals(null);
            var istSelbstVorgesetzter = this.GetType().Equals(Vorgesetzter);
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
            var hierarchie = new List<Vorgesetzter>();

            while (einVorgesetzterexistiert)
            {
                var seinVorgesetzter = mitarbeiterImDurchlauf.Vorgesetzter;
                hierarchie.Add(seinVorgesetzter);
                mitarbeiterImDurchlauf = seinVorgesetzter;
                einVorgesetzterexistiert = !mitarbeiterImDurchlauf.Vorgesetzter.Equals(null);
            }

            var listeMitVorgesetzten = string.Join("\nVorgesetzter ", hierarchie);

            return listeMitVorgesetzten;
        }
    }
}
