using BombonesApp2026.Servicios.DTOs.FormaDePago;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BombonesApp2026.Windows
{
    public partial class frmFormaDePagoAe : Form
    {
        public frmFormaDePagoAe()
        {
            InitializeComponent();
        }

        public bool DataChanged { get; internal set; }

        internal void SetTipo(FormaDePagoUpdateDto? formaEditDto)
        {
            throw new NotImplementedException();
        }
    }
}
