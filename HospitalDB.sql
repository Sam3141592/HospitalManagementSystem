	--USE master
	--GO\\
	CREATE DATABASE HospitalDB
	USE HospitalDB 
	GO

	CREATE TABLE DEPARTMENT (
		Name VARCHAR(50) UNIQUE NOT NULL,
		Department_number INT DEFAULT 1 NOT NULL,
		PRIMARY Key(Department_number)
	)
	CREATE TABLE ADMIN (
		First_name VARCHAR(20) NOT NULL,
		Last_name VARCHAR(20) NOT NULL,
		Staff_ID VARCHAR(9) DEFAULT '0000' NOT NULL,
		Password VARCHAR(30),
		Staff_Email VARCHAR(50),
		PRIMARY KEY (Staff_Email)
	);

	CREATE TABLE DOCTOR (
		First_name VARCHAR(20) NOT NULL,
		Last_name VARCHAR(20) NOT NULL,
		Staff_ID VARCHAR(9) DEFAULT '0000' NOT NULL, 
		National_ID VARCHAR(14) NOT NULL,
		Department_number INT NULL,
		Age INT, 
		Gender char(1),
		Street VARCHAR(50), 
		City VARCHAR(15), 
		Governorate VARCHAR(15), 
		Phone_number VARCHAR(11),
		PRIMARY KEY (Staff_ID),
		FOREIGN KEY (Department_number) REFERENCES DEPARTMENT,
		Password VARCHAR(30) NOT NULL,
		Staff_Email VARCHAR(50) UNIQUE
		)
	CREATE TABLE ANNOUNCEMENT (
		annc_ID VARCHAR(9),
		Manager_ID VARCHAR(9),
		content VARCHAR(MAX),
		annc_date DATE,
		PRIMARY KEY (annc_ID),
		FOREIGN KEY (Manager_ID) REFERENCES DOCTOR
			ON DELETE CASCADE
			ON UPDATE CASCADE
	);
	CREATE TABLE LabTechnician (
	  First_name VARCHAR(255) NOT NULL,
	  Last_name VARCHAR(255) NOT NULL,
	  Staff_ID VARCHAR(20) DEFAULT '0000'  NOT NULL,
	  National_ID VARCHAR(255) NOT NULL,
	  Age INT,
	  Gender CHAR(1),
	  Phone_number VARCHAR(20),
	  Street VARCHAR(255),
	  City VARCHAR(255),
	  Governorate VARCHAR(255),
	  PRIMARY KEY (Staff_ID),
	  Password VARCHAR(30) NOT NULL,
		Staff_Email VARCHAR(50) UNIQUE
	)

	CREATE TABLE NURSE (
	  First_name VARCHAR(255) NOT NULL,
	  Last_name VARCHAR(255) NOT NULL,
	  Staff_ID VARCHAR(255) DEFAULT '0000' PRIMARY KEY NOT NULL,
	  National_ID VARCHAR(255) NOT NULL,
	  Department_number INT NULL,
	  Age INT,
	  Gender CHAR(1),
	  Phone_number VARCHAR(20),
	  Street VARCHAR(255),
	  City VARCHAR(255),
	  Governorate VARCHAR(255),
	  Password VARCHAR(30) NOT NULL,
		Staff_Email VARCHAR(50) UNIQUE

	)

	CREATE TABLE PATIENT (
		First_name VARCHAR(20) NOT NULL,
		Last_name VARCHAR(20) NOT NULL,
		Patient_ID VARCHAR(9) DEFAULT '0000' NOT NULL,
		National_ID VARCHAR(14) NOT NULL,
		Age INT, 
		Gender char(1),
		Street VARCHAR(50), 
		City VARCHAR(15), 
		Governorate VARCHAR(15), 
		Phone_number VARCHAR(11),
		PRIMARY KEY(Patient_ID),
		Password VARCHAR(30) NOT NULL,
		PATIENT_Email VARCHAR(50) UNIQUE
	)
	CREATE TABLE Pharmacist (
	  First_name VARCHAR(255) NOT NULL,
	  Last_name VARCHAR(255) NOT NULL,
	  Staff_ID VARCHAR(255) DEFAULT '0000'  NOT NULL,
	  National_ID VARCHAR(255),
	  Age INT,
	  Gender CHAR(1),
	  Phone_number VARCHAR(20),
	  Street VARCHAR(255),
	  City VARCHAR(255),
	  Governorate VARCHAR(255)
	  PRIMARY KEY (Staff_ID),
	  Password VARCHAR(30) NOT NULL,
		Staff_Email VARCHAR(50) UNIQUE
	);
	CREATE TABLE Medicine (
	  Medicine_Name VARCHAR(255) DEFAULT '0000' PRIMARY KEY,
	  Amount_Available INT DEFAULT 0 NOT NULL,
	);


	CREATE TABLE Room (
	  Room_ID VARCHAR(255) DEFAULT '0000',
	  Room_Type VARCHAR(255),
	  Dno INT,
	  PRIMARY KEY (Room_ID),
	  FOREIGN KEY (Dno) REFERENCES DEPARTMENT
		ON DELETE SET DEFAULT 
		ON UPDATE CASCADE
	);
		CREATE TABLE APPOINTMENT (
		Appointment_ID VARCHAR(10) DEFAULT '0000',
		Time_of_Appointment TIME,
		Date_of_Appointment DATE,
		Type VARCHAR(20),
		Special_notes VARCHAR(max),
		PRIMARY KEY (Appointment_ID),
		DOCTOR_ID VARCHAR(9) FOREIGN KEY REFERENCES DOCTOR ON DELETE SET DEFAULT ON UPDATE CASCADE,
		PATIENT_ID VARCHAR(9) FOREIGN KEY REFERENCES PATIENT ON DELETE CASCADE ON UPDATE CASCADE,
		Room_ID VARCHAR(255) DEFAULT 'R000'FOREIGN KEY REFERENCES ROOM ON DELETE SET DEFAULT ON UPDATE CASCADE

);
	CREATE TABLE Visit (
	  Visit_ID VARCHAR(20) DEFAULT '0000',
	  Visit_DATE DATE,
	  Diagnosis VARCHAR(max),
	  Appointment_ID VARCHAR(10) UNIQUE
	  FOREIGN KEY REFERENCES Appointment
			ON DELETE CASCADE
			ON UPDATE CASCADE,
	  PRIMARY KEY (VISIT_ID) 
	);

	CREATE TABLE FEEDBACK(
		Visit_ID VARCHAR(20) PRIMARY KEY,
		Rating INT,
		Comments VARCHAR(max),
		FOREIGN KEY (Visit_ID) REFERENCES Visit
			ON DELETE CASCADE
			ON UPDATE CASCADE,
	)

	CREATE TABLE TEST(
		Test_ID VARCHAR(20) DEFAULT '0000',
		Date_of_test DATE,
		Type VARCHAR(20),
		Visit_ID VARCHAR(20) NULL,
		pname VARCHAR(30) NULL,
		FromVisit INT NOT NULL, --1 IF TEST IS FROM VISIT AND 0 IF TEST IS NOT FROM A VISIT
		PRIMARY KEY (Test_ID),
		FOREIGN KEY (Visit_ID) REFERENCES Visit
			ON DELETE CASCADE 
			ON UPDATE CASCADE
	)

	CREATE TABLE DOCTOR_MANAGES_DEPARTMENT(
		Doctor_ID VARCHAR(9) DEFAULT '0000',
		Department_number INT DEFAULT 1,
		Start_date date,
		PRIMARY KEY (Doctor_ID, Department_number),
		FOREIGN KEY (Doctor_ID ) REFERENCES DOCTOR
			ON DELETE SET DEFAULT 
			ON UPDATE CASCADE,
		FOREIGN KEY (Department_number) REFERENCES DEPARTMENT
			ON DELETE CASCADE
			ON UPDATE CASCADE
	)










	CREATE TABLE TEST_DONE_BY_TECH(
	  Test_ID VARCHAR(20),
	  Tech_ID VARCHAR(20) DEFAULT '0000',
	  Visit_ID VARCHAR(20),
	  DateConducted DATE,
	  Results VARCHAR(max) NOT NULL,
	  PRIMARY KEY (Test_ID),
	  FOREIGN KEY (Test_ID) REFERENCES TEST
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	  FOREIGN KEY (Tech_ID) REFERENCES LabTechnician
		ON DELETE SET DEFAULT
		ON UPDATE CASCADE,
	);

	CREATE TABLE PHARMACIST_SELL_MEDICINE(
	  Pharmacist_ID VARCHAR(255),
	  Medicine_Name VARCHAR(255),
	  PRIMARY KEY (Pharmacist_ID, Medicine_Name),
	  FOREIGN KEY (Medicine_Name) REFERENCES Medicine
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	  FOREIGN KEY (Pharmacist_ID) REFERENCES Pharmacist
	  ON DELETE CASCADE
		ON UPDATE CASCADE,
	);

	--UPDATE Pharmacist SET First_Name='Alice',Last_Name='J',Staff_ID='PH003',National_ID='JKL012',Department_Number=28,Age='F',Gender='555-1112',Street='321 Elm St',City='Anothercity',Governorate='FL',Phone_Number='ph1',password='ph3@gmial.com' where Staff_ID = 'PH003'


	Select * from LabTechnician

	CREATE TABLE DOCTOR_WORK_AT (
	Room_ID VARCHAR(255) DEFAULT 1,
	Doctor_ID VARCHAR(9),
	working_Day varchar(10),
	start_Hour time, 
	end_Hour time,
	PRIMARY KEY(Doctor_ID, working_Day),
	FOREIGN KEY (Doctor_ID) REFERENCES DOCTOR
		ON DELETE CASCADE 
		ON UPDATE CASCADE,
	FOREIGN KEY (Room_ID) REFERENCES ROOM
		ON DELETE SET DEFAULT 
		ON UPDATE CASCADE
	);
		

		
	

	Select * FROM Room

	CREATE TABLE NURSE_SERVE_AT(
	Room_ID VARCHAR(255) DEFAULT '1',
	Nurse_ID VARCHAR(255),
	working_Day varchar(10),
	start_Hour time, 
	end_Hour time,
	PRIMARY KEY(Room_ID, working_Day),
	FOREIGN KEY (Nurse_ID) REFERENCES NURSE
		ON DELETE CASCADE 
		ON UPDATE CASCADE,
	FOREIGN KEY (Room_ID) REFERENCES ROOM
		ON DELETE SET DEFAULT 
		ON UPDATE CASCADE
	);

	 

	CREATE TABLE VISIT_PRESCRIBE_MEDICINE (
	Medicine_Name varchar(255),
	Visit_ID VARCHAR(20),
	Amount int,
	PRIMARY KEY(Medicine_Name, Visit_ID),
	FOREIGN KEY (Medicine_Name) REFERENCES Medicine
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	FOREIGN KEY (Visit_ID) REFERENCES Visit
		ON DELETE CASCADE 
		ON UPDATE CASCADE
	);

