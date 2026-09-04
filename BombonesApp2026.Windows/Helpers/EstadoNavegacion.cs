namespace BombonesApp2026.Windows.Helpers
{
    public class EstadoNavegacion
    {
        public int PaginaActual { get; set; } = 1;
        public int RegistrosPorPagina { get; set; }
        public int TotalRegistros { get; private set; }
        public int TotalPaginas { get; private set; }
        public EstadoNavegacion(int registrosPorPagina=10)
        {
            RegistrosPorPagina=registrosPorPagina;
        }
        #region Métodos de desplazamiento

        public bool PuedeIrAnterior()
        {
            return PaginaActual > 1;
        }
        public bool PuedeIrSiguiente()
        {
            return PaginaActual < TotalPaginas;
        }
        public void PrimeraPagina()
        {
            PaginaActual = 1;
        }
        public void PaginaAnterior()
        {
            if (PuedeIrAnterior())
            {
                PaginaActual--;
            }
        }
        public void PaginaSiguiente()
        {
            if (PuedeIrSiguiente())
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
        public void Actualizar(int registros)
        {
            TotalRegistros= registros;
            TotalPaginas = (int)Math.Ceiling((double)TotalRegistros / RegistrosPorPagina);
            PaginaActual = int.Min(PaginaActual, TotalPaginas);
        }
    }
}
