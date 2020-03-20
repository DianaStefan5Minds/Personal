namespace Mitarbeiter
{
    public class Vorgesetzter : Mitarbeiter
    {
        public Vorgesetzter(string name) : base(name)
        {
        }

        public void setzeBestelllimit(int eigenesBestelllimit)
        {
            Bestelllimit = eigenesBestelllimit;
        }
    }
}
