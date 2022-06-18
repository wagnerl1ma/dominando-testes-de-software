using Xunit;

namespace Features.Tests;

/// <summary>
/// O objeto é preparado antes de iniciar a classe de teste e só é destruido após o último
/// teste ser executado.
/// </summary>
[Collection(nameof(ClienteCollection))]
public class ClienteTesteInvalido
{
    private readonly ClienteTestsFixture _clienteTestsFixture;

    public ClienteTesteInvalido(ClienteTestsFixture clienteTestsFixture)
    {
        _clienteTestsFixture = clienteTestsFixture;
    }

    [Fact(DisplayName = "Novo Cliente Inválido")]
    [Trait("Categoria", "Cliente Fixture Testes")]
    public void Cliente_NovoCliente_DeveEstarInvalido()
    {
        // Arrange
        var cliente = _clienteTestsFixture.GerarClienteInValido();

        // Act
        var result = cliente.EhValido();

        // Assert 
        Assert.False(result);
        Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
    }
}
