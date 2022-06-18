using System;
using Features.Clientes;
using Xunit;

namespace Features.Tests;

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
            "Eduardo",
            "Pires",
            DateTime.Now.AddYears(-30),
            "edu@edu.com",
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
            "edu2edu.com",
            true,
            DateTime.Now);

        return cliente;
    }

    public void Dispose()
    {
    }
}
