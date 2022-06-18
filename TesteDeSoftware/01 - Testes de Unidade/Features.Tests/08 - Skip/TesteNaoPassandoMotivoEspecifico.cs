using Xunit;

namespace Features.Tests
{
    /// <summary>
    /// No exemplo abaixo através do atributo Skip do Fact é possivel escapar (pular)
    /// testes que por algum motivos o desenvolvedor queira ignorar. Por exemplo, esta
    /// sendo implementada uma migração no projeto e alguns testes irão de fato quebrar
    /// mas como as correções ainda serão feitas, pode-se usar deste artifício
    /// </summary>
    public class TesteNaoPassandoMotivoEspecifico
    {
        [Fact(DisplayName = "Novo Cliente 2.0", Skip = "Nova versão 2.0 quebrando")]
        [Trait("Categoria", "Escapando dos Testes")]
        public void Teste_NaoEstaPassando_VersaoNovaNaoCompativel()
        {
            Assert.True(false);
        }
    }
}