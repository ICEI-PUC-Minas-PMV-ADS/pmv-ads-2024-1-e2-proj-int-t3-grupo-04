namespace NextMidiaWeb.Models.Input
{
    public class PesquisaFiltrosInput
    {
        public string? TextoLivre { get; set; }

        public eFiltrarPor? FiltrarPor { get; set; }
    }

    public enum eFiltrarPor
    {
        DataLancamento = 0,
        DataLancamentoDescrescente = 1,
        Visualizacoes = 2,
        Popularidade = 3
    }
}