using Xunit;

namespace Features.Tests;

/// <summary>
/// O objeto é preparado antes de iniciar a classe de teste e só é destruido após o último
/// teste ser executado.
/// </summary>
[Collection(nameof(ClienteCollection))]
public class ClienteTesteValido
{
    private readonly ClienteTestsFixture _clienteTestsFixture;

    public ClienteTesteValido(ClienteTestsFixture clienteTestsFixture)
    {
        _clienteTestsFixture = clienteTestsFixture;
    }


    [Fact(DisplayName = "Novo Cliente Válido")]
    [Trait("Categoria", "Cliente Fixture Testes")]
    public void Cliente_NovoCliente_DeveEstarValido()
    {
        // Arrange
        var cliente = _clienteTestsFixture.GerarClienteValido();

        // Act
        var result = cliente.EhValido();

        // Assert 
        Assert.True(result);
        Assert.Equal(0, cliente.ValidationResult.Errors.Count);
    }
}
