using Xunit;

namespace Demo.Tests
{
    public class CalculadoraTests
    {

        //Padrão de Nomenclatura para teste: NomeDaClasse_Método_OqueEsperarDoMetodo
        //Ex: Calculadora_Somar_RetornarValorSoma()
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(2, 2);

            // Assert
            Assert.Equal(4, resultado);
        }


        //Teste Teorico
        [Theory]
        [InlineData(1, 1, 2)] // no "InlineData" é necessário passar os mesmo parametros do método e serão incluídos valor no parametro p/ testar o método
        [InlineData(2, 2, 4)]
        [InlineData(4, 2, 6)]
        [InlineData(7, 3, 10)]
        [InlineData(6, 6, 12)]
        [InlineData(9, 9, 18)]
        public void Calculadora_Somar_RetornarValoresSomaCorretos(double v1, double v2, double total)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(v1, v2);

            // Assert
            Assert.Equal(total, resultado);
        }
    }
}