INSERT INTO ADMIN (First_name, Last_name, Staff_ID, Password, Staff_Email)
VALUES
	('John', 'Doe', 'A6', 'a1', 'john.doe@example.com'),
    ('Jane', 'Smith', 'A2', 'adminpass', 'jane.smith@example.com'),
    ('Mike', 'Johnson', 'A3', 'secretpass', 'mike.johnson@example.com'),
    ('Sarah', 'Lee', 'A4', 'admin123', 'sarah.lee@example.com'),
    ('David', 'Nguyen', 'A5', 'passw0rd', 'david.nguyen@example.com');

	select * from ADMIN
	INSERT INTO DEPARTMENT (Name, Department_number, Speciality)
	VALUES
		('Emergency Medicine', 1, 'Trauma'),
		('Cardiology', 2, 'Heart Disease'),
		('Neurology', 3, 'Brain and Nervous System'),
		('Oncology', 4, 'Cancer Treatment')

	select * from DEPARTMENT


	INSERT INTO DOCTOR VALUES
	('John', 'Doe', 'D0001', '12345678901234', 1, 35, 'M', '123 Main St', 'Anytown', 'Anygov', '01234567890', 'd1','d1@gmail.com'),
	('Jane', 'Smith', 'D0002', '23456789012345', 2, 28, 'F', '456 Oak Ave', 'Anycity', 'Anygov', '12345678901', 'd1','d2@gmail.com'),
	('David', 'Lee', 'D0003', '34567890123456', 3 , 42, 'M', '789 Elm St', 'Anystate', 'Anygov', '23456789012', 'd1','d3@gmail.com'),
	('Sara', 'Johnson', 'D0004', '45678901234567', 1, 31, 'F', '321 Pine St', 'Anytown', 'Anygov', '34567890123', 'd1','d4@gmail.com'),
	('Mike', 'Williams', 'D0005', '56789012345678', 2, 47, 'M', '654 Birch Rd', 'Anycity', 'Anygov', '45678901234', 'd1','d5@gmail.com');

	select * from DOCTOR

	INSERT INTO ANNOUNCEMENT (annc_ID, Manager_ID, content, annc_date)
		VALUES
    ('A1', 'D0001', 'Please be informed that there will be a staff meeting next Monday at 10 am in the conference room.', '2023-05-16'),
    ('A2', 'D0002', 'The clinic will be closed on May 20th due to public holiday. Please make sure to reschedule your appointment if necessary.', '2023-05-18'),
    ('A3', 'D0003', 'We are pleased to announce that our clinic has achieved the ISO 9001 certification for quality management system.', '2023-05-15'),
    ('A4', 'D0001', 'Reminder: All staff are required to attend the annual training session on infection control next week.', '2023-05-20'),
    ('A5', 'D0002', 'We have a new doctor joining our team. Dr. John Doe will be available for consultation starting next Monday. Please help us welcome him!', '2023-05-15');

	SELECT * FROM ANNOUNCEMENT
	INSERT INTO LabTechnician
	VALUES 
	('John', 'Doe', 'LT0001', '123456789', 32, 'M', '555-1234', '123 Main St', 'New York', 'NY', 'l1','l1@gmail.com'),
	('Jane', 'Doe', 'LT0002', '987654321', 29, 'F', '555-5678', '456 1st Ave', 'San Francisco', 'CA', 'l1','l2@gmail.com'),
	('Bob', 'Smith', 'LT0003', '456789012', 45, 'M', '555-9876', '789 Elm St', 'Seattle', 'WA', 'l1','l3@gmail.com'),
	('Alice', 'Johnson', 'LT0004', '789012345', 27, 'F', '555-4321', '321 Oak St', 'Chicago', 'IL', 'l1','l4@gmail.com'),
	('Sam', 'Wilson', 'LT0005', '345678901', 35, 'M', '555-1111', '567 Pine St', 'Boston', 'MA', 'l1','l5@gmail.com');

	select * from LabTechnician


	INSERT INTO NURSE 
	VALUES 
	('Sarah', 'Smith', 'N0001', '12345678901234', 1, 30, 'F', '01234567890', '123 Main St', 'Los Angeles', 'California', 'n1','n1@gmail.com'),
	('John', 'Doe', 'N0002', '23456789012345', 2, 35, 'M', '02345678901', '456 Oak Ave', 'New York', 'New York', 'n1','n2@gmail.com'),
	('Emily', 'Jones', 'N0003', '34567890123456', 1, 28, 'F', '03456789012', '789 Pine Blvd', 'Houston', 'Texas', 'n1','n3@gmail.com'),
	('David', 'Nguyen', 'N0004', '45678901234567', 3, 32, 'M', '04567890123', '321 Elm St', 'Chicago', 'Illinois', 'n1','n4@gmail.com'),
	('Lisa', 'Kim', 'N0005', '56789012345678', 2, 27, 'F', '05678901234', '567 Maple Ave', 'San Francisco', 'California', 'n1','n5@gmail.com');

	select * from NURSE


	INSERT INTO PATIENT VALUES 
	('John', 'Doe', 'P0001', '12345678901234', 30, 'M', '123 Main St', 'Anytown', 'Any State', '555-1234', 'p1', 'p1@gmail.com'),
	('Jane', 'Smith', 'P0002', '23456789012345', 25, 'F', '456 Elm St', 'Othertown', 'Another State', '555-2345', 'p1', 'p2@gmail.com'),
	('Bob', 'Johnson', 'P0003', '34567890123456', 45, 'M', '789 Oak St', 'Somewhere', 'Some State', '555-3456', 'p1', 'p3@gmail.com'),
	('Sarah', 'Williams', 'P0004', '45678901234567', 55, 'F', '321 Maple St', 'Nowhere', 'No State', '555-4567', 'p1', 'p4@gmail.com'),
	('Mike', 'Brown', 'P0005', '56789012345678', 20, 'M', '654 Birch St', 'Heretown', 'Here State', '555-5678', 'p1', 'p5@gmail.com');


	select * from PATIENT

	INSERT INTO Room (Room_ID, Room_Type, Dno)
	VALUES ('R001', 'Cardiology', 1),
			('R002', 'Cardiology', 2),
			('R003', 'Oncology', 3),
			('R004', 'Neurology', 4),
			('R005', 'Neurology', 4);

