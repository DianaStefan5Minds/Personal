namespace Mitarbeiter
{
    public class Vorgesetzter : Mitarbeiter
    {
        public Vorgesetzter(string name) : base(name)
        {
            this.Typ = "Vorgesetzter";
        }

        public void setzeBestelllimit(int eigenesBestelllimit)
        {
            Bestelllimit = eigenesBestelllimit;
        }
    }
}
