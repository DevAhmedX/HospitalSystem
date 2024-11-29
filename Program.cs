using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystemEditedVersion
{
    public interface IPatient
    { 
        void SetFName(string fName);
        string GetFName();
        void SetLName(string LName);
        string GetLName();
        string GetFullName();
        void SetAge(byte age);
        byte GetAge();
        void SetGender(string gender);
        string GetGender();
        void SetStatus(string status);
        string GetStatus();
    }

    public class Hospital
    {
        private const int maxPatient = 50;
        private byte currentCount = 0;
        private Patient[] patients = new Patient[maxPatient];


        public Hospital()
        {
           
        }
        public void AddPatient(Patient InitialPatient)
        {
            if(currentCount < maxPatient)
            {
                patients[currentCount++] = InitialPatient;
                Console.WriteLine("Patient added successfully.");
            }
            else
            {
                Console.WriteLine("YOU HAVE REACHED THE MAXIMUM NUMBER OF PATIENTS");
            }
        }

        public void RemovePatient(Patient InitialPatient)
        {
            for (int i = 0; i < currentCount; i++)
            {
                if (patients[i] != null &&
            patients[i].GetFullName().Equals(InitialPatient.GetFullName(),
            StringComparison.OrdinalIgnoreCase))
                {
                    for (int j = i; j < currentCount - 1; j++)
                    {
                        patients[j] = patients[j + 1];
                    }
                    currentCount--;
                    patients[currentCount] = null;

                    Console.WriteLine("Patient deleted successfully.");
                    return; 
                }
            }

            Console.WriteLine("Patient not found.");
        }

        public void PrintPatients()
        {
            Console.WriteLine("List of Patients:");
            for (int i = 0; i < currentCount; i++)
            {
                var patient = patients[i];
                Console.WriteLine($"Name: {patient.GetFullName()}," +
                    $" Age: {patient.GetAge()}, Gender: {patient.GetGender()}," +
                    $" Status: {patient.GetStatus()}");
            }
        }
    }
    public class Patient : IPatient 
    {
        private string fName;
        private string lName;
        private byte age;
        private string gender;
        private string status;

        public Patient(string fName, string lName, byte? age, string gender, string status)
        {
            this.fName = fName;
            this.lName = lName;
            this.age = (age.HasValue && age > 0 && age < 100) ? age.Value : (byte)0; SetGender(gender);
            this.status = status ?? "Default";
        }

        public void SetFName(string fName) => this.fName = fName;
        public string GetFName() => fName;
        public void SetLName(string lName) => this.lName = lName;
        public string GetLName() => lName;
        public string GetFullName() => $"{fName} {lName}";
        public void SetAge(byte age) => this.age = age;
        public byte GetAge() => age;
        public string GetGender() => gender;
        public string GetStatus() => status;

        public void SetGender(string gender)
        {
            if (string.Equals(gender, "Male", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(gender, "Female", StringComparison.OrdinalIgnoreCase))
            {
                this.gender = gender;
            }
            else
            {
                throw new ArgumentException("Gender must be either 'Male' or 'Female'.");
            }
        }

        public void SetStatus(string status)
        {
            if (string.Equals(status, "Urgent", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(status, "Regular", StringComparison.OrdinalIgnoreCase))
            {
                this.status = status;
            }
            else
            {
                throw new ArgumentException("Status must be either 'Urgent' or 'Regular'.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();

            try
            {
                Patient patient1 = new Patient("Ahmed", "Arafa", 19, "Male", "Regular");
                hospital.AddPatient(patient1);

                Patient patient2 = new Patient("Sara", "Khan", 25, "Female", "Urgent");
                hospital.AddPatient(patient2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            hospital.PrintPatients();
            Console.ReadKey();
        }
    }
}
