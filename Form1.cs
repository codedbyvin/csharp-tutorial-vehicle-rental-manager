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

            // When the form is created, we also want the list to connect the list to the datasource of the datagridview right away.
            dgvVehicles.DataSource = myVehicles;
        }
        BindingList<VehicleRental> myVehicles = new BindingList<VehicleRental>();
        VehicleRental vehicle;
        private void btnAddRental_Click(object sender, EventArgs e)
        {
            // Allowing user inputs.
            string customerName = txtCustomerName.Text;
            string vehicleReg = txtVehicleReg.Text;
            string vehicleType = cbxVehicleType.Text;
            decimal days = nudNumofDays.Value;

            // Check for empty values, and ensure all fields are entered.
            if(customerName == "" || vehicleReg == "" || vehicleType == "" || days == 0)
            {
                MessageBox.Show("Please enter all fields!");
                return;
            }

            
            // Creating an instance of the class VehicleRental -> used the default constructor.
            vehicle = new VehicleRental();

            // Giving the private data members values about the instance.
            vehicle.Customer = customerName;
            vehicle.Registration = vehicleReg;
            vehicle.Type = vehicleType;
            vehicle.Days = days;
            vehicle.Cost = vehicle.CalculateTotal();

            // Adding my object to the list
            myVehicles.Add(vehicle);
        }

        private void btnClearInputs_Click(object sender, EventArgs e)
        {
            // Clearing input fields.
            txtCustomerName.Clear();
            txtVehicleReg.Clear();
            cbxVehicleType.SelectedIndex = -1;
            nudNumofDays.Value = 0;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Closes the form.
            this.Close();
        }

        private void btnShowSummary_Click(object sender, EventArgs e)
        {
            // Creating an instance of the form.
            frmSummary summaryForm = new frmSummary();

            // Returns the current position of the seleted object.
            int iSelectedIndex = dgvVehicles.CurrentCell.RowIndex;

            // Passing the object address(of the object selected)
            // to the private data attribute(mVehicle) of the object "summaryForm".
            summaryForm.Vehicle = myVehicles[iSelectedIndex];

            // Shows the form.
            summaryForm.ShowDialog();

            // Returns the edited object and adds it to the position the selected object was at to overwrite it.
            myVehicles[iSelectedIndex] = summaryForm.Vehicle;
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            // using the "using" statement. It automatically closes and disposes the stream for you.
            // The FileStream object creates the file on the hardware.
            using(FileStream file = new FileStream("rentals.ser", FileMode.Create))
            {
                // The BinaryFormatter object converts/translates the objects of the list"myVehicles" to binary data.
                BinaryFormatter bFormatter = new BinaryFormatter();

                // Using the .Serialize method to translate the objects to binary data.
                bFormatter.Serialize(file, myVehicles);

                MessageBox.Show("Data saved successfully!", "Data Saved");
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                // The FileStream tool here opens the file.
                using(FileStream file = new FileStream("rentals.ser", FileMode.Open))
                {
                    BinaryFormatter bFormatter = new BinaryFormatter();

                    myVehicles.Clear();

                    // The .Deserialize() method returns the type object.
                    // With explicit casting, we tell C# that the returned type is a binding list of the type VehicleRental.
                    // With implicit typing, we tell C# that whatever data type is on the right lock the variable "tempList" to it.
                    var tempList = (BindingList<VehicleRental>)bFormatter.Deserialize(file);

                    // We loop through the collection(tempList), and each element is addeed to the main list"myVehicles".
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
