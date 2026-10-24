using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PRACTICAL_TEST_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            dgvVehicles.DataSource = myVehicles;
        }
        BindingList<VehicleRental> myVehicles = new BindingList<VehicleRental>();
        VehicleRental vehicle;
        private void btnAddRental_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string vehicleReg = txtVehicleReg.Text;
            string vehicleType = cbxVehicleType.Text;
            decimal days = nudNumofDays.Value;

            if(customerName == "" || vehicleReg == "" || vehicleType == "" || days == 0)
            {
                MessageBox.Show("Please enter all fields!");
                return;
            }
            
            vehicle = new VehicleRental();

            vehicle.Customer = customerName;
            vehicle.Registration = vehicleReg;
            vehicle.Type = vehicleType;
            vehicle.Days = days;
            vehicle.Cost = vehicle.CalculateTotal();

            myVehicles.Add(vehicle);
        }

        private void btnClearInputs_Click(object sender, EventArgs e)
        {
            txtCustomerName.Clear();
            txtVehicleReg.Clear();
            cbxVehicleType.SelectedIndex = -1;
            nudNumofDays.Value = 0;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowSummary_Click(object sender, EventArgs e)
        {
            frmSummary summaryForm = new frmSummary();

            int iSelectedIndex = dgvVehicles.CurrentCell.RowIndex;

            summaryForm.Vehicle = myVehicles[iSelectedIndex];
            summaryForm.ShowDialog();

            myVehicles[iSelectedIndex] = summaryForm.Vehicle;
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            using(FileStream file = new FileStream("rentals.ser", FileMode.Create))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(file, myVehicles);

                MessageBox.Show("Data saved successfully!", "Data Saved");
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                using(FileStream file = new FileStream("rentals.ser", FileMode.Open))
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();

                    myVehicles.Clear();

                    var tempList = (BindingList<VehicleRental>)bFormatter.Deserialize(file);

                    foreach(VehicleRental vehicle in tempList)
                    {
                        myVehicles.Add(vehicle);
                    }

                    MessageBox.Show("Data loaded successfully!", "Data Loaded");
                }
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("File not found!", "FileNotFoundException");
            }

            
        }
    }
}
