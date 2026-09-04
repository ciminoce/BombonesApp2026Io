namespace BombonesApp2026.Entidades
{
    abstract class Producto
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
        public Producto(string nombre, decimal precio, int stock)
        {
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
        }
        public abstract string MostrarDatos();
        public decimal PrecioConDescuento(decimal porcentaje)
        {
            return Precio * (1 - porcentaje / 100);
        }

    }
}
