using BombonesApp2026.Servicios.Common;
using BombonesApp2026.Servicios.DTOs.TipoBombon;
using BombonesApp2026.Servicios.Intefaces;
using BombonesApp2026.Windows.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace BombonesApp2026.Windows
{
    //TODO: OJO ver cuando se cambia el estado en el primer registro!!!
    //TODO: OJO ajustes también en el agregar!!!
    public partial class frmTiposDeBombones : Form
    {
        private readonly IServiceProvider _serviceProvider;

        private BindingSource _bindingSource = new BindingSource();

        //para paginar
        private int _paginaActual = 1;
        private int _totalRegistros = 0;
        private int _totalPaginas = 0;
        private int _cantidadPorPagina = 10;

        //para ordenar
        private string campoOrdenar = "Nombre";
        private bool esAscendente = true;
        //para filtrar
        private bool? filtroActivo = null;

        public frmTiposDeBombones(IServiceProvider provider)
        {
            InitializeComponent();
            _serviceProvider = provider;
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmTiposDeBombones_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void RecargarGrilla()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var tipoBombonesServicio = scope.ServiceProvider
                    .GetRequiredService<ITipoBombonServicio>();
                try
                {
                    var resultadoConsulta = tipoBombonesServicio
                        .ObtenerPagina(_paginaActual, _cantidadPorPagina,
                        campoOrdenar, esAscendente, filtroActivo);
                    if (resultadoConsulta.IsFailure)
                    {
                        ErrorHelper.MostrarErrores(resultadoConsulta.Errors);
                        return;
                    }

                    MostrarDatosEnGrilla(resultadoConsulta.Value!);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MostrarDatosEnGrilla(ResultadoPaginacionDto<TipoBombonListDto> resultado)
        {
            if (resultado.Items is null ||
                resultado.Items.Count == 0) return;

            _totalPaginas = resultado.TotalPaginas;
            _totalRegistros = resultado.CantidadRegistros;

            _bindingSource.DataSource = resultado.Items;
            dgvDatos.DataSource = _bindingSource;

            int desde = 1 + (_paginaActual - 1) * _cantidadPorPagina;
            int hasta = desde + _cantidadPorPagina;
            if (hasta > _totalRegistros)
            {
                hasta = _totalRegistros;
            }
            lblCantidad.Text=$"Del {desde} a {hasta} de {_totalRegistros}";
            lblPaginas.Text = $"{_paginaActual} de {_totalPaginas}";

            btnPrimero.Enabled = resultado.TieneRegistrosAnteriores;
            btnAnterior.Enabled = resultado.TieneRegistrosAnteriores;
            btnSiguiente.Enabled = resultado.TieneRegistrosSiguientes;
            btnUltimo.Enabled = resultado.TieneRegistrosSiguientes;

        }

        private void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filtroActivo = true;
            _paginaActual = 1;
            tsbFiltrar.BackColor = Color.Orange;
            RecargarGrilla();

        }


        private void noActivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filtroActivo = false;
            _paginaActual = 1;
            tsbFiltrar.BackColor = Color.Orange;
            RecargarGrilla();


        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            filtroActivo = null;
            _paginaActual = 1;
            tsbFiltrar.BackColor = SystemColors.Control;
            RecargarGrilla();
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current==null)
            {
                MessageBox.Show("Debe seleccionar una fila",
                    "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TipoBombonListDto tipoListDto = (TipoBombonListDto)_bindingSource.Current;
            using (var scope = _serviceProvider.CreateScope())
            {
                var tipoBombonServicio = scope.ServiceProvider
                        .GetRequiredService<ITipoBombonServicio>();
                var resultadoConsulta = tipoBombonServicio.ObtenerParaBorrar(tipoListDto.TipoBombonId);
                if (resultadoConsulta.IsFailure)
                {
                    ErrorHelper.MostrarErrores(resultadoConsulta.Errors);
                    return;

                }
                var tipoDeleteDto = resultadoConsulta.Value;
                var dr = MessageBox.Show($"¿Desea borrar el tipo {tipoListDto.Nombre}?",
                    "Confirmar Borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) return;
                try
                {
                    var resultadoEliminacion = tipoBombonServicio
                        .Borrar(tipoDeleteDto!);
                    if (resultadoEliminacion.IsConcurrencyConflict)
                    {
                        ErrorHelper.MostrarErrores(resultadoEliminacion.Errors);
                        RecargarGrilla();
                        return;

                    }
                    if (resultadoEliminacion.IsFailure)
                    {
                        ErrorHelper.MostrarErrores(resultadoEliminacion.Errors);
                        return;

                    }
                    MessageBox.Show("Registro eliminado satisfactoriamente",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RecargarGrilla();
                    if (_paginaActual>_totalPaginas && _totalPaginas>=1)
                    {
                        _paginaActual = _totalPaginas;
                        RecargarGrilla();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                using (frmTipoDeBombonesAe frm = scope.ServiceProvider.GetRequiredService<frmTipoDeBombonesAe>())
                {
                    frm.Text = "Nuevo Tipo de Bombón";
                    frm.ShowDialog();
                    if (frm.DataChanged)
                    {
                        var nuevoId = frm.UltimoId;
                        bool sePuedeVer = filtroActivo is null || filtroActivo == true;
                        if (sePuedeVer)
                        {
                            var tipoServicio = scope.ServiceProvider
                                .GetRequiredService<ITipoBombonServicio>();
                            var resultado = tipoServicio.ObtenerPaginaRegistro(nuevoId, _cantidadPorPagina);
                            if (resultado.IsFailure)
                            {
                                ErrorHelper.MostrarErrores(resultado.Errors);
                                return;
                            }
                            _paginaActual = resultado.Value;
                        }
                        RecargarGrilla();
                        if (sePuedeVer)
                        {
                            var nuevoTipo = _bindingSource.List
                                .Cast<TipoBombonListDto>()
                                .FirstOrDefault(tp => tp.TipoBombonId == nuevoId);
                            if (nuevoTipo is null) return;
                            _bindingSource.Position = _bindingSource.IndexOf(nuevoTipo);

                        }
                        else
                        {
                            MessageBox.Show("Los registros agregados no se pueden mostrar\npor condición de filtro o búsqueda",
                                "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                }
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (_bindingSource.Current==null)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var tipoListDto = (TipoBombonListDto)_bindingSource.Current;
            var seleccionadoId = tipoListDto.TipoBombonId;
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var tipoBombonesServicio = scope.ServiceProvider
                .GetRequiredService<ITipoBombonServicio>();
                    var resultadoConsulta = tipoBombonesServicio
                        .ObtenerParaEditar(tipoListDto.TipoBombonId);
                    if (resultadoConsulta.IsFailure)
                    {
                        ErrorHelper.MostrarErrores(resultadoConsulta.Errors);
                        return;
                    }
                    var tipoEditDto = resultadoConsulta.Value;
                    using (frmTipoDeBombonesAe frm = scope.ServiceProvider
                        .GetRequiredService<frmTipoDeBombonesAe>())
                    {
                        frm.Text = "Editar Tipo de Bombón";
                        frm.SetTipo(tipoEditDto);
                        frm.ShowDialog();
                        var tipoEditado = frm.GetTipo();
                        if (tipoEditado is null) return;
                        bool sePuedeVer = filtroActivo is null ||
                            filtroActivo == tipoEditado.Activo;

                        if (sePuedeVer)
                        {
                            var resultadoPagina = tipoBombonesServicio
                                .ObtenerPaginaRegistro(seleccionadoId, _cantidadPorPagina,
                                filtroActivo);
                            if (resultadoConsulta.IsFailure)
                            {
                                ErrorHelper.MostrarErrores(resultadoPagina.Errors);
                                return;
                            }
                            _paginaActual = resultadoPagina.Value;
                        }

                        if (frm.ConcurrencyConflict)//si hubo concurrencia se recarga la grilla
                        {
                            RecargarGrilla();
                        }
                        if (frm.DataChanged)//si cambiaron los datos se recarga la grilla
                        {
                            RecargarGrilla();
                        }
                        if (sePuedeVer)
                        {
                            var registroEditado = _bindingSource.List
                                .Cast<TipoBombonListDto>()
                                .FirstOrDefault(tb => tb.TipoBombonId == seleccionadoId);
                            if (registroEditado is null) return;
                            _bindingSource.Position = _bindingSource.IndexOf(registroEditado);

                        }
                        else
                        {
                            MessageBox.Show("El registro editado no se puede mostrar\npor condición de filtro o búsqueda",
                                "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            _paginaActual = 1;
            RecargarGrilla();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            _paginaActual--;
            if (_paginaActual == 0)
            {
                _paginaActual = 1;
            }
            RecargarGrilla();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            _paginaActual++;
            if (_paginaActual > _totalPaginas)
            {
                _paginaActual = _totalPaginas;
            }
            RecargarGrilla();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            _paginaActual = _totalPaginas;
            RecargarGrilla();
        }
    }
}
