using BombonesApp2026.Servicios.DTOs.FormaDePago;
using BombonesApp2026.Servicios.Intefaces;
using BombonesApp2026.Windows.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace BombonesApp2026.Windows
{
    public partial class frmFormasDePago : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private List<FormaDePagoListDto>? _listaFormas;
        private bool filtroActivo = false;
        public frmFormasDePago(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void tsbCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila",
                    "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var filaSeleccionada = dgvDatos.SelectedRows[0];
            if (filaSeleccionada.Tag is null) return;
            FormaDePagoListDto formaListDto = (FormaDePagoListDto)filaSeleccionada.Tag;
            using (var scope = _serviceProvider.CreateScope())
            {
                var formaDePagoServicio = scope.ServiceProvider
                        .GetRequiredService<IFormaDePagoServicio>();
                var dr = MessageBox.Show($"¿Desea borrar la forma de pago {formaListDto.Nombre}?",
                    "Confirmar Borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No) return;
                try
                {
                    var resultadoEliminacion = formaDePagoServicio
                        .Borrar(formaListDto.FormaDePagoId!);
                    if (resultadoEliminacion.IsFailure)
                    {
                        ErrorHelper.MostrarErrores(resultadoEliminacion.Errors);
                        return;

                    }
                    MessageBox.Show("Registro eliminado satisfactoriamente",
                        "Mensaje",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RecargarGrilla();
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
                using (frmFormaDePagoAe frm = scope.ServiceProvider.GetRequiredService<frmFormaDePagoAe>())
                {
                    frm.Text = "Nuevo Tipo de Bombón";
                    frm.ShowDialog();
                    if (frm.DataChanged)
                    {
                        RecargarGrilla();
                    }

                }
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedCells.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila de la grilla",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var filaSeleccionada = dgvDatos.SelectedRows[0];
            if (filaSeleccionada.Tag is null) return;
            var formaListDto = (FormaDePagoListDto)filaSeleccionada.Tag;
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var formaDePagoServicio = scope.ServiceProvider
                .GetRequiredService<IFormaDePagoServicio>();
                    var resultadoConsulta = formaDePagoServicio
                        .ObtenerParaEditar(formaListDto.FormaDePagoId);
                    if (resultadoConsulta.IsFailure)
                    {
                        ErrorHelper.MostrarErrores(resultadoConsulta.Errors);
                        return;
                    }
                    var formaEditDto = resultadoConsulta.Value;
                    using (frmFormaDePagoAe frm = scope.ServiceProvider
                        .GetRequiredService<frmFormaDePagoAe>())
                    {
                        frm.Text = "Editar Tipo de Bombón";
                        frm.SetTipo(formaEditDto);
                        frm.ShowDialog();
                        if (frm.DataChanged)//si cambiaron los datos se recarga la grilla
                        {
                            RecargarGrilla();
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
        private void RecargarGrilla()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var formaDePagoServicio = scope.ServiceProvider
                    .GetRequiredService<IFormaDePagoServicio>();
                try
                {
                    var resultadoConsulta = formaDePagoServicio.ObtenerTodos();
                    if (resultadoConsulta.IsFailure)
                    {
                        ErrorHelper.MostrarErrores(resultadoConsulta.Errors);
                        return;
                    }
                    _listaFormas = resultadoConsulta.Value;
                    MostrarDatosEnGrilla(_listaFormas);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MostrarDatosEnGrilla(List<FormaDePagoListDto>? lista)
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            if (lista is null ||
                lista.Count == 0) return;
            foreach (var item in lista)
            {
                var r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, item);
                GridHelper.AgregarFila(r, dgvDatos);
            }
            lblCantidad.Text = lista.Count.ToString();
        }

        private void frmFormasDePago_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void activosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var formaDePagoServicio = scope.ServiceProvider
                    .GetRequiredService<IFormaDePagoServicio>();
                try
                {
                    var resultadoConsulta = formaDePagoServicio.FiltrarPorActivo(true);
                    if (resultadoConsulta.IsFailure)
                    {
                        ErrorHelper.MostrarErrores(resultadoConsulta.Errors);
                        return;
                    }
                    _listaFormas = resultadoConsulta.Value;
                    MostrarDatosEnGrilla(_listaFormas);
                    ManejarControles(true);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void ManejarControles(bool v)
        {
            filtroActivo = v;
            tsbFiltrar.BackColor = filtroActivo ? Color.Orange : SystemColors.Control;

            tsbNuevo.Enabled = !v;
            tsbEditar.Enabled = !v;
            tsbBorrar.Enabled = !v;
        }

        private void noActivosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var formaDePagoServicio = scope.ServiceProvider
                    .GetRequiredService<IFormaDePagoServicio>();
                try
                {
                    var resultadoConsulta = formaDePagoServicio.FiltrarPorActivo(false);
                    if (resultadoConsulta.IsFailure)
                    {
                        ErrorHelper.MostrarErrores(resultadoConsulta.Errors);
                        return;
                    }
                    _listaFormas = resultadoConsulta.Value;
                    MostrarDatosEnGrilla(_listaFormas);
                    ManejarControles(true);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            RecargarGrilla();
            ManejarControles(false);
        }
    }
}
