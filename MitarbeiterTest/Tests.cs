namespace MitarbeiterTest
{
    using System;

    using Xunit;
    using Personal;
    using FluentAssertions;

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
        }


        public void SetzeVorgesetzte()
        {
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
            SetzeVorgesetzte();

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
            SetzeVorgesetzte();

            //Act
            var WichtigerDarfBestellen = wichtiger.DarfBestellen(wert);

            //Assert
            WichtigerDarfBestellen.Should().Be(allowed);
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
            Arrange();
            SetzeVorgesetzte();
            erweiterePersonalUndÄndereBestelllimit();

            //Act
            var winkelmannDarfBestellen = winkelmann.DarfBestellen(30);

            //Assert
            winkelmannDarfBestellen.Should().Be(true);
        }

        [Fact]
        public void WichtigerDarfFür25EuroBestellen_sollTrueZurückgeben()
        {
            //Arrange
            Arrange();
            SetzeVorgesetzte();
            erweiterePersonalUndÄndereBestelllimit();

            //Act
            var wichtigerDarfBestellen = wichtiger.DarfBestellen(30);

            //Assert
            wichtigerDarfBestellen.Should().Be(true);
        }
    }
}
