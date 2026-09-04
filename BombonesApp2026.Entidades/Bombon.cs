namespace BombonesApp2026.Entidades
{
    class Bombon : Producto
    {

        public int PesoEnGramos { get; set; }
        public bool TieneAzucar { get; set; }
        public Bombon(string nombre, decimal precio, int stock,
            int pesoEnGramos, bool tieneAzucar) : base(nombre, precio, stock)
        {
            PesoEnGramos = pesoEnGramos;
            TieneAzucar = tieneAzucar;
        }
        public override string MostrarDatos()
        {
            return $"Bombón: Nombre: {Nombre} - Precio:{Precio:C2} - Peso:{PesoEnGramos} grs";
        }

    }
}
