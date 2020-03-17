using System;

using Xunit;

namespace MitarbeiterTest
{
    using System.Net.WebSockets;

    public class Tests
    {
        private Mitarbeiter winkelmann = new Mitarbeiter("Walter Winkelmann");
        private Vorgesetzter wichtig = new Vorgesetzter(name = "Waltraud Wichtig");
        private Vorgesetzter wichtiger = new Vorgesetzter(name = "Hermann Wichtiger");
        winkelmann.SetzeVorgesetzten(wichtig);
        wichtig.SetzeVorgesetzten(wichtiger);
        }

        [Theory]
        [InlineData(15, true)]
        [InlineData(20, true)]
        [InlineData(21, false)]
        public void WinkelmannDarfBestellen(int wert, bool allowed)
        {
            //Arrange
            ErzeugePersonal();

            //Act
            var WinkelmannDarfBestellen = winkelmann.DarfBestellen(wert);

            //Assert
            WinkelmannDarfBestellen.Should().Equal(allowed);
        }

        [Theory]
        [InlineData(15, true)]
        [InlineData(25, false)]
        public void WichtigerDarfBestellen(int wert, bool allowed)
        {
            //Arrange
            ErzeugePersonal();

            //Act
            var WichtigerDarfBestellen = wichtiger.DarfBestellen(wert);

            //Assert
            WichtigerDarfBestellen.Should().Equal(allowed);
        }

        public void erweiterePersonalUndÄndereBestelllimit()
        {
            Mitarbeiter.SetzeAllgemeinesLimit(30);
            var winzig = new Mitarbeiter("Willi Winzig");
        }

        [Fact]
        public void WinkelmannDarfFür21EuroBestellen_sollTrueZurückgeben()
        {
            //Arrange
            ErzeugePersonal();
            erweiterePersonalUndÄndereBestelllimit();

            //Act
            var winkelmannDarfBestellen = winkelmann.DarfBestellen(30);

            //Assert
            winkelmannDarfBestellen.Should().Equal(true);
        }

        [Fact]
        public void WichtigerDarfFür25EuroBestellen_sollTrueZurückgeben()
        {
            //Arrange
            ErzeugePersonal();
            erweiterePersonalUndÄndereBestelllimit();

            //Act
            var wichtigerDarfBestellen = wichtiger.DarfBestellen(30);

            //Assert
            wichtigerDarfBestellen.Should().Equal(true);
        }


    }
}
