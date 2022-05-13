using Xunit;

namespace Demo.Tests
{
    public class AssertStringsTests
    {

        [Fact]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Wagner", "Lima");

            //Assert
            Assert.Equal("Wagner Lima", nomeCompleto);

        }

        [Fact]
        public void StringsTools_UnirNomes_DeveIgnorarCase() //Ignorando Letras Maiusculas ou Minusculas
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Wagner", "Lima");

            // Assert
            Assert.Equal("WAGNER LIMA", nomeCompleto, true);
        }



        [Fact]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Wagner", "Lima");

            // Assert
            Assert.Contains("agne", nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom() //Busca as letras do começo da string
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Wagner", "Lima");

            // Assert
            Assert.StartsWith("Wag", nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_DeveAcabarCom() //Busca as letras do fim da string
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Wagner", "Lima");

            // Assert
            Assert.EndsWith("ima", nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Wagner", "Lima");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
        }
    }
}
