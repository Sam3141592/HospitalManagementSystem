## Hospital Management System  
**Course:** CIE 206 – Database Management Systems  
**Framework:** ASP.NET Core  
**Database:** SQL Server  

### Description
The **Hospital Management System** is a web-based application developed using **ASP.NET Core** that enables hospitals to manage their operations, staff, and patients efficiently. It integrates a **SQL Server** database to store and retrieve information related to hospital departments, administrators, doctors, nurses, pharmacists, lab technicians, and patients.

This system provides a structured approach to hospital management by offering role-based authentication, patient record management, and administrative controls.

---

### Features
#### **1. Role-Based System**
- **Administrators** can manage staff and hospital settings.
- **Doctors** can view patient history and update records.
- **Nurses** assist doctors in managing patient care.
- **Pharmacists** handle medication and prescriptions.
- **Lab Technicians** update medical test results.
- **Patients** can register and book appointments.

#### **2. Database Management**
- SQL database stores hospital data in multiple tables.
- Supports CRUD (Create, Read, Update, Delete) operations.
- Predefined **HospitalDB.sql** script initializes database structure.

#### **3. Web Pages**
- **Authentication**: Patient and medical staff login/signup (`msignin.cshtml`, `psignin.cshtml`, `psignup.cshtml`).
- **Admin Dashboard**: Manages hospital operations (`ADMIN/` directory).
- **Doctor Dashboard**: Manages patient data (`DOCTOR/` directory).
- **Pharmacist Dashboard**: Prescription management (`Pharmacist/`).
- **Patient Portal**: Allows viewing medical history and appointments (`Patient_Pages/`).
- **Feedback System**: Allows users to submit reviews (`feedback.cshtml`).

#### **4. Data Visualization**
- Uses **Chart.js** for displaying graphical reports (`ChartJs.cs`, `Dataset.cs`, `Options.cs` in `Models/`).
- Tracks hospital performance metrics such as patient visits and medication distribution.

---

### Database Schema
The database is structured with multiple tables. The **HospitalDB.sql** script defines:
- **DEPARTMENT**: Stores hospital department details.
- **ADMIN**: Stores administrator login information.
- **DOCTOR, NURSE, PHARMACIST, LAB TECHNICIAN**: Staff tables for role-based management.
- **PATIENT**: Stores patient medical records.

---

### Structure
#### **Key Directories & Files**
- **Models/** – Defines data structures (`HosDB.cs`, `ChartJs.cs`).
- **Pages/** – Contains web pages and logic (`ADMIN/`, `DOCTOR/`, `Patient_Pages/`, etc.).
- **wwwroot/** – Static assets (CSS, JS, Images).
- **appsettings.json** – Database connection and configuration.
- **HospitalDB.sql** – SQL script to set up the database.

---

### How to Run the Project
1. **Database Setup**
   - Create a database using **SQL Server**.
   - Run the `HospitalDB.sql` script to create necessary tables.

2. **Build and Run ASP.NET Core App**
   - Open the project in **Visual Studio**.
   - Set **HospitalManagementSystem.csproj** as the startup project.
   - Run the project (`Ctrl + F5`).

3. **Access the Web Application**
   - Navigate to `http://localhost:5000` (or assigned port).
   - Register as an admin, doctor, pharmacist, or patient.
   - Manage hospital operations through the dashboard.
