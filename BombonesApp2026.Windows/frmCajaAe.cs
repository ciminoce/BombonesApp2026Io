using BombonesApp2026.Servicios.DTOs.Caja;
using BombonesApp2026.Windows.Helpers;
using CajaesApp2026.Servicios.Intefaces;

namespace BombonesApp2026.Windows
{
    public partial class frmCajaAe : Form
    {
        private CajaUpdateDto? _cajaDto;
        private readonly ICajaServicio _cajaServicio;
        private bool _esEdicion = false;

        public frmCajaAe(ICajaServicio cajaServicio)
        {
            InitializeComponent();
            _cajaServicio = cajaServicio;

        }
        public int UltimoId { get; private set; }
        public bool DataChanged { get; private set; }
        public bool ConcurrencyConflict { get; private set; }//Agregado para informar de conflicto de concurrencia
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_cajaDto is null)
            {
                chkActivo.Checked = true;
                chkActivo.Enabled = false;
            }
            else
            {
                txtNombreCaja.Text = _cajaDto.Nombre;
                txtDescripcion.Text = _cajaDto.Descripcion;
                txtPrecio.Text = _cajaDto.Precio.ToString();
                nudStock.Value = _cajaDto.Stock;
                txtSurtida.Text = _cajaDto.EsSurtida?"Si":"No";
                txtPrecio.Text = _cajaDto.Precio.ToString();
                txtCantidadBombones.Text = _cajaDto.CantidadBombones.ToString();
                chkActivo.Checked = _cajaDto.Activo;
                _esEdicion = true;

            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                try
                {
                    if (!_esEdicion)
                    {

                        var cajaCreateDto = new CajaCreateDto();
                        cajaCreateDto.Nombre = txtNombreCaja.Text;
                        cajaCreateDto.Descripcion = txtDescripcion.Text;
                        cajaCreateDto.Precio = decimal.Parse(txtPrecio.Text);
                        cajaCreateDto.CantidadBombones = (int)nudCantidadBombones.Value;
                        cajaCreateDto.Stock = (int)nudStock.Value;
                        cajaCreateDto.EsSurtida = chkEsSurtida.Checked;

                        var resultadoAgregar = _cajaServicio.Agregar(cajaCreateDto);
                        if (resultadoAgregar.IsFailure)
                        {
                            ErrorHelper.MostrarErrores(resultadoAgregar.Errors);
                            return;
                        }
                        DataChanged = true;
                        UltimoId = resultadoAgregar.Value;
                        var respuestaAgregarOtro = MessageBox.Show("Registro agregado\n¿Desea agregar otro?",
                                "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                        if (respuestaAgregarOtro == DialogResult.No)
                        {
                            DialogResult = DialogResult.OK;
                        }
                        InicializarControles();

                    }
                    else
                    {
                        if (_cajaDto is null)
                        {
                            _cajaDto = new CajaUpdateDto();
                        }
                        _cajaDto.Nombre = txtNombreCaja.Text;
                        _cajaDto.Descripcion = txtDescripcion.Text;
                        _cajaDto.Precio = decimal.Parse(txtPrecio.Text);
                        _cajaDto.CantidadBombones = (int)nudCantidadBombones.Value;
                        _cajaDto.Stock = (int)nudStock.Value;
                        _cajaDto.EsSurtida = chkEsSurtida.Checked;
                        _cajaDto.Activo = chkActivo.Checked;

                        var resultadoEditar = _cajaServicio
                            .Editar(_cajaDto);
                        if (resultadoEditar.IsConcurrencyConflict)
                        {
                            ErrorHelper.MostrarErrores(resultadoEditar.Errors);

                            ConcurrencyConflict = true;
                            DialogResult = DialogResult.Cancel;

                            Close();
                            return;
                        }
                        if (resultadoEditar.IsFailure)
                        {
                            ErrorHelper.MostrarErrores(resultadoEditar.Errors);
                            return;
                        }
                        DataChanged = true;
                        MessageBox.Show("Registro editado satisfactoriamente",
                            "Mensaje",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InicializarControles()
        {
            txtNombreCaja.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            nudStock.Value = 0;
            txtCantidadBombones.Clear();
            txtSurtida.Clear();
            chkActivo.Checked = true;
            chkActivo.Enabled = false;
            txtNombreCaja.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrWhiteSpace(txtNombreCaja.Text))
            {
                valido = false;
                errorProvider1.SetError(txtNombreCaja, "El nombre es requerido");
            }
            else if (txtNombreCaja.Text.Length > 100)
            {
                valido = false;
                errorProvider1.SetError(txtNombreCaja, "El nombre debe tener no más de 100 caracteres");
            }
            if (txtDescripcion.Text.Length > 250)
            {
                valido = false;
                errorProvider1.SetError(txtDescripcion, "La descripción no puede tener más de 250 caracteres");
            }
            return valido;
        }

        public void SetCaja(CajaUpdateDto? cajaDto)
        {
            _cajaDto = cajaDto;
        }
        public CajaUpdateDto? GetCaja()
        {
            return _cajaDto;
        }


    }
}
