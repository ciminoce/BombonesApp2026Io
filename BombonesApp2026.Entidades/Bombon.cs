namespace BombonesApp2026.Entidades
{
    public class Bombon : Producto
    {
        private int _tipoBombonId;
        private int _pesoEnGramos;

        public int TipoBombonId
        {
            get => _tipoBombonId;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "El identificador del tipo de bombón debe ser un entero positivo.");
                }

                _tipoBombonId = value;
            }
        }

        // Propiedad de navegación
        public TipoBombon TipoBombon { get; set; } = null!;

        public int PesoEnGramos
        {
            get => _pesoEnGramos;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "El peso en gramos debe ser mayor a 0.");
                }

                _pesoEnGramos = value;
            }
        }

        public bool TieneAzucar { get; set; }

        public Bombon(
            string nombre,
            decimal precio,
            int stock,
            int pesoEnGramos,
            bool tieneAzucar) : base(nombre, precio, stock)
        {
            // Invoca el setter de la propiedad con sus respectivas validaciones
            PesoEnGramos = pesoEnGramos;
            TieneAzucar = tieneAzucar;
        }

        public override string MostrarDatos()
        {
            string azucarInfo = TieneAzucar ? "Con azúcar" : "Sin azúcar";
            return $"Bombón: {Nombre} - Precio: {Precio:C2} - Peso: {PesoEnGramos} grs - ({azucarInfo})";
        }
    }
}
