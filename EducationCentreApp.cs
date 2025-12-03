using System;
using System.Collections.Generic;
using System.Linq;


namespace EducationCentreSystem
{
   
    public class Person
    {
        // Private fields implementing encapsulation principle
        private string _name;
        private string _telephone;
        private string _email;
        private string _role;

        
        public Person()
        {
            _name = "";
            _telephone = "";
            _email = "";
            _role = "";
        }

       
        public Person(string name, string telephone, string email, string role)
        {
            _name = name;
            _telephone = telephone;
            _email = email;
            _role = role;
        }

        
        public string Name
        {
            get { return _name; }
            set { _name = value ?? ""; }
        }

        
        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value ?? ""; }
        }

       
        public string Email
        {
            get { return _email; }
            set { _email = value ?? ""; }
        }

      
        public string Role
        {
            get { return _role; }
            set { _role = value ?? ""; }
        }

   
        public virtual string DisplayInfo()
        {
            return string.Format("Name: {0}, Telephone: {1}, Email: {2}, Role: {3}", Name, Telephone, Email, Role);
        }

  
        public virtual string GetDetailedInfo()
        {
            return DisplayInfo();
        }
    }

    
    public class Teacher : Person
    {
        // Private fields specific to teachers
        private decimal _salary;
        private string _subject1;
        private string _subject2;

        public Teacher() : base()
        {
            _salary = 0.0m;
            _subject1 = "";
            _subject2 = "";
        }

        public Teacher(string name, string telephone, string email, decimal salary, string subject1, string subject2)
            : base(name, telephone, email, "Teacher")
        {
            _salary = salary;
            _subject1 = subject1;
            _subject2 = subject2;
        }

        public decimal Salary
        {
            get { return _salary; }
            set { _salary = value >= 0 ? value : 0; }
        }

        
        public string Subject1
        {
            get { return _subject1; }
            set { _subject1 = value ?? ""; }
        }

        public string Subject2
        {
            get { return _subject2; }
            set { _subject2 = value ?? ""; }
        }

        public override string DisplayInfo()
        {
            return base.DisplayInfo() + string.Format(", Salary: ${0:F2}, Subjects: {1}, {2}", Salary, Subject1, Subject2);
        }

        public override string GetDetailedInfo()
        {
            return "=== Teacher Information ===\n" +
                   "Name: " + Name + "\n" +
                   "Telephone: " + Telephone + "\n" +
                   "Email: " + Email + "\n" +
                   "Role: " + Role + "\n" +
                   "Salary: $" + Salary.ToString("F2") + "\n" +
                   "Subject 1: " + Subject1 + "\n" +
                   "Subject 2: " + Subject2 + "\n";
        }
    }


    public class Admin : Person
    {
        // Private fields specific to administration staff
        private decimal _salary;
        private string _employmentType; // Full-time or Part-time
        private int _workingHours;

     
        public Admin() : base()
        {
            _salary = 0.0m;
            _employmentType = "Full-time";
            _workingHours = 40;
        }

        
        public Admin(string name, string telephone, string email, decimal salary, string employmentType, int workingHours)
            : base(name, telephone, email, "Admin")
        {
            _salary = salary;
            _employmentType = employmentType;
            _workingHours = workingHours;
        }

        public decimal Salary
        {
            get { return _salary; }
            set { _salary = value >= 0 ? value : 0; }
        }

     
        public string EmploymentType
        {
            get { return _employmentType; }
            set 
            { 
                if (value == "Full-time" || value == "Part-time")
                    _employmentType = value;
                else
                    _employmentType = "Full-time";
            }
        }

        public int WorkingHours
        {
            get { return _workingHours; }
            set { _workingHours = value > 0 ? value : 1; }
        }


        public override string DisplayInfo()
        {
            return base.DisplayInfo() + string.Format(", Salary: ${0:F2}, Employment: {1}, Hours: {2}", Salary, EmploymentType, WorkingHours);
        }

   
        public override string GetDetailedInfo()
        {
            return "=== Administration Information ===\n" +
                   "Name: " + Name + "\n" +
                   "Telephone: " + Telephone + "\n" +
                   "Email: " + Email + "\n" +
                   "Role: " + Role + "\n" +
                   "Salary: $" + Salary.ToString("F2") + "\n" +
                   "Employment Type: " + EmploymentType + "\n" +
                   "Working Hours: " + WorkingHours.ToString() + " hours/week\n";
        }
    }


    public class Student : Person
    {
        // Private fields specific to students
        private string _subject1;
        private string _subject2;
        private string _subject3;

    
        public Student() : base()
        {
            _subject1 = "";
            _subject2 = "";
            _subject3 = "";
        }

      
        public Student(string name, string telephone, string email, string subject1, string subject2, string subject3)
            : base(name, telephone, email, "Student")
        {
            _subject1 = subject1;
            _subject2 = subject2;
            _subject3 = subject3;
        }

       
        public string Subject1
        {
            get { return _subject1; }
            set { _subject1 = value ?? ""; }
        }


        public string Subject2
        {
            get { return _subject2; }
            set { _subject2 = value ?? ""; }
        }

    
        public string Subject3
        {
            get { return _subject3; }
            set { _subject3 = value ?? ""; }
        }

        public override string DisplayInfo()
        {
            return base.DisplayInfo() + string.Format(", Subjects: {0}, {1}, {2}", Subject1, Subject2, Subject3);
        }


        public override string GetDetailedInfo()
        {
            return "=== Student Information ===\n" +
                   "Name: " + Name + "\n" +
                   "Telephone: " + Telephone + "\n" +
                   "Email: " + Email + "\n" +
                   "Role: " + Role + "\n" +
                   "Subject 1: " + Subject1 + "\n" +
                   "Subject 2: " + Subject2 + "\n" +
                   "Subject 3: " + Subject3 + "\n";
        }
    }


    public class Program
    {
        // Data structures to store different types of users
        // Using List<T> to handle unknown number of objects as required
        private static List<Teacher> teachers = new List<Teacher>();
        private static List<Admin> admins = new List<Admin>();
        private static List<Student> students = new List<Student>();

      
        static void Main(string[] args)
        {
            Console.WriteLine("=== Education Centre Desktop Information System ===");
            Console.WriteLine("Welcome to the comprehensive user management system!");
            Console.WriteLine("====================================================\n");

            // Initialize system with sample data for demonstration
            InitializeSampleData();

            // Main program loop - continues until user chooses to exit
            bool continueRunning = true;
            while (continueRunning)
            {
                continueRunning = DisplayMainMenu();
            }

            Console.WriteLine("\nThank you for using the Education Centre Information System!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        
        private static void InitializeSampleData()
        {
            // Sample Teachers
            teachers.Add(new Teacher("Dr. John Smith", "555-0101", "j.smith@edu.centre", 75000.00m, "Mathematics", "Physics"));
            teachers.Add(new Teacher("Prof. Sarah Johnson", "555-0102", "s.johnson@edu.centre", 82000.00m, "English", "Literature"));
            
            // Sample Admin Staff
            admins.Add(new Admin("Mike Brown", "555-0201", "m.brown@edu.centre", 45000.00m, "Full-time", 40));
            admins.Add(new Admin("Lisa Davis", "555-0202", "l.davis@edu.centre", 25000.00m, "Part-time", 20));
            
            // Sample Students
            students.Add(new Student("Emma Wilson", "555-0301", "e.wilson@student.edu", "Mathematics", "Physics", "Chemistry"));
            students.Add(new Student("James Miller", "555-0302", "j.miller@student.edu", "English", "History", "Art"));
            
            Console.WriteLine("System initialized with sample data.");
            Console.WriteLine(string.Format("Sample data loaded: {0} teachers, {1} admins, {2} students\n", teachers.Count, admins.Count, students.Count));
        }

  
        private static bool DisplayMainMenu()
        {
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. Add New Data");
            Console.WriteLine("2. View All Existing Data");
            Console.WriteLine("3. View Existing Data by User Group");
            Console.WriteLine("4. Edit Existing Data");
            Console.WriteLine("5. Delete Existing Data");
            Console.WriteLine("6. Exit System");
            Console.Write("\nPlease select an option (1-6): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            // Process user choice using switch statement
            switch (choice)
            {
                case "1":
                    AddNewData();
                    break;
                case "2":
                    ViewAllData();
                    break;
                case "3":
                    ViewDataByGroup();
                    break;
                case "4":
                    EditExistingData();
                    break;
                case "5":
                    DeleteExistingData();
                    break;
                case "6":
                    return false; // Exit the program
                default:
                    Console.WriteLine("Invalid option. Please select a number between 1 and 6.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return true; // Continue the program
        }

        /// <summary>
        /// Handles adding new data to the system
        /// Provides submenu for different user types and validates input
        /// </summary>
        private static void AddNewData()
        {
            Console.WriteLine("=== Add New Data ===");
            Console.WriteLine("1. Add New Teacher");
            Console.WriteLine("2. Add New Admin");
            Console.WriteLine("3. Add New Student");
            Console.Write("Select user type to add (1-3): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddNewTeacher();
                    break;
                case "2":
                    AddNewAdmin();
                    break;
                case "3":
                    AddNewStudent();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please choose 1, 2, or 3.");
                    break;
            }
        }

     
        private static void AddNewTeacher()
        {
            Console.WriteLine("=== Add New Teacher ===");
            
            // Collect basic person information
            Console.Write("Enter teacher's full name: ");
            string name = Console.ReadLine();
            
            Console.Write("Enter teacher's telephone: ");
            string telephone = Console.ReadLine();
            
            Console.Write("Enter teacher's email: ");
            string email = Console.ReadLine();
            
            // Collect teacher-specific information
            Console.Write("Enter teacher's salary: $");
            decimal salary;
            if (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid salary format. Setting to $0.00");
                salary = 0.00m;
            }
            
            Console.Write("Enter first subject taught: ");
            string subject1 = Console.ReadLine();
            
            Console.Write("Enter second subject taught: ");
            string subject2 = Console.ReadLine();

            // Create new teacher object and add to collection
            Teacher newTeacher = new Teacher(name, telephone, email, salary, subject1, subject2);
            teachers.Add(newTeacher);
            
            Console.WriteLine(string.Format("\nTeacher '{0}' added successfully!", name));
            Console.WriteLine(string.Format("Total teachers in system: {0}", teachers.Count));
        }

        
        private static void AddNewAdmin()
        {
            Console.WriteLine("=== Add New Admin ===");
            
            // Collect basic person information
            Console.Write("Enter admin's full name: ");
            string name = Console.ReadLine();
            
            Console.Write("Enter admin's telephone: ");
            string telephone = Console.ReadLine();
            
            Console.Write("Enter admin's email: ");
            string email = Console.ReadLine();
            
            // Collect admin-specific information
            Console.Write("Enter admin's salary: $");
            decimal salary;
            if (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid salary format. Setting to $0.00");
                salary = 0.00m;
            }
            
            Console.Write("Enter employment type (Full-time/Part-time): ");
            string employmentType = Console.ReadLine();
            
            Console.Write("Enter working hours per week: ");
            int workingHours;
            if (!int.TryParse(Console.ReadLine(), out workingHours))
            {
                Console.WriteLine("Invalid hours format. Setting to 40 hours");
                workingHours = 40;
            }

            // Create new admin object and add to collection
            Admin newAdmin = new Admin(name, telephone, email, salary, employmentType, workingHours);
            admins.Add(newAdmin);
            
            Console.WriteLine(string.Format("\nAdmin '{0}' added successfully!", name));
            Console.WriteLine(string.Format("Total admins in system: {0}", admins.Count));
        }
        
        /// </summary>
        private static void AddNewStudent()
        {
            Console.WriteLine("=== Add New Student ===");
            
            // Collect basic person information
            Console.Write("Enter student's full name: ");
            string name = Console.ReadLine();
            
            Console.Write("Enter student's telephone: ");
            string telephone = Console.ReadLine();
            
            Console.Write("Enter student's email: ");
            string email = Console.ReadLine();
            
            // Collect student-specific information (three subjects)
            Console.Write("Enter first subject: ");
            string subject1 = Console.ReadLine();
            
            Console.Write("Enter second subject: ");
            string subject2 = Console.ReadLine();
            
            Console.Write("Enter third subject: ");
            string subject3 = Console.ReadLine();

            // Create new student object and add to collection
            Student newStudent = new Student(name, telephone, email, subject1, subject2, subject3);
            students.Add(newStudent);
            
            Console.WriteLine(string.Format("\nStudent '{0}' added successfully!", name));
            Console.WriteLine(string.Format("Total students in system: {0}", students.Count));
        }

     
        private static void ViewAllData()
        {
            Console.WriteLine("=== View All Existing Data ===");
            
            int totalRecords = teachers.Count + admins.Count + students.Count;
            if (totalRecords == 0)
            {
                Console.WriteLine("No records found in the system.");
                return;
            }

            Console.WriteLine(string.Format("Total Records in System: {0}", totalRecords));
            Console.WriteLine(string.Format("Teachers: {0}, Admins: {1}, Students: {2}\n", teachers.Count, admins.Count, students.Count));

            // Display all teachers using polymorphism
            if (teachers.Count > 0)
            {
                Console.WriteLine("=== TEACHERS ===");
                for (int i = 0; i < teachers.Count; i++)
                {
                    Console.WriteLine(string.Format("{0}. {1}", i + 1, teachers[i].DisplayInfo()));
                }
                Console.WriteLine();
            }

            // Display all admins using polymorphism
            if (admins.Count > 0)
            {
                Console.WriteLine("=== ADMINISTRATION ===");
                for (int i = 0; i < admins.Count; i++)
                {
                    Console.WriteLine(string.Format("{0}. {1}", i + 1, admins[i].DisplayInfo()));
                }
                Console.WriteLine();
            }

            // Display all students using polymorphism
            if (students.Count > 0)
            {
                Console.WriteLine("=== STUDENTS ===");
                for (int i = 0; i < students.Count; i++)
                {
                    Console.WriteLine(string.Format("{0}. {1}", i + 1, students[i].DisplayInfo()));
                }
                Console.WriteLine();
            }
        }

    
        private static void ViewDataByGroup()
        {
            Console.WriteLine("=== View Data by User Group ===");
            Console.WriteLine("1. View Teachers");
            Console.WriteLine("2. View Administration");
            Console.WriteLine("3. View Students");
            Console.Write("Select group to view (1-3): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ViewTeachers();
                    break;
                case "2":
                    ViewAdmins();
                    break;
                case "3":
                    ViewStudents();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please choose 1, 2, or 3.");
                    break;
            }
        }

  
        private static void ViewTeachers()
        {
            Console.WriteLine("=== Teachers in System ===");
            
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers found in the system.");
                return;
            }

            Console.WriteLine(string.Format("Total Teachers: {0}\n", teachers.Count));
            
            for (int i = 0; i < teachers.Count; i++)
            {
                Console.WriteLine(string.Format("Teacher {0}:", i + 1));
                Console.WriteLine(teachers[i].GetDetailedInfo());
            }
        }

       
        private static void ViewAdmins()
        {
            Console.WriteLine("=== Administration Staff in System ===");
            
            if (admins.Count == 0)
            {
                Console.WriteLine("No administration staff found in the system.");
                return;
            }

            Console.WriteLine(string.Format("Total Admin Staff: {0}\n", admins.Count));
            
            for (int i = 0; i < admins.Count; i++)
            {
                Console.WriteLine(string.Format("Admin {0}:", i + 1));
                Console.WriteLine(admins[i].GetDetailedInfo());
            }
        }

  
        private static void ViewStudents()
        {
            Console.WriteLine("=== Students in System ===");
            
            if (students.Count == 0)
            {
                Console.WriteLine("No students found in the system.");
                return;
            }

            Console.WriteLine(string.Format("Total Students: {0}\n", students.Count));
            
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine(string.Format("Student {0}:", i + 1));
                Console.WriteLine(students[i].GetDetailedInfo());
            }
        }

 
        private static void EditExistingData()
        {
            Console.WriteLine("=== Edit Existing Data ===");
            Console.WriteLine("1. Edit Teacher");
            Console.WriteLine("2. Edit Admin");
            Console.WriteLine("3. Edit Student");
            Console.Write("Select user type to edit (1-3): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    EditTeacher();
                    break;
                case "2":
                    EditAdmin();
                    break;
                case "3":
                    EditStudent();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please choose 1, 2, or 3.");
                    break;
            }
        }

     
        private static void EditTeacher()
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers found to edit.");
                return;
            }

            Console.WriteLine("=== Edit Teacher ===");
            Console.WriteLine("Current Teachers:");
            
            // Display all teachers with index numbers
            for (int i = 0; i < teachers.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i + 1, teachers[i].DisplayInfo()));
            }

            Console.Write(string.Format("\nSelect teacher to edit (1-{0}): ", teachers.Count));
            int selection;
            if (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > teachers.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            // Get the selected teacher (convert to 0-based index)
            Teacher selectedTeacher = teachers[selection - 1];
            Console.WriteLine(string.Format("\nEditing: {0}\n", selectedTeacher.DisplayInfo()));

            // Allow editing of each field with current value display
            Console.WriteLine("Press Enter to keep current value, or enter new value:");
            
            Console.Write(string.Format("Name (current: {0}): ", selectedTeacher.Name));
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                selectedTeacher.Name = newName;

            Console.Write(string.Format("Telephone (current: {0}): ", selectedTeacher.Telephone));
            string newTelephone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTelephone))
                selectedTeacher.Telephone = newTelephone;

            Console.Write(string.Format("Email (current: {0}): ", selectedTeacher.Email));
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail))
                selectedTeacher.Email = newEmail;

            Console.Write(string.Format("Salary (current: ${0:F2}): $", selectedTeacher.Salary));
            string salaryInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(salaryInput))
            {
                decimal newSalary;
                if (decimal.TryParse(salaryInput, out newSalary))
                    selectedTeacher.Salary = newSalary;
            }

            Console.Write(string.Format("Subject 1 (current: {0}): ", selectedTeacher.Subject1));
            string newSubject1 = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSubject1))
                selectedTeacher.Subject1 = newSubject1;

            Console.Write(string.Format("Subject 2 (current: {0}): ", selectedTeacher.Subject2));
            string newSubject2 = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSubject2))
                selectedTeacher.Subject2 = newSubject2;

            Console.WriteLine(string.Format("\nTeacher '{0}' updated successfully!", selectedTeacher.Name));
        }

        /// <summary>
        /// Edits a selected admin's information
        /// Displays current admins and allows modification of selected record
        /// </summary>
        private static void EditAdmin()
        {
            if (admins.Count == 0)
            {
                Console.WriteLine("No admins found to edit.");
                return;
            }

            Console.WriteLine("=== Edit Admin ===");
            Console.WriteLine("Current Admin Staff:");
            
            // Display all admins with index numbers
            for (int i = 0; i < admins.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i + 1, admins[i].DisplayInfo()));
            }

            Console.Write(string.Format("\nSelect admin to edit (1-{0}): ", admins.Count));
            int selection;
            if (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > admins.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            // Get the selected admin (convert to 0-based index)
            Admin selectedAdmin = admins[selection - 1];
            Console.WriteLine(string.Format("\nEditing: {0}\n", selectedAdmin.DisplayInfo()));

            // Allow editing of each field with current value display
            Console.WriteLine("Press Enter to keep current value, or enter new value:");
            
            Console.Write(string.Format("Name (current: {0}): ", selectedAdmin.Name));
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                selectedAdmin.Name = newName;

            Console.Write(string.Format("Telephone (current: {0}): ", selectedAdmin.Telephone));
            string newTelephone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTelephone))
                selectedAdmin.Telephone = newTelephone;

            Console.Write(string.Format("Email (current: {0}): ", selectedAdmin.Email));
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail))
                selectedAdmin.Email = newEmail;

            Console.Write(string.Format("Salary (current: ${0:F2}): $", selectedAdmin.Salary));
            string salaryInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(salaryInput))
            {
                decimal newSalary;
                if (decimal.TryParse(salaryInput, out newSalary))
                    selectedAdmin.Salary = newSalary;
            }

            Console.Write(string.Format("Employment Type (current: {0}): ", selectedAdmin.EmploymentType));
            string newEmploymentType = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmploymentType))
                selectedAdmin.EmploymentType = newEmploymentType;

            Console.Write(string.Format("Working Hours (current: {0}): ", selectedAdmin.WorkingHours));
            string hoursInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(hoursInput))
            {
                int newHours;
                if (int.TryParse(hoursInput, out newHours))
                    selectedAdmin.WorkingHours = newHours;
            }

            Console.WriteLine(string.Format("\nAdmin '{0}' updated successfully!", selectedAdmin.Name));
        }

        /// <summary>
        /// Edits a selected student's information
        /// Displays current students and allows modification of selected record
        /// </summary>
        private static void EditStudent()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found to edit.");
                return;
            }

            Console.WriteLine("=== Edit Student ===");
            Console.WriteLine("Current Students:");
            
            // Display all students with index numbers
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i + 1, students[i].DisplayInfo()));
            }

            Console.Write(string.Format("\nSelect student to edit (1-{0}): ", students.Count));
            int selection;
            if (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > students.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            // Get the selected student (convert to 0-based index)
            Student selectedStudent = students[selection - 1];
            Console.WriteLine(string.Format("\nEditing: {0}\n", selectedStudent.DisplayInfo()));

            // Allow editing of each field with current value display
            Console.WriteLine("Press Enter to keep current value, or enter new value:");
            
            Console.Write(string.Format("Name (current: {0}): ", selectedStudent.Name));
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                selectedStudent.Name = newName;

            Console.Write(string.Format("Telephone (current: {0}): ", selectedStudent.Telephone));
            string newTelephone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTelephone))
                selectedStudent.Telephone = newTelephone;

            Console.Write(string.Format("Email (current: {0}): ", selectedStudent.Email));
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail))
                selectedStudent.Email = newEmail;

            Console.Write(string.Format("Subject 1 (current: {0}): ", selectedStudent.Subject1));
            string newSubject1 = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSubject1))
                selectedStudent.Subject1 = newSubject1;

            Console.Write(string.Format("Subject 2 (current: {0}): ", selectedStudent.Subject2));
            string newSubject2 = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSubject2))
                selectedStudent.Subject2 = newSubject2;

            Console.Write(string.Format("Subject 3 (current: {0}): ", selectedStudent.Subject3));
            string newSubject3 = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSubject3))
                selectedStudent.Subject3 = newSubject3;

            Console.WriteLine(string.Format("\nStudent '{0}' updated successfully!", selectedStudent.Name));
        }

        /// <summary>
        /// Handles deletion of existing data from the system
        /// Provides submenu for different user types and confirms deletion
        /// </summary>
        private static void DeleteExistingData()
        {
            Console.WriteLine("=== Delete Existing Data ===");
            Console.WriteLine("1. Delete Teacher");
            Console.WriteLine("2. Delete Admin");
            Console.WriteLine("3. Delete Student");
            Console.Write("Select user type to delete (1-3): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    DeleteTeacher();
                    break;
                case "2":
                    DeleteAdmin();
                    break;
                case "3":
                    DeleteStudent();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please choose 1, 2, or 3.");
                    break;
            }
        }

        /// <summary>
        /// Deletes a selected teacher from the system
        /// Displays current teachers, confirms deletion, and removes from collection
        /// </summary>
        private static void DeleteTeacher()
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers found to delete.");
                return;
            }

            Console.WriteLine("=== Delete Teacher ===");
            Console.WriteLine("Current Teachers:");
            
            // Display all teachers with index numbers
            for (int i = 0; i < teachers.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i + 1, teachers[i].DisplayInfo()));
            }

            Console.Write(string.Format("\nSelect teacher to delete (1-{0}): ", teachers.Count));
            int selection;
            if (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > teachers.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            // Get the selected teacher for confirmation
            Teacher selectedTeacher = teachers[selection - 1];
            Console.WriteLine("\nAre you sure you want to delete this teacher?");
            Console.WriteLine(selectedTeacher.DisplayInfo());
            Console.Write("Type 'YES' to confirm deletion: ");
            
            string confirmation = Console.ReadLine();
            if (confirmation != null && confirmation.ToUpper() == "YES")
            {
                string deletedName = selectedTeacher.Name;
                teachers.RemoveAt(selection - 1);
                Console.WriteLine(string.Format("\nTeacher '{0}' deleted successfully!", deletedName));
                Console.WriteLine(string.Format("Remaining teachers: {0}", teachers.Count));
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }

        /// <summary>
        /// Deletes a selected admin from the system
        /// Displays current admins, confirms deletion, and removes from collection
        /// </summary>
        private static void DeleteAdmin()
        {
            if (admins.Count == 0)
            {
                Console.WriteLine("No admins found to delete.");
                return;
            }

            Console.WriteLine("=== Delete Admin ===");
            Console.WriteLine("Current Admin Staff:");
            
            // Display all admins with index numbers
            for (int i = 0; i < admins.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i + 1, admins[i].DisplayInfo()));
            }

            Console.Write(string.Format("\nSelect admin to delete (1-{0}): ", admins.Count));
            int selection;
            if (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > admins.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            // Get the selected admin for confirmation
            Admin selectedAdmin = admins[selection - 1];
            Console.WriteLine("\nAre you sure you want to delete this admin?");
            Console.WriteLine(selectedAdmin.DisplayInfo());
            Console.Write("Type 'YES' to confirm deletion: ");
            
            string confirmation = Console.ReadLine();
            if (confirmation != null && confirmation.ToUpper() == "YES")
            {
                string deletedName = selectedAdmin.Name;
                admins.RemoveAt(selection - 1);
                Console.WriteLine(string.Format("\nAdmin '{0}' deleted successfully!", deletedName));
                Console.WriteLine(string.Format("Remaining admins: {0}", admins.Count));
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }

        /// <summary>
        /// Deletes a selected student from the system
        /// Displays current students, confirms deletion, and removes from collection
        /// </summary>
        private static void DeleteStudent()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found to delete.");
                return;
            }

            Console.WriteLine("=== Delete Student ===");
            Console.WriteLine("Current Students:");
            
            // Display all students with index numbers
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i + 1, students[i].DisplayInfo()));
            }

            Console.Write(string.Format("\nSelect student to delete (1-{0}): ", students.Count));
            int selection;
            if (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > students.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            // Get the selected student for confirmation
            Student selectedStudent = students[selection - 1];
            Console.WriteLine("\nAre you sure you want to delete this student?");
            Console.WriteLine(selectedStudent.DisplayInfo());
            Console.Write("Type 'YES' to confirm deletion: ");
            
            string confirmation = Console.ReadLine();
            if (confirmation != null && confirmation.ToUpper() == "YES")
            {
                string deletedName = selectedStudent.Name;
                students.RemoveAt(selection - 1);
                Console.WriteLine(string.Format("\nStudent '{0}' deleted successfully!", deletedName));
                Console.WriteLine(string.Format("Remaining students: {0}", students.Count));
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }
    }
}

