namespace BombonesApp2026.Entidades
{
    class Caja : Producto
    {
        public Caja(string nombre, decimal precio, int stock, int cantidadBombones,
            bool esSurtida) : base(nombre, precio, stock)
        {
            CantidadBombones = cantidadBombones;
            EsSurtida = esSurtida;
        }

        public bool EsSurtida { get; set; }
        public int CantidadBombones { get; set; }
        public override string MostrarDatos()
        {
            return $"Caja: Nombre: {Nombre} - Precio:{Precio:C2} - Cant. Bombones:{CantidadBombones} unidades";
        }

    }
}
