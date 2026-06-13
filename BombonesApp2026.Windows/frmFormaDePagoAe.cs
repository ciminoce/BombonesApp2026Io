using BombonesApp2026.Servicios.DTOs.FormaDePago;
using BombonesApp2026.Servicios.Intefaces;
using BombonesApp2026.Windows.Helpers;

namespace BombonesApp2026.Windows
{
    public partial class frmFormaDePagoAe : Form
    {
        private FormaDePagoUpdateDto? _formDePagoUpdateDto;
        private readonly IFormaDePagoServicio _formaServicio;
        private bool _esEdicion = false;
        public frmFormaDePagoAe(IFormaDePagoServicio formaServicio)
        {
            InitializeComponent();
            _formaServicio = formaServicio;
        }

        public bool DataChanged { get; private set; }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (_formDePagoUpdateDto is null)
            {
                chkActivo.Checked = true;
                chkActivo.Enabled = false;

            }
            else
            {
                txtForma.Text = _formDePagoUpdateDto.Nombre;
                chkActivo.Checked = _formDePagoUpdateDto.Activo;
                chkActivo.Enabled = true;
                _esEdicion = true;
            }

        }

        public void SetTipo(FormaDePagoUpdateDto? formaEditDto)
        {
            _formDePagoUpdateDto = formaEditDto;
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

                        var _formaDePagoCreateDto = new FormaDePagoCreateDto();
                        _formaDePagoCreateDto.Nombre = txtForma.Text;
                        _formaDePagoCreateDto.Activo = true;
                        var resultadoAgregar = _formaServicio.Agregar(_formaDePagoCreateDto);
                        if (resultadoAgregar.IsFailure)
                        {
                            ErrorHelper.MostrarErrores(resultadoAgregar.Errors);
                            return;
                        }
                        DataChanged = true;
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
                        if (_formDePagoUpdateDto is null)
                        {
                            _formDePagoUpdateDto = new FormaDePagoUpdateDto();
                        }
                        _formDePagoUpdateDto.Nombre = txtForma.Text;
                        _formDePagoUpdateDto.Activo = chkActivo.Checked;

                        var resultadoEditar = _formaServicio
                            .Editar(_formDePagoUpdateDto);
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
            txtForma.Clear();
            chkActivo.Checked = true;
            chkActivo.Enabled = false;
            txtForma.Focus();
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtForma.Text))
            {
                valido = false;
                errorProvider1.SetError(txtForma, "El nombre es requerido");

            }
            return valido;
        }

    }
}
