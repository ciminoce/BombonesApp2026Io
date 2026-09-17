namespace BombonesApp2026.Entidades
{
    public class Caja : Producto
    {
        private int _cantidadBombones;

        public bool EsSurtida { get; set; }

        public int CantidadBombones
        {
            get => _cantidadBombones;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "La cantidad de bombones debe ser mayor a 0.");
                }

                _cantidadBombones = value;
            }
        }

        public Caja(
            string nombre,
            decimal precio,
            int stock,
            int cantidadBombones,
            bool esSurtida) : base(nombre, precio, stock)
        {
            // Se utiliza la propiedad para ejecutar el 'setter' y su validación
            CantidadBombones = cantidadBombones;
            EsSurtida = esSurtida;
        }

        public override string MostrarDatos()
        {
            string tipoCaja = EsSurtida ? "Surtida" : "Especial";
            return $"Caja ({tipoCaja}): {Nombre} - Precio: {Precio:C2} - Cant. Bombones: {CantidadBombones} unidades";
        }
    }
}
