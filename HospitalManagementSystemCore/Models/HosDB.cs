using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using Newtonsoft.Json;
using System.Collections;

namespace HospitalManagementSystem.Models
{
    public class HosDB
    {
        SqlConnection con;
        public HosDB()
        {
            string conStr = "Data Source=عمار\\MSSQLSERVER01;Initial Catalog=HospitalDB;Integrated Security=True";
            con = new SqlConnection(conStr);
        }
        public string isDocOrMang(string sid)
        {
            string Q = "SELECT COUNT(*) FROM DOCTOR_MANAGES_DEPARTMENT WHERE Doctor_ID = '" + sid + "'";
            int t = (int)FunctionExecuteScalar(Q);
            if(t == 0)
            {
                return "DOCTOR2";
            }
            else
            {
                return "DOCTOR";
            }
        }
        public string getPatientIDByEmail(string email)
        {
            string Q = "SELECT PATIENT_ID FROM PATIENT WHERE PATIENT_Email = '" + email + "'";
            return (string)FunctionExecuteScalar(Q);
        }
        public bool CheckPassword(string userID, string enPassword)
        {
            string pass = "";
            if (GetUserType(userID) == "NONE")
            {
                return false;
            }
            string Q = "SELECT Password FROM " + GetUserType(userID) + " WHERE Staff_ID = '" + userID + "'"; ;
            pass = (string)FunctionExecuteScalar(Q);
            if (pass == (string)enPassword) { return true; }
            else { return false; }
        }

        public string IncrementAppointmentID(string appointmentID)
        {
            string numericPart = appointmentID.Substring(4);
            int numericValue = int.Parse(numericPart);
            int newNumericValue = numericValue + 1;
            string newAppointmentID = appointmentID.Substring(0, 4) + newNumericValue.ToString("D3");
            return newAppointmentID;
        }

        private string GetDayOfWeek(string dateString)
        {
            DateTime date;
            if (DateTime.TryParseExact(dateString, "MM-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                return date.ToString("dddd");
            }
            else
            {
                return "Invalid Date";
            }
        }

        public static string ConvertDateFormat(string date)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(date, "MM-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate.ToString("yyyy-MM-dd");
            }
            else
            {
                return "Invalid Date";
            }
        }

        public void addAppointment(string pid, string span,string type, string notes,string docName,string date)
        {
            string Qmax = "SELECT MAX(Appointment_ID) AS MaxAppointmentID FROM Appointment";
            string maxApptID = IncrementAppointmentID((string)FunctionExecuteScalar(Qmax));
            string[] names = docName.Split(' ');
            string QDoc = $"SELECT Staff_ID from DOCTOR where DOCTOR.First_name = '{names[0]}' and DOCTOR.Last_name = '{names[1]}'";
            string docID = (string)FunctionExecuteScalar(QDoc);
            string QRoom = $"SELECT ROOM_ID from DOCTOR_WORK_AT Where Doctor_ID = '{docID}' and working_Day = '{GetDayOfWeek(date)}'";
            string roomID = (string)FunctionExecuteScalar(QRoom);
            string Q = $"insert APPOINTMENT values('{maxApptID}','{span}','{ConvertDateFormat(date)}','{type}','{notes}','{docID}','{pid}','{roomID}') ";
            FunctionExecuteNonQuery(Q);
        }


        public DataTable GetAppointmentsStartingHours(string docName, string date)
        {
            string[] names = docName.Split(' ');
            DataTable dt = new DataTable();
            string Q = $"select Time_of_Appointment from APPOINTMENT, DOCTOR where DOCTOR.First_name = '{names[0]}' and DOCTOR.Last_name = '{names[1]}' and Doctor.Staff_ID = Appointment.DOCTOR_ID and Date_of_Appointment = '{date}'";
            return FunctionExecuteReader(Q);
        }


        public DataTable GetAllDeptsNames()
        {
            string Q = "SELECT Name from department";
            return FunctionExecuteReader(Q);
        }

        public DataTable GetWorkingDayDoctor(string docName)
        {
            string[] words = docName.Split(' ');
            String Q = $"select working_Day from DOCTOR_WORK_AT, DOCTOR where DOCTOR.First_name = '{words[0]}' and DOCTOR.Last_name = '{words[1]}' and DOCTOR.Staff_ID = DOCTOR_WORK_AT.Doctor_ID";
            return FunctionExecuteReader(Q);
        }
        public DataTable GetDoctorNameByDeptName(string deptname)
        {
            string Q = $"select doctor.First_name + ' ' + DOCTOR.Last_name as Doctor_Name from DOCTOR, DEPARTMENT where Doctor.Department_number = Department.Department_number and DEPARTMENT.Name = '{deptname}'";
            return FunctionExecuteReader(Q);
        }


        public TimeSpan GetDoctorWorkingStartHours(string docName, string workingDay) {
            string[] docnames = docName.Split(" ");
            string Q = $"select start_Hour from DOCTOR_WORK_AT, DOCTOR where DOCTOR.First_name = '{docnames[0]}' and DOCTOR.Last_name = '{docnames[1]}' and Doctor.Staff_ID = DOCTOR_WORK_AT.Doctor_ID and DOCTOR_WORK_AT.working_Day = '{workingDay}'";
            return (TimeSpan)FunctionExecuteScalar(Q);
        }
        public TimeSpan GetDoctorWorkingEndHours(string docName, string workingDay)
        {
            string[] docnames = docName.Split(" ");
            string Q = $"select end_Hour from DOCTOR_WORK_AT, DOCTOR where DOCTOR.First_name = '{docnames[0]}' and DOCTOR.Last_name = '{docnames[1]}' and Doctor.Staff_ID = DOCTOR_WORK_AT.Doctor_ID and DOCTOR_WORK_AT.working_Day = '{workingDay}'";
            return (TimeSpan)FunctionExecuteScalar(Q);
        }

