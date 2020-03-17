namespace Personal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.AccessControl;
    using System.Security.Cryptography;

    internal class Program
    {
        public static void Main(string[] args)
        {
        }

        public class Mitarbeiter
        {
            public string Name { get; set; }
            public int Bestelllimit { get; set; }
            public Vorgesetzter Vorgesetzter { get; set; }

            public Mitarbeiter(string name)
            {
                this.Name = name;
                this.Bestelllimit = SetzeAllgemeinesLimit();
                this.Vorgesetzter = SetzeVorgesetzten();
            }

            public void SetzeAllgemeinesLimit(int limit = 20)
            {
                this.Bestelllimit = limit;
            }

            public void SetzeVorgesetzten(Vorgesetzter meinVorgesetzter = null)
            {
                this.Vorgesetzter = meinVorgesetzter;
            }

            public bool DarfBestellen(int bestellhöhe)
            {
                if (bestellhöhe <= this.Bestelllimit)
                {
                    return true;
                }

                return false;
            }

            public string gibInfo(Mitarbeiter einMitarbeiter)
            {
                var personalart = string.Format("Ich bin {0}, ", einMitarbeiter.GetType());
                var name = string.Format("Name {0}. ", einMitarbeiter.Name);
                var vorgesetzter = string.Format("Mein Vorgesetzter ist {0}. ", einMitarbeiter.Vorgesetzter);
                var bestelllimit = string.Format("Mein Bestelllimit ist {0} Euro.", einMitarbeiter.Bestelllimit);

                var hatKeinenVorgesetzten = einMitarbeiter.Vorgesetzter.Equals(null);
                var istSelbstVorgesetzter = einMitarbeiter.GetType().Equals(Vorgesetzter);
                var istFreierMitarbeiter = hatKeinenVorgesetzten && !istSelbstVorgesetzter;

                if (hatKeinenVorgesetzten)
                {
                    vorgesetzter = "";
                }

                if (istFreierMitarbeiter)
                {
                    personalart = "Ich bin freier Mitarbeiter.";

                }

                return(personalart + name + vorgesetzter + bestelllimit);
            }

            public string gibHierarchie(Mitarbeiter einMitarbeiter)
            {
                var einVorgesetzterexistiert = einMitarbeiter.Vorgesetzter.Equals(!null);
                var mitarbeiterImDurchlauf = einMitarbeiter;
                var hierarchie = [];
                while (einVorgesetzterexistiert)
                {
                    var seinVorgesetzter =mitarbeiterImDurchlauf.Vorgesetzter;
                    hierarchie.Add(seinVorgesetzter);
                    mitarbeiterImDurchlauf = seinVorgesetzter;
                    einVorgesetzterexistiert = mitarbeiterImDurchlauf.Vorgesetzter.Equals(!null);
                }

                return string.Join("\nVorgesetzter ", hierarchie);
            }
        }

        public class Vorgesetzter : Mitarbeiter
        {
            public Vorgesetzter(string name) : base(name)
            {
            }

            public void setzeBestelllimit(int eigenesBestelllimit)
            {
                this.Bestelllimit = eigenesBestelllimit;
            }
        }
    }


}
