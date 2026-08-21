namespace BombonesApp2026.Windows.Helpers
{
    public class EstadoNavegacion
    {
        public int PaginaActual { get; set; } = 1;
        public int RegistrosPorPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public EstadoNavegacion(int registrosPorPagina=10)
        {
            RegistrosPorPagina=registrosPorPagina;
        }
        #region Métodos de desplazamiento

        public void PrimeraPagina()
        {
            PaginaActual = 1;
        }
        public void PaginaAnterior()
        {
            if (PaginaActual > 1)
            {
                PaginaActual--;
            }
        }
        public void PaginaSiguiente()
        {
            if (PaginaActual < TotalPaginas)
            {
                PaginaActual++;
            }
        }
        public void UltimaPagina()
        {
            PaginaActual = TotalPaginas;
        }
        #endregion
        #region Cálculos
        private (int desde, int hasta) CalcularRango()
        {
            int desde = 1 + (PaginaActual - 1) * RegistrosPorPagina;
            int hasta = int.Min(desde + RegistrosPorPagina - 1, TotalRegistros);
            return (desde, hasta);
        }
        public string TextoRegistros()
        {
            var (desde, hasta) = CalcularRango();
            return $"Del {desde} a {hasta} de {TotalRegistros}";
        }
        public string TextoPaginas()
        {
            return $"{PaginaActual} de {TotalPaginas}";
        }
        #endregion
    }
}
