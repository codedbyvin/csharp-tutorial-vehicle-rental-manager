using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PRACTICAL_TEST_1
{
    [Serializable]
    public class VehicleRental
    {
        private string mCustomerName;
        private string mVehicleReg;
        private string mVehicleType;
        private decimal mDays;
        private int mRentalCost;
        public VehicleRental()
        {
            mCustomerName = "";
            mVehicleReg = "";
            mVehicleType = "";
            mDays = 0;
        }
        public VehicleRental(string customerName, string vehicleReg, string vehicleType, decimal days, int rentalCost)
        {
            mCustomerName = customerName;
            mVehicleReg = vehicleReg;
            mVehicleType = vehicleType;
            mDays = days;
            mRentalCost = rentalCost;
        }

        public string Customer
        {
            get { return mCustomerName; }
            set { mCustomerName = value; }
        }
        public string Registration
        {
            get { return mVehicleReg; }
            set { mVehicleReg = value; }
        }
        public string Type
        {
            get { return mVehicleType; }
            set { mVehicleType = value; }
        }
        public decimal Days
        {
            get { return mDays; }
            set { mDays = value; }
        }
        public int Cost
        {
            get { return mRentalCost; }
            set { mRentalCost = value; }
        }

        public int CalculateTotal()
        {
            int rentalCost = 0;
            int SedanRate = 450;
            int SUVRate = 650;
            int TruckRate = 800;

            switch (mVehicleType)
            {
                case "Sedan":
                    rentalCost = Convert.ToInt32(mDays) * SedanRate;
                    break;
                case "SUV":
                    rentalCost = Convert.ToInt32(mDays) * SUVRate;
                    break;
                case "Truck":
                    rentalCost = Convert.ToInt32(mDays) * TruckRate;
                    break;
            }

            return rentalCost;
        }
    }
}
