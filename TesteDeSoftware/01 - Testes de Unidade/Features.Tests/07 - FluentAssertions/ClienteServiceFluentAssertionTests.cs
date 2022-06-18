using System.Threading;
using Features.Clientes;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceFluentAssertionTests
    {
        readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;

        private readonly ClienteService _clienteService;

        public ClienteServiceFluentAssertionTests(ClienteTestsAutoMockerFixture clienteTestsFixture)
        {
            _clienteTestsAutoMockerFixture = clienteTestsFixture;
            _clienteService = _clienteTestsAutoMockerFixture.ObterClienteService();
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            //Assert.True(cliente.EhValido());

            // Assert using FluentAssertions
            cliente.EhValido().Should().BeTrue();

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente),Times.Once);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m=>m.Publish(It.IsAny<INotification>(),CancellationToken.None),Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();

            // Act
            _clienteService.Adicionar(cliente);

            // Assert
            //Assert.False(cliente.EhValido());

            // Assert using FluentAssertions
            cliente.EhValido().Should().BeFalse("Have inconsistencies");
            cliente.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1);

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Fluent Assertion Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(_clienteTestsAutoMockerFixture.ObterClientesVariados());

            // Act
            var clientes = _clienteService.ObterTodosAtivos();

            // Assert 
            //Assert.True(clientes.Any());
            //Assert.False(clientes.Count(c => !c.Ativo) > 0);

            // Assert using FluentAssertions
            clientes.Should().HaveCountGreaterOrEqualTo(1).And.OnlyHaveUniqueItems();
            clientes.Should().NotContain(c => !c.Ativo);

            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);

            // A validação abaixo é apenas um exemplo de uso do ExecutionTimeOf, mas, o recomendado
            // é utilizar esse tipo de validação em testes de integração e não em testes de unidade
            _clienteService.ExecutionTimeOf(c=>c.ObterTodosAtivos())
                .Should()
                .BeLessOrEqualTo(50.Milliseconds(),
                    "runs thousands of times per second");
        }
    }
}