namespace MitarbeiterTest
{
    using System.Reflection.Metadata;

    using FluentAssertions;
    using Mitarbeiter;

    using Xunit;

    public class Tests
    {
        public Mitarbeiter winkelmann;
        public Vorgesetzter wichtig;
        public Vorgesetzter wichtiger;

        public void Arrange()
        {
            winkelmann= new Mitarbeiter("Walter Winkelmann");
            wichtig = new Vorgesetzter("Waltraud Wichtig");
            wichtiger = new Vorgesetzter("Hermann Wichtiger");

            this.winkelmann.SetzeVorgesetzten(wichtig);
            this.wichtig.SetzeVorgesetzten(wichtiger);
        }

        [Theory]
        [InlineData(15, true)]
        [InlineData(20, true)]
        [InlineData(21, false)]
        public void WinkelmannDarfBestellen(int wert, bool allowed)
        {
            //Arrange
            Arrange();

            //Act
            var winkelmannDarfBestellen = winkelmann.DarfBestellen(wert);

            //Assert
            winkelmannDarfBestellen.Should().Be(allowed);
        }

        [Theory]
        [InlineData(15, true)]
        [InlineData(25, false)]
        public void WichtigerDarfBestellen(int wert, bool allowed)
        {
            //Arrange
            Arrange();

            //Act
            var WichtigerDarfBestellen = wichtiger.DarfBestellen(wert);

            //Assert
            WichtigerDarfBestellen.Should().Be(allowed);
        }

        public void Arrange2()
        {
            Arrange();
            var winzig = new Mitarbeiter("Willi Winzig");
            Mitarbeiter.Bestelllimit = Mitarbeiter.SetzeAllgemeinesLimit(30);

        }

        [Theory]
        [InlineData(21, true)]
        [InlineData(25, true)]
        public void WinkelmannDarfMehrBestellen_sollTrueZurückgeben(int limit, bool accepted)
        {
            //Arrange
            Arrange2();

            //Act
            var winkelmannDarfBestellen = winkelmann.DarfBestellen(limit);

            //Assert
            winkelmannDarfBestellen.Should().Be(accepted);
        }

        public void Arrange3()
        {
            Arrange2();
            wichtig.setzeBestelllimit(10);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(11, false)]
        public void WichtigHatBestellmengeVon10Euro(int limit, bool accepted)
        {
            //Arrange
            Arrange3();

            //Act
            var wichtigDarfBestellen = wichtig.DarfBestellen(limit);

            //Assert
            wichtigDarfBestellen.Should().Be(accepted);
        }

        public void Arrange4()
        {
            Arrange2();
            wichtig.setzeBestelllimit(5000);
        }

        [Theory]
        [InlineData(2000, true)]
        [InlineData(7000, false)]
        public void WichtigHatBestellmengeVon5000Euro(int limit, bool accepted)
        {
            //Arrange
            Arrange4();
            wichtig.setzeBestelllimit(5000);

            //Act
            var wichtigDarfBestellen = wichtig.DarfBestellen(limit);

            //Assert
            wichtigDarfBestellen.Should().Be(accepted);
        }

        [Fact]
        public void InfotextfürWichtigStimmt()
        {
            //Arrange
            Arrange4();
            var erwarteterInfotext = "Ich bin Vorgesetzter, Name Waltraud Wichtig. " +
                                     "Mein Vorgesetzter ist Hermann Wichtiger. Mein Bestelllimit ist 5000 EUR.";

            //Act
            var eigentlicherInfotest = wichtig.gibInfo();

            //Assert
            eigentlicherInfotest.Should().Be(erwarteterInfotext);
        }

        [Fact]
        public void HierarchieVonWichtigStimmt()
        {
            //Arrange
            Arrange4();
            var erwarteteVorgesetzte = "\nVorgesetzter Hermann Wichtiger \nVorgesetzter Waltraud Wichtig";

            //Act
            var eigentlicheVorgesetzte = wichtig.gibHierarchie();

            //Assert
            eigentlicheVorgesetzte.Should().Be(erwarteteVorgesetzte);
        }

        [Fact]
        public void InfotextVonWichtigerStimmt()
        {
            //Arrange
            Arrange4();
            var erwarteterInfotext = "Ich bin Vorgesetzter, Name Hermann Wichtiger. Mein Bestelllimit ist 30 EUR.";

            //Act
            var eigentlicherInfotest = wichtiger.gibInfo();

            //Assert
            eigentlicherInfotest.Should().Be(erwarteterInfotext);
        }

        [Fact]
        public void HierarchieVonWichtigerStimmt()
        {
            //Arrange
            Arrange4();
            var erwarteteVorgesetzte = "\nVorgesetzter Hermann Wichtiger";

            //Act
            var eigentlicheVorgesetzte = wichtiger.gibHierarchie();

            //Assert
            eigentlicheVorgesetzte.Should().Be(erwarteteVorgesetzte);
        }

        [Fact]
        public void InfotextVonWinkelmannStimmt()
        {
            //Arrange
            Arrange4();
            var erwarteterInfotext = "Ich bin Mitarbeiter, Name Walter Winkelmann. " +
                                     "Mein Vorgesetzter ist Waltraud Wichtig. Mein Bestelllimit ist 30 EUR.";

            //Act
            var eigentlicherInfotest = winkelmann.gibInfo();

            //Assert
            eigentlicherInfotest.Should().Be(erwarteterInfotext);
        }

        [Fact]
        public void HierarchieVonWinkelmannStimmt()
        {
            //Arrange
            Arrange4();
            var erwarteteVorgesetzte = "\nVorgesetzter Hermann Wichtiger " +
                                       "\nVorgesetzter Waltraud Wichtig " +
                                       "\nMitarbeiter Walter Winkelmann";

            //Act
            var eigentlicheVorgesetzte = winkelmann.gibHierarchie();

            //Assert
            eigentlicheVorgesetzte.Should().Be(erwarteteVorgesetzte);
        }

        public void Arrange5()
        {
            Arrange4();
            wichtig.SetzeVorgesetzten(null);
        }

        [Fact]
        public void AndereHierarchieVonWinkelmannStimmt()
        {
            //Arrange
            Arrange5();
            var erwarteteVorgesetzte = "\nVorgesetzter Waltraud Wichtig " +
                                       "\nMitarbeiter Walter Winkelmann";

            //Act
            var eigentlicheVorgesetzte = winkelmann.gibHierarchie();

            //Assert
            eigentlicheVorgesetzte.Should().Be(erwarteteVorgesetzte);
        }

        [Fact]
        public void InfotextVonWinzigStimmt()
        {
            //Arrange
            Arrange5();
            var erwarteterInfotext = "Ich bin freier Mitarbeiter, Name Willi Winzig. Mein Bestelllimit ist 30 EUR.";

            //Act
            var eigentlicherInfotest = winkelmann.gibInfo();

            //Assert
            eigentlicherInfotest.Should().Be(erwarteterInfotext);
        }

        [Fact]
        public void HierarchieVonWinzigStimmt()
        {
            //Arrange
            Arrange5();
            var erwarteterInfotext = "freier Mitarbeiter Willi Winzig";

            //Act
            var eigentlicherInfotest = winkelmann.gibHierarchie();

            //Assert
            eigentlicherInfotest.Should().Be(erwarteterInfotext);
        }


    }
}
