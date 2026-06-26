namespace BombonesApp2026.Servicios.Common
{
    public class ResultadoPaginacionDto<T> where T : class
    {
        public List<T> Items { get; set; } = new List<T>();
        public int CantidadRegistros { get; set; }
        public int CantidadPorPagina { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas =>(int) Math.Ceiling((double)CantidadRegistros / CantidadPorPagina);

        public bool TieneRegistrosAnteriores => PaginaActual > 1;
        public bool TieneRegistrosSiguientes => PaginaActual < TotalPaginas;
    }
}