INSERT INTO APPOINTMENT (Appointment_ID, Time_of_Appointment, Date_of_Appointment, Type, Special_notes, DOCTOR_ID, PATIENT_ID, Room_ID) VALUES 
('APPT001', '10:00:00', '2023-06-15', 'Check-up', 'Patient has a history of high blood pressure.', 'D0001', 'P0005', 'R001'),
('APPT002', '14:00:00', '2023-05-01', 'Follow-up', 'Patient needs a prescription refill.', 'D0001', 'P0002', 'R002'),
('APPT003', '11:00:00', '2023-07-10', 'Consultation', 'Patient is allergic to penicillin.', 'D0001', 'P0001', 'R003'),
('APPT004', '13:00:00', '2023-06-29', 'Check-up', NULL, 'D0001', 'P0001', 'R004'),
('APPT005', '09:00:00', '2023-08-12', 'Procedure', 'No Notes', 'D0001', 'P0004', 'R005'),
('APPT006', '11:00:00', '2023-07-12', 'Check-up', 'No Notes', 'D0002', 'P0001', 'R005'),
('APPT007', '18:00:00', '2023-06-12', 'Consultation', 'No Notes', 'D0002', 'P0002', 'R004'),
('APPT008', '21:00:00', '2023-05-12', 'Procedure', 'No Notes', 'D0003', 'P0004', 'R003')

	select * from APPOINTMENT

	INSERT INTO Visit (Visit_ID, Visit_DATE, Diagnosis, Appointment_ID)
	VALUES ('V001', '2023-04-21', 'Common cold', 'APPT001'),
		   ('V002', '2023-04-22', 'Flu', 'APPT002'),
		   ('V003', '2023-04-23', 'Migraine', 'APPT003'),
		   ('V004', '2023-04-24', 'Back pain', 'APPT004'),
		   ('V005', '2023-04-25', 'Flu', 'APPT005'),
		   ('V006', '2023-02-25', 'Common cold', 'APPT006'),
		   ('V007', '2023-01-25', 'Anxiety', 'APPT007'),
		   ('V008', '2023-05-25', 'Back', 'APPT008')


	select * from Visit


	INSERT INTO Medicine (Medicine_Name, Amount_Available) 
	VALUES 
	('Medicine A', 10), 
	('Medicine B', 20), 
	('Medicine C', 5), 
	('Medicine D', 15), 
	('Medicine E', 30);

	select * from Medicine


	INSERT INTO DOCTOR_MANAGES_DEPARTMENT (Doctor_ID, Department_number, Start_date)
	VALUES 
		('D0001', 1, '2022-01-01'),
		('D0002', 2, '2022-02-01'),
		('D0003', 2, '2022-02-01')

	 select * from DOCTOR_MANAGES_DEPARTMENT


	INSERT INTO TEST (Test_ID, Date_of_test, Type, Visit_ID, FromVisit)
	VALUES
	('T1', '2023-04-20', 'PCR', 'V001',1),
	('T2', '2023-04-19', 'Rapid', 'V002',1),
	('T3', '2023-04-18', 'PCR', 'V003',1),
	('T4', '2023-04-17', 'PCR', 'V004',1),
	('T5', '2023-04-16', 'Rapid', 'V005',1),
	('T6', '2023-04-17', 'PCR', 'V004',1),
	('T7', '2023-04-16', 'Rapid', 'V005',1);

	select * from TEST

	INSERT INTO TEST_DONE_BY_TECH (Test_ID, Tech_ID, Visit_ID, DateConducted, Results) VALUES
	('T1', 'LT0001', 'V001','2023-04-19','https://drive.google.com/file/d/1pSLY2J_IkshLE8AcJy_Lia6oMjLIaG8y/view?usp=share_link'),
	('T2', 'LT0002', 'V002','2023-04-18','https://drive.google.com/file/d/1pSLY2J_IkshLE8AcJy_Lia6oMjLIaG8y/view?usp=share_link'),
	('T3', 'LT0001', 'V003','2023-04-17','https://drive.google.com/file/d/1pSLY2J_IkshLE8AcJy_Lia6oMjLIaG8y/view?usp=share_link'),
	('T4', 'LT0002', 'V004','2023-04-25','https://drive.google.com/file/d/1pSLY2J_IkshLE8AcJy_Lia6oMjLIaG8y/view?usp=share_link'),
	('T5', 'LT0003', 'V005','2023-04-27','https://drive.google.com/file/d/1pSLY2J_IkshLE8AcJy_Lia6oMjLIaG8y/view?usp=share_link');



	select * from TEST_DONE_BY_TECH

	INSERT INTO Pharmacist
	VALUES ('Jane', 'Doe', 'PH001', 'DEF456', 25, 'F', '555-5678', '456 Oak St', 'Sometown', 'NY','ph1','ph1@gmial.com'),
		   ('Bob', 'Smith', 'PH002', 'GHI789', 35, 'M', '555-9101', '789 Pine St', 'Othertown', 'TX','ph1','ph2@gmial.com'),
		   ('Alice', 'Johnson', 'PH003', 'JKL012', 28, 'F', '555-1112', '321 Elm St', 'Anothercity', 'FL','ph1','ph3@gmial.com'),
		   ('David', 'Lee', 'PH004', 'MNO345', 32, 'M', '555-1314', '987 Elm St', 'Anothercity', 'FL','ph1','ph4@gmial.com'),
		   ('Susan', 'Brown', 'PH005', 'PQR678', 40, 'F', '555-1516', '123 Maple St', 'Sometown', 'NY','ph1','ph5@gmial.com'),
		   ('Michael', 'Taylor', 'PH006', 'STU901', 45, 'M', '555-1718', '456 Pine St', 'Anytown', 'CA','ph1','ph6@gmial.com');

	select * from Pharmacist

	INSERT INTO PHARMACIST_SELL_MEDICINE (Pharmacist_ID, Medicine_Name)
	VALUES
	('PH002', 'Medicine A'),
	('PH003', 'Medicine B'),
	('PH001', 'Medicine C'),
	('PH004', 'Medicine D'),
	('PH001', 'Medicine A');

	select * from PHARMACIST_SELL_MEDICINE

	select * from PHARMACIST

	

	  

	select * from Room

	INSERT INTO NURSE_SERVE_AT (Room_ID, Nurse_ID, working_Day, start_Hour, end_Hour)
	VALUES 
	('R001', 'N0001', 'Monday', '08:00:00', '12:00:00'),
	('R003', 'N0004', 'Monday', '13:00:00', '17:00:00'),
	('R004', 'N0003', 'Tuesday', '08:00:00', '12:00:00'),
	('R002', 'N0002', 'Tuesday', '13:00:00', '17:00:00'),
	('R003', 'N0001', 'Wednesday', '08:00:00', '12:00:00')
	select * from DOCTOR_WORK_AT

	


	INSERT INTO DOCTOR_WORK_AT (Room_ID,Doctor_ID, working_Day, start_Hour, end_Hour)
