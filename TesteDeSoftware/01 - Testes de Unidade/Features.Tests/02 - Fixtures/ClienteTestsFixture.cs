using System;
using Features.Clientes;
using Xunit;

namespace Features.Tests;

//Collection Fixture é usado para sempre usar uma mesma instancia de um obj nos testes.

[CollectionDefinition(nameof(ClienteCollection))]
public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
{
}

/// <summary>
/// Os objetos criados na fixture são válidos durante toda execução dos 
/// testes que estão nessa coleção
/// </summary>
public class ClienteTestsFixture : IDisposable
{
    public Cliente GerarClienteValido()
    {
        var cliente = new Cliente(
            Guid.NewGuid(),
            "Wagner",
            "Lima",
            DateTime.Now.AddYears(-30),
            "wagner@wagner.com",
            true,
            DateTime.Now);

        return cliente;
    }

    public Cliente GerarClienteInValido()
    {
        var cliente = new Cliente(
            Guid.NewGuid(),
            "",
            "",
            DateTime.Now,
            "wag2wagner.com",
            true,
            DateTime.Now);

        return cliente;
    }

    public void Dispose()
    {
    }
}
