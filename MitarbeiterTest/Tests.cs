namespace MitarbeiterTest
{
    using Xunit;
    using Personal;
    using System;

    using FluentAssertions;

    public class Tests
    {
        public static Mitarbeiter winkelmann = new Mitarbeiter("Walter Winkelmann");
        public static Vorgesetzter wichtig = new Vorgesetzter("Waltraud Wichtig");
        public static Vorgesetzter wichtiger = new Vorgesetzter("Hermann Wichtiger");

        public static void SetzeVorgesetzte()
        {
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
            SetzeVorgesetzte();

            //Act
            var winkelmannDarfBestellen = winkelmann.DarfBestellen(wert);

            //Assert
            winkelmannDarfBestellen.Should().Equals(allowed);
        }

        [Theory]
        [InlineData(15, true)]
        [InlineData(25, false)]
        public void WichtigerDarfBestellen(int wert, bool allowed)
        {
            //Arrange
            SetzeVorgesetzte();

            //Act
            var WichtigerDarfBestellen = wichtiger.DarfBestellen(wert);

            //Assert
            WichtigerDarfBestellen.Should().Equals(allowed);
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
            SetzeVorgesetzte();
            erweiterePersonalUndÄndereBestelllimit();

            //Act
            var winkelmannDarfBestellen = winkelmann.DarfBestellen(30);

            //Assert
            winkelmannDarfBestellen.Should().Equals(true);
        }

        [Fact]
        public void WichtigerDarfFür25EuroBestellen_sollTrueZurückgeben()
        {
            //Arrange
            SetzeVorgesetzte();
            erweiterePersonalUndÄndereBestelllimit();

            //Act
            var wichtigerDarfBestellen = wichtiger.DarfBestellen(30);

            //Assert
            wichtigerDarfBestellen.Should().Equals(true);
        }
    }
}