VALUES ('R001','D0001', 'Monday', '09:00:00', '17:00:00');

INSERT INTO DOCTOR_WORK_AT (Room_ID,Doctor_ID, working_Day, start_Hour, end_Hour)
VALUES ('R001','D0002', 'Tuesday', '10:00:00', '18:00:00');

INSERT INTO DOCTOR_WORK_AT (Room_ID,Doctor_ID, working_Day, start_Hour, end_Hour)
VALUES ('R001','D0003', 'Wednesday', '08:00:00', '16:00:00');



	INSERT INTO VISIT_PRESCRIBE_MEDICINE (Medicine_Name, Visit_ID, Amount)
	VALUES 
	('Medicine A', 'V001', 2),
	('Medicine C', 'V002', 3),
	('Medicine D', 'V003', 1),
	('Medicine B', 'V004', 4),
	('Medicine A', 'V005', 2);

	select * from VISIT_PRESCRIBE_MEDICINE

	SELECT * FROM ADMIN
	INSERT INTO FEEDBACK (Visit_ID, Rating, Comments) VALUES
('V001', 4, 'Great service, friendly staff.'),
('V002', 3, 'Average experience, could have been better.'),
('V003', 5, 'Absolutely loved it! Will definitely come back.'),
('V004', 2, 'Terrible experience, would not recommend.'),
('V005', 4, 'Good food, nice atmosphere.');

select * from FEEDBACK
SELECT AVG(Rating) FROM FEEDBACK AS F WHERE Visit_ID IN(SELECT Visit_ID FROM VISIT AS V WHERE Appointment_ID IN( SELECT Appointment_ID FROM APPOINTMENT AS A WHERE A.Appointment_ID = V.Appointment_ID AND A.DOCTOR_ID = 'D0001'));

SELECT * FROM VISIT WHERE Appointment_ID IN(SELECT Appointment_ID FROM APPOINTMENT WHERE DOCTOR_ID = 'D0001');

SELECT * FROM ADMIN

SELECT * FROM NURSE_SERVE_AT

select * FROM DOCTOR_WORK_AT

SELECT AVG(Rating) FROM FEEDBACK AS F WHERE Visit_ID IN(SELECT Visit_ID FROM VISIT AS V WHERE Appointment_ID IN
( SELECT Appointment_ID FROM APPOINTMENT AS A WHERE A.Appointment_ID = V.Appointment_ID 
AND A.DOCTOR_ID = 'D0001'))
