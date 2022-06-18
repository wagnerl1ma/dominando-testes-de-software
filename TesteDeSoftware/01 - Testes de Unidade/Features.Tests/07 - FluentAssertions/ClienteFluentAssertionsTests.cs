using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteFluentAssertionsTests
    {
        private readonly ClienteTestsAutoMockerFixture _clienteTestsFixture;
        readonly ITestOutputHelper _outputHelper;

        public ClienteFluentAssertionsTests(ClienteTestsAutoMockerFixture clienteTestsFixture, 
                                            ITestOutputHelper outputHelper)
        {
            _clienteTestsFixture = clienteTestsFixture;
            _outputHelper = outputHelper;
        }
        

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.True(result);
            //Assert.Equal(0, cliente.ValidationResult.Errors.Count);

            // Assert using FluentAssertions
            result.Should().BeTrue();
            cliente.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Categoria", "Cliente Fluent Assertion Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            //Assert.False(result);
            //Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);

            // Assert using FluentAssertions
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1, "must have validation errorrs");

            _outputHelper.WriteLine($"Were found {cliente.ValidationResult.Errors.Count} errors in this validation");
        }
    }
}