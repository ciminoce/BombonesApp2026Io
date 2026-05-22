using BombonesApp2026.Entidades;
using BombonesApp2026.Servicios.DTOs.FormaDePago;

namespace BombonesApp2026.Servicios.Mapeadores
{
    public static class FormaDePagoMapper
    {
        public static FormaDePagoListDto ToListDto(FormaDePago formaDePago)
        {
            return new FormaDePagoListDto
            {
                FormaDePagoId = formaDePago.FormaDePagoId,
                Nombre = formaDePago.Nombre,
                Activo = formaDePago.Activo,
            };
        }
        public static FormaDePago ToEntidad(FormaDePagoCreateDto formaDePagoDto)
        {
            return new FormaDePago
            {
                Nombre = formaDePagoDto.Nombre,
            };
        }
        public static FormaDePago ToEntidad(FormaDePagoUpdateDto formaDePagoDto)
        {
            return new FormaDePago
            {
                FormaDePagoId = formaDePagoDto.FormaDePagoId,
                Nombre = formaDePagoDto.Nombre,
                Activo = formaDePagoDto.Activo,
            };
        }

        public static FormaDePagoUpdateDto ToUpdateDto(FormaDePago formaDePago)
        {
            return new FormaDePagoUpdateDto
            {
                FormaDePagoId = formaDePago.FormaDePagoId,
                Nombre = formaDePago.Nombre,
                Activo = formaDePago.Activo,
            };
        }
    }
}