        public DataTable getDoctorsPa()
        {

            string Q = "SELECT * FROM DOCTOR";
            DataTable dt1 = FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Doctor Name");
            res.Columns.Add("Rating");
            res.Columns.Add("Number of Feedback");
            res.Columns.Add("Department Name");
            res.Columns.Add("Doctor ID");
            res.Columns.Add("Department Number");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q2 = "SELECT First_Name + ' ' + Last_Name AS DNAME FROM DOCTOR WHERE Staff_ID = '" + dt1.Rows[i][2] + "'";
                string dname = (string)FunctionExecuteScalar(Q2);
                string Q3 = "SELECT AVG(Rating) FROM FEEDBACK AS F WHERE Visit_ID IN(SELECT Visit_ID FROM VISIT AS V WHERE Appointment_ID IN( SELECT Appointment_ID FROM APPOINTMENT AS A WHERE A.Appointment_ID = V.Appointment_ID AND A.DOCTOR_ID = '" + dt1.Rows[i][2] + "'))";
                int avg = 0;
                object result = FunctionExecuteScalar(Q3);
                if (result != null && result != DBNull.Value)
                {
                    avg = Convert.ToInt32(result);
                }
                string Q4 = "SELECT COUNT(*) FROM FEEDBACK WHERE Visit_ID IN (SELECT Visit_ID FROM VISIT WHERE Appointment_ID IN (SELECT Appointment_ID FROM APPOINTMENT WHERE DOCTOR_ID= '" + dt1.Rows[i][2] + "'))";
                int num = (int)FunctionExecuteScalar(Q4);
                string Q6 = "SELECT Name FROM DEPARTMENT WHERE Department_number = '" + dt1.Rows[i][4] + "'";
                string deptname = (string)FunctionExecuteScalar(Q6);
                res.Rows.Add(dname, avg, num, deptname, dt1.Rows[i][2], dt1.Rows[i][4]);
            }
            return res;
        }
        public DataTable getDeptsPa()
        {
            string Q = "SELECT * FROM DEPARTMENT";
            DataTable dt1 = FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Department Name");
            res.Columns.Add("Number Of Doctors");
            res.Columns.Add("Department Average Rates");
            res.Columns.Add("Department number");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q2 = "SELECT COUNT(*) FROM DOCTOR WHERE Department_number= '" + dt1.Rows[i][1] + "'";
                int count = (int)FunctionExecuteScalar(Q2);
                string Q3 = "SELECT AVG(Rating) FROM FEEDBACK WHERE Visit_ID IN(SELECT Visit_ID FROM VISIT WHERE Appointment_ID IN(SELECT Appointment_ID FROM APPOINTMENT WHERE DOCTOR_ID IN(SELECT Staff_ID FROM DOCTOR WHERE Department_number = " + dt1.Rows[i][1] + ")))";
                int avg = 0;
                object result = FunctionExecuteScalar(Q3);
                if (result != null && result != DBNull.Value)
                {
                    avg = Convert.ToInt32(result);
                }

                res.Rows.Add(dt1.Rows[i][0], count, avg, dt1.Rows[i][1]);
            }
            return res;
        }
        public DataTable getTestsPa(string pid)
        {
            string Q = "SELECT DateConducted, Results,Test_ID FROM TEST_DONE_BY_TECH WHERE Test_ID IN (SELECT Test_ID FROM TEST WHERE Visit_ID IN (SELECT Visit_ID FROM VISIT WHERE Appointment_ID IN(SELECT Appointment_ID FROM APPOINTMENT WHERE PATIENT_ID = '" + pid + "'))) ";
            DataTable dt1 = FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Test Name");
            res.Columns.Add("Date Requested", typeof(DateTime));
            res.Columns.Add("Date Conducted", typeof(DateTime));
            res.Columns.Add("Results");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q2 = "SELECT Date_of_test FROM TEST WHERE Test_ID= '" + dt1.Rows[i][2] + "'";
                DateTime dateR = (DateTime)FunctionExecuteScalar(Q2);
                string Q3 = "SELECT Type FROM TEST WHERE Test_ID= '" + dt1.Rows[i][2] + "'";
                string tname = (string)FunctionExecuteScalar(Q3);
                res.Rows.Add(tname, dateR, dt1.Rows[i][0], dt1.Rows[i][1]);
            }
            return res;
        }
        public void AddFeedback(string vid, int rating, string comment)
        {
            string Q = "INSERT INTO FEEDBACK VALUES ('" + vid + "', "+ rating+ ", '" + comment + "')";
            FunctionExecuteNonQuery(Q);
        }
        public string getPnameByID(string pid)
        {
            string Q = "SELECT First_Name + ' ' + Last_Name AS PNAME FROM PATIENT WHERE Patient_ID = '" + pid + "'";
            return (string)FunctionExecuteScalar(Q);
        }
        public void deleteAppt(string apptID)
        {
            string Q = "DELETE FROM APPOINTMENT WHERE Appointment_ID = '" + apptID + "'";
            FunctionExecuteNonQuery(Q);
        }
        public DataTable getApptsPa(string pid)
        {
            string Q = "SELECT * FROM APPOINTMENT WHERE PATIENT_ID = '" + pid + "' ORDER BY Date_of_Appointment ASC, Time_of_Appointment ASC";
            DataTable dt1 = FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Doctor Name");
            res.Columns.Add("Room ID");
            res.Columns.Add("Date", typeof(DateTime));
            res.Columns.Add("Time");
            res.Columns.Add("Appt ID");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q2 = "SELECT (First_Name + ' ' + Last_Name) AS Dname FROM DOCTOR WHERE Staff_ID= '" + dt1.Rows[i][5] + "'";
                string dname = (string)FunctionExecuteScalar(Q2);
                res.Rows.Add(dname, dt1.Rows[i][7], dt1.Rows[i][2], dt1.Rows[i][1], dt1.Rows[i][0]);
            }
            return res;
        }
        public DataTable getVisitsPa(string pid)
        {
            string Q = "SELECT * FROM VISIT AS V WHERE V.Appointment_ID IN (SELECT A.Appointment_ID FROM APPOINTMENT AS A WHERE A.Appointment_ID = V.Appointment_ID AND A.PATIENT_ID = '" + pid + "')";
            DataTable dt1 = FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Doctor Name");
            res.Columns.Add("Room ID");
            res.Columns.Add("Date", typeof(DateTime));
            res.Columns.Add("Visit ID");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q5 = "SELECT DOCTOR_ID FROM APPOINTMENT WHERE APPOINTMENT_ID = '" + dt1.Rows[i][3] + "'";
                string did = (string)FunctionExecuteScalar(Q5);
                string Q6 = "SELECT Room_ID FROM APPOINTMENT WHERE APPOINTMENT_ID = '" + dt1.Rows[i][3] + "'";
                string rid = (string)FunctionExecuteScalar(Q6);
                string Q2 = "SELECT (First_Name + ' ' + Last_Name) AS Dname FROM DOCTOR WHERE Staff_ID= '" + did + "'";
                string dname = (string)FunctionExecuteScalar(Q2);
                res.Rows.Add(dname, rid, dt1.Rows[i][1], dt1.Rows[i][0]);
            }
            return res;
        }


        public void AddStockMedicine(string name, int amount)
        {
            string Q = $"UPDATE medicine SET Amount_Available = Amount_Available + {amount} WHERE Medicine_Name = '{name}'";
            FunctionExecuteNonQuery(Q);
        }


        public void DecreaseMedicine(string name,string ID)
        {
            string Q = $"UPDATE medicine SET Amount_Available = Amount_Available - 1 WHERE Medicine_Name = '{name}'";
            string Q2 = $"INSERT INTO PHARMACIST_SELL_MEDICINE values('{ID}','{name}')";
            try
            {
                con.Open();
                SqlCommand command1 = new SqlCommand(Q, con);
                command1.ExecuteNonQuery();
                SqlCommand command2 = new SqlCommand(Q2, con);
                command2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally { con.Close(); }

        }

        public void AddMedicine(string name, int amount)
        {
            string Q = $"Insert Into Medicine values ('{name}',{amount})";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(Q, con);
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally { con.Close(); }

        }
        private String GetQueryStringUpdate(List<object> list, Dictionary<string,List<String>> keyValuePairs,string profession)
        {
            string s = $"UPDATE {profession} SET ";
            for (int i = 0;i< list.Count;i++) {
                s += keyValuePairs[profession.ToLower()][i];
                s += "=";
                if (list[i].GetType() == typeof(int))
                {
                    s += list[i];
                }
                else
                {
                    s += $"'{list[i]}'";
                }
                if (i != list.Count - 1)
                {
                    s += ",";
                }
            }
            return s;
        }

        public void UpdateUserAdmin(List<object> list,string profession, List<string> ID)
        {
            Dictionary<string, List<String>> keyValuePairs = new Dictionary<string, List<String>>()
            {
                { "doctor" ,new List<String>() {"First_Name","Last_Name","Staff_ID","National_ID","Department_Number", "Age","Gender","Street","City","Governorate","Phone_Number","password","Staff_Email" } },
                { "pharmacist" ,new List<String>() {"First_Name","Last_Name","Staff_ID","National_ID", "Age","Gender","Street","City","Governorate","Phone_Number","password","Staff_Email" } },
                { "nurse" ,new List<String>() {"First_Name","Last_Name","Staff_ID","National_ID", "Department_Number" ,"Age","Gender","Phone_Number","Street","City","Governorate","password","Staff_Email" } },
                { "labtechnician" ,new List<String>() {"First_Name","Last_Name","Staff_ID","National_ID", "Age","Gender", "Phone_Number", "Street","City","Governorate","password","Staff_Email" } },
                { "patient" ,new List<String>() {"First_Name","Last_Name", "Patient_ID", "National_ID", "Age","Gender","Street","City","Governorate","Phone_Number","password","Patient_Email" } },
                { "department" ,new List<String>() {"Name","Department_number", "Speciality", "password"} },
                { "nurse_serve_at" ,new List<String>() {"Room_ID","Nurse_ID", "Working_Day", "start_Hour", "end_Hour" } },
                { "doctor_work_at" ,new List<String>() {"Room_ID","Doctor_ID", "Working_Day", "start_Hour", "end_Hour" } }
            };
            Dictionary<string, List<string>> keyValuePairsProfession = new Dictionary<string, List<string>>()
                {
                    { "doctor", new List<string> { "Staff_ID" } },
                    { "pharmacist", new List<string> { "Staff_ID" } },
                    { "labtechnician", new List<string> { "Staff_ID" } },
                    { "nurse", new List<string> { "Staff_ID" } },
                    { "patient", new List<string> { "Patient_ID" } },
                    { "department", new List<string> { "Department_number" } },
                    { "nurse_serve_at", new List<string> { "Nurse_ID", "working_Day" } },
                    { "doctor_work_at", new List<string> { "Doctor_ID", "working_Day" } },
                };
            String Q = GetQueryStringUpdate(list, keyValuePairs, profession);
            Q += $" where ";
            for (int i = 0; i < keyValuePairsProfession[profession.ToLower()].Count; i++)
            {
                Q += $"{keyValuePairsProfession[profession.ToLower()][i]} = '{ID[i]}'";
                if (i != keyValuePairsProfession[profession.ToLower()].Count - 1)
                {
                    Q += " and ";
                }
            }
            
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(Q, con);
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally { con.Close(); }


        }


        public string GetUserType(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "NONE";
            }

            switch (id[0])
            {
                case 'D':
                    return "DOCTOR";
                case 'N':
                    return "NURSE";
                case 'L':
                    return "LabTechnician";
                case 'P':
                    return "Pharmacist";
                case 'A':
                    return "ADMIN";
                default:
                    return "NONE";
            }
        }
        public DataTable getAnnc() {
            string Q = "SELECT * FROM ANNOUNCEMENT ORDER BY annc_date DESC";
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Manger Name");
            res.Columns.Add("Announcement Date");
            res.Columns.Add("Announcement Content");
            for (int i =0; i < dt1.Rows.Count; i++)
            {
                string managerName = getStaffFnameByID((string)dt1.Rows[i][1]);
                res.Rows.Add(managerName, (DateTime)dt1.Rows[i][3], (string)dt1.Rows[i][2]);

            }             
            return res;
        }

        public void addTest(string vid, string tname)
        {
            string todayDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            string Q1 = "SELECT MAX(CAST(SUBSTRING(test_id, 2, LEN(test_id) - 1) AS INTEGER)) AS max_test_id FROM test WHERE test_id LIKE 'T%'";
            int maxtestid = (int)FunctionExecuteScalar(Q1);
            string Q2 = $"INSERT INTO TEST (Test_ID, Date_of_test, Type, Visit_ID, FromVisit) VALUES ('T{maxtestid + 1}','{todayDate}','{tname}', '{vid}', 1)";
            FunctionExecuteNonQuery(Q2);
        }
        public string addVisit(string apptID, string diagnosis)
        {
            string todayDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            string Q1 = "SELECT MAX(CAST(SUBSTRING(visit_id, 2, LEN(visit_id) - 1) AS INTEGER)) AS max_visit_id FROM VISIT WHERE Visit_id LIKE 'V%'";
            int maxVisitId = (int)FunctionExecuteScalar(Q1);
            maxVisitId++;
            string Q = "INSERT INTO VISIT (Visit_ID, Visit_DATE, Diagnosis, Appointment_ID) VALUES ('V" + maxVisitId + "', '" + todayDate + "', '" + diagnosis + "', '" + apptID + "')";
            FunctionExecuteNonQuery(Q);
            return "V"+maxVisitId;
        }
        public DataTable getApptsToday(string id, string today)
        {
            string Q = "SELECT * FROM APPOINTMENT WHERE Appointment_ID NOT IN(SELECT Appointment_ID FROM VISIT) AND DOCTOR_ID = '" + id + "' AND Date_of_Appointment = '" + today + "'";
            DataTable dt1 = (DataTable)FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Patient Name");
            res.Columns.Add("Type");
            res.Columns.Add("Date");
            res.Columns.Add("Time");
            res.Columns.Add("Notes");
            res.Columns.Add("Appt_id");
            res.Columns.Add("Patient ID");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q2 = "SELECT PATIENT_ID FROM APPOINTMENT WHERE Appointment_ID= '" + dt1.Rows[i][0] + "'";
                string pid = (string)FunctionExecuteScalar(Q2);
                string Q3 = "SELECT First_Name FROM PATIENT WHERE Patient_ID = '" + pid + "'";
                string Q4 = "SELECT Last_Name FROM PATIENT WHERE Patient_ID = '" + pid + "'";
                string pname = (string)FunctionExecuteScalar(Q3) + " " + (string)FunctionExecuteScalar(Q4);

                res.Rows.Add(pname, dt1.Rows[i][3], dt1.Rows[i][2], dt1.Rows[i][1], dt1.Rows[i][4],  dt1.Rows[i][0], pid);
            }
            return res;
        }
        public void AddAnnc(string id, string date, string content)
        {
            string Q1 = "SELECT MAX(CAST(SUBSTRING(annc_id, 2, LEN(annc_id) - 1) AS INTEGER)) AS max_annc_id FROM ANNOUNCEMENT WHERE annc_id LIKE 'A%'";
            int maxAnncId = (int)FunctionExecuteScalar(Q1);
            maxAnncId++;
            string Q = "INSERT INTO ANNOUNCEMENT (annc_ID, Manager_ID, content, annc_date) VALUES('A" + maxAnncId + "', '" + id + "', '" + content + "', '" + date + "')";
            FunctionExecuteNonQuery(Q);
        }
        public int getNuFeebackDept(string id)
        {
            String Q1 = "SELECT Department_number FROM DOCTOR WHERE Staff_ID = '" + id + "'";
            int dnumber = (int)FunctionExecuteScalar(Q1);
            string Q2 = "SELECT COUNT(*) FROM FEEDBACK WHERE Visit_ID IN(SELECT Visit_ID FROM VISIT WHERE Appointment_ID IN(SELECT Appointment_ID FROM APPOINTMENT WHERE DOCTOR_ID IN(SELECT Staff_ID FROM DOCTOR WHERE Department_number = " + dnumber + ")))";
            return (int)FunctionExecuteScalar(Q2);
        }

        public string getDeptName(string id)
        {
            string Q = "SELECT Department_number FROM DOCTOR WHERE Staff_ID = '" + id + "'";
            int dnumber= (int)FunctionExecuteScalar(Q);
            string Q2 = "SELECT Name FROM DEPARTMENT WHERE Department_number = '" + dnumber + "'";
            return (string)FunctionExecuteScalar(Q2);
        }
        public int getDepartmentNumber (string id)
        {
            string Q = "SELECT Department_number FROM DOCTOR WHERE Staff_ID = '" + id + "'";
            return (int)FunctionExecuteScalar(Q);
        }
        public int getDeptAvgRate(string id)
        {
            string Q1 = "SELECT Department_number FROM DOCTOR WHERE Staff_ID = '" + id + "'";
            int dnumber = (int)FunctionExecuteScalar(Q1);
            string Q2 = "SELECT AVG(Rating) FROM FEEDBACK WHERE Visit_ID IN(SELECT Visit_ID FROM VISIT WHERE Appointment_ID IN(SELECT Appointment_ID FROM APPOINTMENT WHERE DOCTOR_ID IN(SELECT Staff_ID FROM DOCTOR WHERE Department_number = "+ dnumber + ")))";
            int avg = 0;
            object result = FunctionExecuteScalar(Q2);
            if (result != null && result != DBNull.Value)
            {
                avg = Convert.ToInt32(result);
            }
            return avg;
        }
        public DataTable getDeptFeedback(string id)
        {
            string Q = "SELECT * FROM FEEDBACK WHERE Visit_ID IN (SELECT Visit_ID FROM VISIT WHERE Appointment_ID IN (SELECT Appointment_ID FROM APPOINTMENT WHERE DOCTOR_ID= '" + id + "'))";
            DataTable dt1 = (DataTable)FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Doctor Name");
            res.Columns.Add("Rate");
            res.Columns.Add("Comment");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q1 = "SELECT Appointment_ID FROM VISIT WHERE Visit_ID = '" + dt1.Rows[i][0] + "' ";
                string apptid = (string)FunctionExecuteScalar(Q1);
                string Q2 = "SELECT DOCTOR_ID FROM APPOINTMENT WHERE Appointment_ID= '" + apptid + "'";
                string did = (string)FunctionExecuteScalar(Q2);
                string Q3 = "SELECT First_Name FROM DOCTOR WHERE Staff_ID = '" + did + "'";
                string Q4 = "SELECT Last_Name FROM DOCTOR WHERE Staff_ID = '" + did + "'";
                string dname = (string)FunctionExecuteScalar(Q3) + " " + (string)FunctionExecuteScalar(Q4);
                res.Rows.Add(dname, dt1.Rows[i][1], dt1.Rows[i][2]);
            }
            return res;

        }
        public DataTable getVisitsDoc(string id)
        {
            string Q = "SELECT * FROM VISIT WHERE Appointment_ID IN(SELECT Appointment_ID FROM VISIT WHERE Appointment_ID IN( SELECT Appointment_ID FROM APPOINTMENT WHERE DOCTOR_ID = '" + id + "'))";
            DataTable dt1 = (DataTable)FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Patient Name");
            res.Columns.Add("Diagnosis");
            res.Columns.Add("Date",typeof(DateTime));
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q1 = "SELECT Appointment_ID FROM VISIT WHERE Visit_ID = '" + dt1.Rows[i][0] + "'";
                string apptid = (string)FunctionExecuteScalar(Q1);
                string Q2 = "SELECT PATIENT_ID FROM APPOINTMENT WHERE Appointment_ID= '" + apptid + "'";
                string pid = (string)FunctionExecuteScalar(Q2);
                string Q3 = "SELECT First_Name FROM PATIENT WHERE Patient_ID = '" + pid + "'";
                string Q4 = "SELECT Last_Name FROM PATIENT WHERE Patient_ID = '" + pid + "'";
                string pname = (string)FunctionExecuteScalar(Q3) + " " + (string)FunctionExecuteScalar(Q4);

                res.Rows.Add(pname, dt1.Rows[i][2], dt1.Rows[i][1]);
            }
            return res;
        }
        public DataTable getApptsDoc(string id)
        {
            string Q = "SELECT * FROM APPOINTMENT WHERE Appointment_ID NOT IN(SELECT Appointment_ID FROM VISIT) AND DOCTOR_ID = '" + id + "'";
            DataTable dt1 = (DataTable)FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Patient Name");
            res.Columns.Add("Type");
            res.Columns.Add("Date", typeof(DateTime));
            res.Columns.Add("Time");
            res.Columns.Add("Notes");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q2 = "SELECT PATIENT_ID FROM APPOINTMENT WHERE Appointment_ID= '" + dt1.Rows[i][0] + "'";
                string pid = (string)FunctionExecuteScalar(Q2);
                string Q3 = "SELECT First_Name FROM PATIENT WHERE Patient_ID = '" + pid + "'";
                string Q4 = "SELECT Last_Name FROM PATIENT WHERE Patient_ID = '" + pid + "'";
                string pname = (string)FunctionExecuteScalar(Q3) + " " + (string)FunctionExecuteScalar(Q4);

                res.Rows.Add(pname, dt1.Rows[i][3], dt1.Rows[i][2], dt1.Rows[i][1], dt1.Rows[i][4]);
            }
            return res;
        }
        public int getAverageRating(string id)
        {
            string Q = "SELECT AVG(Rating) FROM FEEDBACK AS F WHERE Visit_ID IN(SELECT Visit_ID FROM VISIT AS V WHERE Appointment_ID IN( SELECT Appointment_ID FROM APPOINTMENT AS A WHERE A.Appointment_ID = V.Appointment_ID AND A.DOCTOR_ID = '" + id + "'))";
            int avg = 0;
            object result = FunctionExecuteScalar(Q);
            if (result != null && result != DBNull.Value)
            {
                avg = Convert.ToInt32(result);
            }
            return avg;
        }
        public int getFeedbackCount(string id)
        {
            string Q = "SELECT COUNT(*) FROM FEEDBACK AS F WHERE Visit_ID IN(SELECT Visit_ID FROM VISIT AS V WHERE Appointment_ID IN( SELECT Appointment_ID FROM APPOINTMENT AS A WHERE A.Appointment_ID = V.Appointment_ID AND A.DOCTOR_ID = '" + id + "'))";
            return (int)FunctionExecuteScalar(Q);
        }
        public int getPatientsTreatedById(string id)
        {

            string Q = "SELECT COUNT(PATIENT_ID) FROM APPOINTMENT AS A WHERE Appointment_ID IN( SELECT Appointment_ID FROM VISIT AS V WHERE A.Appointment_ID = V.Appointment_ID) AND DOCTOR_ID = '" + id + "'";
            return (int)FunctionExecuteScalar(Q);
        }

        public Tuple<string[], int[]> ViewVisitsDoctor(string sid)
        {
            string Q1 = "SELECT MONTH(Visit_DATE) AS [Month], COUNT(*) AS [Number of Visits] FROM Visit WHERE Visit_ID IN (SELECT Visit_ID FROM VISIT WHERE Appointment_ID IN (SELECT Appointment_ID FROM APPOINTMENT WHERE DOCTOR_ID IN (SELECT Staff_ID FROM DOCTOR WHERE Staff_ID = '" + sid + "'))) GROUP BY MONTH(Visit_DATE)";
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)FunctionExecuteReader(Q1);

            string[] labels = new string[dt1.Rows.Count];
            int[] data = new int[dt1.Rows.Count];
            int j = 0;
            // Iterate over the rows and populate the arrays
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                labels[i] = dt1.Rows[i][j].ToString();
                data[i] = Convert.ToInt32(dt1.Rows[i][j+1]);
            }
            return Tuple.Create(labels, data);
        }
        public Tuple<string[], int[]> ViewVisitsAdmin()
        {
            string Q1 = "SELECT MONTH(Visit_DATE) AS [Month], COUNT(*) AS [Number of Visits] FROM Visit GROUP BY MONTH(Visit_DATE)";
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)FunctionExecuteReader(Q1);

            string[] labels = new string[dt1.Rows.Count];
            int[] data = new int[dt1.Rows.Count];
            int j = 0;
            // Iterate over the rows and populate the arrays
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                labels[i] = dt1.Rows[i][j].ToString();
                data[i] = Convert.ToInt32(dt1.Rows[i][j + 1]);
            }
            return Tuple.Create(labels, data);
        }
       
        public DataTable getNurseSchedule(string sid)
        {
            string Q = "SELECT * FROM NURSE_SERVE_AT WHERE Nurse_ID = '" + sid + "'";
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)FunctionExecuteReader(Q);
            DataTable res = new DataTable();
            res.Columns.Add("Day");
            res.Columns.Add("Room ID");
            res.Columns.Add("start");
            res.Columns.Add("end");

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                res.Rows.Add( dt1.Rows[i][2], dt1.Rows[i][0], dt1.Rows[i][3] , dt1.Rows[i][4]);
            }
            return res;
        }
        public void MarkTestDone(string testid, string res, string sid, string date)
        {
            string Q1 = "SELECT FromVisit FROM TEST WHERE Test_ID = '" + testid + "'";
            string Q2 = "SELECT Visit_ID FROM TEST WHERE Test_ID = '" + testid + "'";
            string vid;
            string Q3;
            if ((int)FunctionExecuteScalar(Q1) ==1)
            {
                vid = (string)FunctionExecuteScalar(Q2);
                Q3 = "INSERT INTO TEST_DONE_BY_TECH (Test_ID, Tech_ID, Visit_ID, DateConducted, Results) VALUES ('" + testid + "', '" + sid + "', '" + vid + "', '" + date + "', '" + res + "');";
            }
            else
            {
                vid = "";
                //Q2 = $"INSERT INTO TEST_DONE_BY_TECH (Test_ID, Tech_ID, DateConducted, Results) VALUES ('{testid}', '{sid}', '{date}', '{res}');";
                Q3 = "INSERT INTO TEST_DONE_BY_TECH (Test_ID, Tech_ID, Visit_ID, DateConducted, Results) VALUES ('" + testid + "', '" + sid + "', '" + vid + "', '" + date + "', '" + res + "');";

            }
            FunctionExecuteNonQuery(Q3);
        }
        
        public void AddTestOut(string npname,  string ntname, string date)
        {
            string Q1 = "SELECT MAX(CAST(SUBSTRING(test_id, 2, LEN(test_id) - 1) AS INTEGER)) AS max_test_id FROM test WHERE test_id LIKE 'T%'";
            int maxtestid = (int)FunctionExecuteScalar(Q1);
            string Q2 = $"INSERT INTO TEST (Test_ID, Date_of_test, Type, pname, FromVisit) VALUES ('T{maxtestid+1}','{date}','{ntname}','{npname}', 0)";
            FunctionExecuteNonQuery(Q2);
        }
        public DataTable getTestReq()   
        {
            DataTable res = new DataTable();
            res.Columns.Add("Patient Name");
            res.Columns.Add("Test ID");
            res.Columns.Add("Test Name");
            res.Columns.Add("Date Requested", typeof(DateTime));

            string Q1 = "SELECT Visit_ID, Type, Date_of_test, Test_ID, pname, FromVisit FROM TEST WHERE Test_ID not in (SELECT Test_ID FROM TEST_DONE_BY_TECH )";
            DataTable dt1 = (DataTable)FunctionExecuteReader(Q1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if ((int)dt1.Rows[i][5] == 1)
                {
                    string Q2 = "SELECT Appointment_ID FROM Visit WHERE Visit_ID = '" + dt1.Rows[i][0] + "'";
                    string apptid = (string)FunctionExecuteScalar(Q2);
                    string Q3 = "SELECT PATIENT_ID FROM APPOINTMENT WHERE Appointment_ID = '" + apptid + "'";
                    string pid = (string)FunctionExecuteScalar(Q3);
                    string Q4 = "SELECT First_Name FROM PATIENT WHERE Patient_ID = '" + pid + "'";
                    string pname = (string)FunctionExecuteScalar(Q4);
                    res.Rows.Add(pname, dt1.Rows[i][3], dt1.Rows[i][1], dt1.Rows[i][2]);
                }
                else
                {
                    res.Rows.Add(dt1.Rows[i][4], dt1.Rows[i][3], dt1.Rows[i][1], dt1.Rows[i][2]);

                }
            }
            return res;
        }
        //(Test_ID, Tech_ID, Visit_ID, DateConducted, Results)
        public void getTestDone(string testid, string labtechid, string vid, string datecon, string results)
        {
            string Q = "INSERT INTO TEST_DONE_BY_TECH (Test_ID, Tech_ID, Visit_ID, DateConducted, Results) VALUES ("+ testid +","+ labtechid+ "," + vid + "," + datecon + "," + results+ ")";
            FunctionExecuteNonQuery(Q);
        }
        public DataTable getTestDoneById(string id)
        {
            DataTable res = new DataTable();
            res.Columns.Add("Patient Name");
            res.Columns.Add("Test Name");
            res.Columns.Add("Date Requested", typeof(DateTime));
            res.Columns.Add("Date Conducted", typeof(DateTime));
            res.Columns.Add("Results");
            string Q1 = "SELECT Visit_ID, Test_ID, DateConducted, Results FROM TEST_DONE_BY_TECH WHERE Tech_ID = '" + id + "'";
            
            DataTable dt1 = (DataTable)FunctionExecuteReader(Q1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Q8 = "SELECT FromVisit FROM TEST WHERE Test_ID ='" + dt1.Rows[i][1] + "' ";
                string pname;
                int fromVisit = (int)FunctionExecuteScalar(Q8);
                if (fromVisit == 1)
                {
                    string Q2 = "SELECT Appointment_ID FROM Visit WHERE Visit_ID = '" + dt1.Rows[i][0] + "'";
                    string apptid = (string)FunctionExecuteScalar(Q2);
                    string Q3 = "SELECT PATIENT_ID FROM APPOINTMENT WHERE Appointment_ID = '" + apptid + "'";
                    string pid = (string)FunctionExecuteScalar(Q3);
                    string Q4 = "SELECT First_Name FROM PATIENT WHERE Patient_ID = '" + pid + "'";
                    pname = (string)FunctionExecuteScalar(Q4);
                }
                else
                {
                    string Q5 = $"SELECT pname FROM TEST WHERE Test_ID =  '{dt1.Rows[i][1]}'";
                    pname = (string)FunctionExecuteScalar(Q5);
                }
                string Q6 = "SELECT Type FROM TEST WHERE Test_ID = '" + dt1.Rows[i][1] + "'";
                string type = (string)FunctionExecuteScalar(Q6);
                string Q7 = "SELECT Date_of_test FROM TEST WHERE Test_ID = '" + dt1.Rows[i][1] + "'";
                DateTime date = (DateTime)FunctionExecuteScalar(Q7);
                res.Rows.Add(pname, type, date, dt1.Rows[i][2], dt1.Rows[i][3]);
            }
            return res;
        }

        public int getTestsNo(string id)
        {
            string Q = "SELECT COUNT(*) FROM TEST_DONE_BY_TECH WHERE Tech_ID = '" + id + "'";
            return (int)FunctionExecuteScalar(Q);
        }
        public string getStaffFnameByID(string id)
        {
            string Q = "SELECT First_Name FROM " + GetUserType(id) + " WHERE Staff_ID = '" + id + "'";
            string res = (string)FunctionExecuteScalar(Q);  
            return res;

        }
        public void addPatient(string fname, string lname, int age, string govern, string city, string street, string nid, string email, string pass, string pid, string g, string number)
        {
            string Q = "INSERT INTO PATIENT VALUES ('" + fname + "','" + lname + "','" + pid + "','" + nid + "'," + age + ", '" + g + "','" + street + "', '" + city + "', '" + govern + "', '" + number + "', '" + pass + "','" + email + "')";
            FunctionExecuteNonQuery(Q);
        }
        


        public DataTable GetUserAdmin(string id,string profession)
        {
            List<string> ID = JsonConvert.DeserializeObject<List<string>>(id);
            Dictionary<string, List<object>> keyValuePairsProfession = new Dictionary<string, List<object>>()
                {
                    { "doctor", new List<object> { "Staff_ID" } },
                    { "pharmacist", new List<object> { "Staff_ID" } },
                    { "labtechnician", new List<object> { "Staff_ID" } },
                    { "nurse", new List<object> { "Staff_ID" } },
                    { "patient", new List<object> { "Patient_ID" } },
                    { "department", new List<object> { "Department_number" } },
                    { "nurse_serve_at", new List<object> { "Nurse_ID", "working_Day" } },
                    { "doctor_work_at", new List<object> { "Doctor_ID", "working_Day" } },
                };
            String Q = $"Select * from {profession.ToUpper()} where ";

            for(int i = 0;i< keyValuePairsProfession[profession.ToLower()].Count; i++)
            {
                int result;
                if (int.TryParse(ID[i], out result)) {
                    Q += $"{keyValuePairsProfession[profession.ToLower()][i]} = {ID[i]}";
                }
                else {
                    Q += $"{keyValuePairsProfession[profession.ToLower()][i]} = '{ID[i]}'";
                }
                if(i!= keyValuePairsProfession[profession.ToLower()].Count - 1)
                {
                    Q += " and ";
                }
            }


               return FunctionExecuteReader(Q);

        }

        public void RemoveUserAdmin(List<string> ID, string profession)
        {
            Dictionary<string, List<object>> keyValuePairsProfession = new Dictionary<string, List<object>>()
                {
                    { "doctor", new List<object> { "Staff_ID" } },
                    { "pharmacist", new List<object> { "Staff_ID" } },
                    { "labtechnician", new List<object> { "Staff_ID" } },
                    { "nurse", new List<object> { "Staff_ID" } },
                    { "patient", new List<object> { "Patient_ID" } },
                    { "department", new List<object> { "Department_number" } },
                    { "nurse_serve_at", new List<object> { "Nurse_ID", "working_Day" } },
                };
            string Q = $"DELETE FROM {profession} WHERE ";
            for (int i = 0; i < keyValuePairsProfession[profession.ToLower()].Count; i++)
            {
                int result;
                if (int.TryParse(ID[i], out result))
                {
                    Q += $"{keyValuePairsProfession[profession.ToLower()][i]} = {ID[i]}";
                }
                else
                {
                    Q += $"{keyValuePairsProfession[profession.ToLower()][i]} = '{ID[i]}'";
                }
                if (i != keyValuePairsProfession[profession.ToLower()].Count - 1)
                {
                    Q += " and ";
                }
            }
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(Q, con);
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally { con.Close(); }

        }
        private string conCat(List<object> list)
        {
            String s = "";
            for(int i = 0;i<list.Count;i++)
            {
                if (list[i].GetType()==typeof(int)) {
                    s += list[i];
                }
                else
                {
                    s += $"'{list[i]}'";
                }
                if (i != list.Count - 1)
                {
                    s += ",";
                }
            }
            return s;
        }
        public void addMedicalStaff(string profession, List<object> list)
        {
            string Q = $"INSERT INTO {profession.ToUpper()} VALUES ({conCat(list)})";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(Q, con);
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally { con.Close(); }

        }
        public DataTable GetTableAdmin(string profession)
        {
            DataTable dt = new DataTable();
            string q = $"Select * from {profession.ToUpper()}";
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand(q, con);
                dt.Load(comm.ExecuteReader());
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally { con.Close(); }
            return dt;
        }
        public bool CheckPasswordPatientE(string userEmail, string enPassword)
        {
            string pass = "";
            string Q = "SELECT Password FROM PATIENT WHERE PATIENT_Email = '" + userEmail + "'"; ;
            pass = (string)FunctionExecuteScalar(Q);
            if (pass == (string)enPassword) { return true; }
            else { return false; }
        }

        private object FunctionExecuteScalar(string Q)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(Q, con);
                object result = command.ExecuteScalar();
                con.Close();
                return result;
            }
            catch (Exception ex)
            {
                con.Close();
                return ex;
            }
        }
        private void FunctionExecuteNonQuery(string Q)
        {
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(Q, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }
        private DataTable FunctionExecuteReader(string Q)
        {
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(Q, con);
                dt.Load(command.ExecuteReader());
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            finally { con.Close() ; }
            return dt;
        }

    }
}
