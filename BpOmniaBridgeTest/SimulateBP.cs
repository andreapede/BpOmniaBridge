using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPS;
using System.Collections;

namespace BpOmniaBridgeTest
{
    public interface IName
    {
        string First { get; set; }
        string Middle { get; set; }
        string Last { get; set; }
        string FullName { get; set; }
    }

    public class DefineIName : IName
    {
        private string name;
        private string middle = "Middle";
        private string lastname;
        private string fullname;

        public DefineIName(string name, string lastname)
        {
            First = name;
            Middle = middle;
            Last = lastname;
            fullname = String.Join(" ", new string[3] { name, middle, lastname });
        }

        public string First
        {
            set { name = value; }
            get { return name; }
        }

        public string Middle
        {
            set { middle = value; }
            get { return middle; }
        }

        public string Last
        {
            set { lastname = value; }
            get { return lastname; }
        }

        public string FullName
        {
            set { fullname = value; }
            get { return fullname; }
        }
    }

    public interface IPatient
    {
        int InternalId { get; set; }
        IName Name { get; set; }
        string Gender { get; set; }
        DateTime DOB { get; set; }
        decimal Height { get; set; }
        decimal Weight { get; set; }
        string Ethnicity { get; set; }
    }

    
    public class DefineIPatient : IPatient
    {
        private int id;
        private string gender;
        private DateTime dob;
        private decimal height;
        private decimal weight;
        private string ethnicity;
        private IName name;

        public DefineIPatient(dynamic id, dynamic name, dynamic lastname, dynamic gender, dynamic dob, dynamic height, dynamic weight, dynamic ethnicity)
        {
            InternalId = id;
            name = new DefineIName(name, lastname);
            DOB = dob;
            Height = height;
            Weight = weight;
            Ethnicity = ethnicity;
            Gender = gender;
            Name = name;
        }  

        public int InternalId
        {
            set { id = value; }
            get { return id; }
        }

        public IName Name
        {
            set { name = value; }
            get { return name; }
        }

        public string Gender
        {
            set { gender = value; }
            get { return gender; }
        }

        public DateTime DOB
        {
            set { dob = value; }
            get { return dob; }
        }

        public decimal Height
        {
            set { height = value; }
            get { return height; }
        }

        public decimal Weight
        {
            set { weight = value; }
            get { return weight; }
        }

        public string Ethnicity
        {
            set { ethnicity = value; }
            get { return ethnicity; }
        }
    }


    public interface IUser
    {
        int UserID { get; set; }
        string UserName { get; set; }
    }

    public class DefineIUser: IUser
    {
        private int id;
        private string name;

        public DefineIUser(dynamic id, dynamic name)
        {
            UserID = id;
            UserName = name;
        }

        public int UserID
        {
            set { id = value; }
            get { return id; }
        }

        public string UserName
        {
            set { name = value; }
            get { return name; }
        }
    }

    public interface ITest
    {
        IPatient Patient { get; set; }
        IUser User { get; set; }
    }

    public interface BP
    {
        ITest CurrentTest { get; set; }
    }

    public class CurrentTest : ITest
    {
        private Hashtable patientDetail;
        private Hashtable userDetail;
        private IPatient patient;
        private IUser user;

        public CurrentTest(Hashtable patientHash, Hashtable userHash)
        {
            patientDetail = patientHash;
            userDetail = userHash;
            user = new DefineIUser(userDetail["id"], userDetail["name"]);
            patient = new DefineIPatient(patientDetail["id"],
                                        patientDetail["name"],
                                        patientDetail["lastname"],
                                        patientDetail["gender"],
                                        patientDetail["dob"],
                                        patientDetail["height"],
                                        patientDetail["weight"],
                                        patientDetail["ethnicity"]);
            Patient = patient;
            User = user;

        }

        public IPatient Patient
        {
            set { patient = value; }
            get { return patient; }
        }

        public IUser User
        {
            set { user = value; }
            get { return user; }
        }
    }

    public class SimulateBP : BP
    {
        private Hashtable patientDetail;
        private Hashtable userDetail;
        private ITest test;

        public SimulateBP(Hashtable patient, Hashtable user)
        {
            patientDetail = patient;
            userDetail = user;
            test = new CurrentTest(patientDetail, userDetail);
            CurrentTest = test;
        }

        public ITest CurrentTest
        {
            set { test = value; }
            get { return test; }
        }
    }
}


