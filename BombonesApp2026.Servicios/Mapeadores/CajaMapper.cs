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
        public static CajaUpdateDto ToUpdateDto(this Caja caja)
        {
            return new CajaUpdateDto
            {
                ProductoId = caja.ProductoId,
                Nombre = caja.Nombre,
                Descripcion = caja.Descripcion,
                Precio = caja.Precio,
                Stock = caja.Stock,
                EsSurtida = caja.EsSurtida,
                CantidadBombones = caja.CantidadBombones,
                Activo = caja.Activo,
                RowVersion = caja.RowVersion
            };
        }
        public static CajaDeleteDto ToDeleteDto(this Caja caja)
        {
            return new CajaDeleteDto
            {
                ProductoId = caja.ProductoId,
                RowVersion = caja.RowVersion,
            };
        }

    }

}
