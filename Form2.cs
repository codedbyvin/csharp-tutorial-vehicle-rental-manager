using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRACTICAL_TEST_1
{
    public partial class frmSummary : Form
    {
        public frmSummary()
        {
            InitializeComponent();
        }

        private VehicleRental mVehicle;

        public VehicleRental Vehicle
        {
            set { mVehicle = value; }
            get { return mVehicle; }
        }

        private void btnaConfirmInformation_Click(object sender, EventArgs e)
        {
            mVehicle.Customer = txtCustomerName.Text;
            mVehicle.Registration = txtVehicleReg.Text;
            mVehicle.Type = txtVehicleType.Text;
            mVehicle.Days = Convert.ToDecimal(txtNumofDays.Text);

            this.Close();
        }

        private void frmSummary_Load(object sender, EventArgs e)
        {
            txtCustomerName.Text = mVehicle.Customer;
            txtVehicleReg.Text = mVehicle.Registration;
            txtVehicleType.Text = mVehicle.Type;
            txtNumofDays.Text = Convert.ToString(mVehicle.Days);
            txtRentalCost.Text = Convert.ToString(mVehicle.Cost);
        }
    }
}
