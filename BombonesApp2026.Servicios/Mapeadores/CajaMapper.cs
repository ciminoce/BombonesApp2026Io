using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.DTOs.Caja;

namespace CajaesApp2026.Servicios.Mapeadores
{
    public static class CajaMapper
    {
        public static CajaListDto ToListDto(this Caja caja)
        {
            return new CajaListDto
            {
                ProductoId = caja.ProductoId,
                NombreCaja = caja.Nombre,
                Precio = caja.Precio,
                Stock = caja.Stock,
                EsSurtida = caja.EsSurtida,
                CantidadBombones = caja.CantidadBombones,
                Activo = caja.Activo
            };
        }

        public static Caja ToEntidad(this CajaCreateDto dto)
        {
            return new Caja
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock,
                EsSurtida = dto.EsSurtida,
                CantidadBombones = dto.CantidadBombones
            };
        }
        public static CajaUpdateDto ToUpdateDto(this Caja Caja)
        {
            return new CajaUpdateDto
            {
                ProductoId = Caja.ProductoId,
                Nombre = Caja.Nombre,
                Descripcion = Caja.Descripcion,
                Precio = Caja.Precio,
                Stock = Caja.Stock,
                EsSurtida = Caja.EsSurtida,
                CantidadBombones = Caja.CantidadBombones,
                Activo = Caja.Activo,
                RowVersion = Caja.RowVersion
            };
        }
        public static CajaDeleteDto ToDeleteDto(this Caja Caja)
        {
            return new CajaDeleteDto
            {
                ProductoId = Caja.ProductoId,
                RowVersion = Caja.RowVersion,
            };
        }

    }

}
