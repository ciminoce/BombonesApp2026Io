namespace BombonesApp2026.Entidades
{
    public abstract class Producto
    {
        private string _nombre = null!;
        private string? _descripcion;
        private decimal _precio;
        private int _stock;

        public int ProductoId { get; set; }

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El nombre del producto no puede estar vacío ni ser nulo.", nameof(value));
                }

                if (value.Trim().Length > 100)
                {
                    throw new ArgumentException("El nombre del producto no puede superar los 100 caracteres.", nameof(value));
                }

                _nombre = value.Trim();
            }
        }

        public string? Descripcion
        {
            get => _descripcion;
            set
            {
                if (value != null && value.Trim().Length > 250)
                {
                    throw new ArgumentException("La descripción no puede superar los 250 caracteres.", nameof(value));
                }

                _descripcion = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
            }
        }

        public decimal Precio
        {
            get => _precio;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "El precio no puede ser negativo.");
                }

                _precio = value;
            }
        }

        public int Stock
        {
            get => _stock;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "El stock no puede ser negativo.");
                }

                _stock = value;
            }
        }

        public bool Activo { get; set; } = true;

        protected Producto(string nombre, decimal precio, int stock)
        {
            // Las asignaciones invocan los 'setters' correspondientes y sus validaciones
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
        }

        public abstract string MostrarDatos();

        public decimal PrecioConDescuento(decimal porcentaje)
        {
            if (porcentaje < 0 || porcentaje > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(porcentaje), "El porcentaje de descuento debe estar entre 0 y 100.");
            }

            return Precio * (1 - porcentaje / 100m);
        }
    }
}
