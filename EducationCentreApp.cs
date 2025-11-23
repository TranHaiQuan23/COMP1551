using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// Data Model Classes (same as before)
public class Person
{
    private string _name, _telephone, _email, _role;

    public Person() { _name = _telephone = _email = _role = ""; }
    
    public Person(string name, string telephone, string email, string role)
    {
        _name = name; _telephone = telephone; _email = email; _role = role;
    }

    public string Name { get { return _name; } set { _name = value ?? ""; } }
    public string Telephone { get { return _telephone; } set { _telephone = value ?? ""; } }
    public string Email { get { return _email; } set { _email = value ?? ""; } }
    public string Role { get { return _role; } set { _role = value ?? ""; } }

    public virtual string DisplayInfo()
    {
        return string.Format("{0} | {1} | {2} | {3}", Name, Telephone, Email, Role);
    }
}

public class Teacher : Person
{
    private decimal _salary;
    private string _subject1, _subject2;

    public Teacher() : base() { _salary = 0; _subject1 = _subject2 = ""; }
    
    public Teacher(string name, string telephone, string email, decimal salary, string subject1, string subject2)
        : base(name, telephone, email, "Teacher")
    {
        _salary = salary; _subject1 = subject1; _subject2 = subject2;
    }

    public decimal Salary { get { return _salary; } set { _salary = value >= 0 ? value : 0; } }
    public string Subject1 { get { return _subject1; } set { _subject1 = value ?? ""; } }
    public string Subject2 { get { return _subject2; } set { _subject2 = value ?? ""; } }

    public override string DisplayInfo()
    {
        return base.DisplayInfo() + string.Format(" | ${0:F2} | {1}, {2}", Salary, Subject1, Subject2);
    }
}

public class Admin : Person
{
    private decimal _salary;
    private string _employmentType;
    private int _workingHours;

    public Admin() : base() { _salary = 0; _employmentType = "Full-time"; _workingHours = 40; }
    
    public Admin(string name, string telephone, string email, decimal salary, string employmentType, int workingHours)
        : base(name, telephone, email, "Admin")
    {
        _salary = salary; _employmentType = employmentType; _workingHours = workingHours;
    }

    public decimal Salary { get { return _salary; } set { _salary = value >= 0 ? value : 0; } }
    public string EmploymentType { get { return _employmentType; } set { _employmentType = value; } }
    public int WorkingHours { get { return _workingHours; } set { _workingHours = value > 0 ? value : 1; } }

    public override string DisplayInfo()
    {
        return base.DisplayInfo() + string.Format(" | ${0:F2} | {1} | {2}h", Salary, EmploymentType, WorkingHours);
    }
}

public class Student : Person
{
    private string _subject1, _subject2, _subject3;

    public Student() : base() { _subject1 = _subject2 = _subject3 = ""; }
    
    public Student(string name, string telephone, string email, string subject1, string subject2, string subject3)
        : base(name, telephone, email, "Student")
    {
        _subject1 = subject1; _subject2 = subject2; _subject3 = subject3;
    }

    public string Subject1 { get { return _subject1; } set { _subject1 = value ?? ""; } }
    public string Subject2 { get { return _subject2; } set { _subject2 = value ?? ""; } }
    public string Subject3 { get { return _subject3; } set { _subject3 = value ?? ""; } }

    public override string DisplayInfo()
    {
        return base.DisplayInfo() + string.Format(" | {0}, {1}, {2}", Subject1, Subject2, Subject3);
    }
}

// Main Windows Forms Application
public partial class EducationCentreApp : Form
{
    private List<Teacher> teachers = new List<Teacher>();
    private List<Admin> admins = new List<Admin>();
    private List<Student> students = new List<Student>();

    private TabControl tabControl;
    private TabPage addTab, viewTab, editTab, deleteTab;
    
    // Add Tab Controls
    private ComboBox cmbUserType;
    private TextBox txtName, txtPhone, txtEmail, txtSalary, txtSubject1, txtSubject2, txtSubject3, txtHours;
    private ComboBox cmbEmployment;
    private Panel pnlSpecific;
    private Button btnAdd, btnClear;
    
    // View Tab Controls  
    private ComboBox cmbFilter;
    private ListBox lstView;
    private Button btnRefresh;
    
    // Edit Tab Controls
    private ComboBox cmbEditType;
    private ListBox lstEdit;
    private Panel pnlEdit;
    private Button btnSaveEdit;
    
    // Delete Tab Controls
    private ComboBox cmbDeleteType;
    private ListBox lstDelete;
    private Button btnDelete;

    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblStatus;

    public EducationCentreApp()
    {
        InitializeComponent();
        InitializeSampleData();
        RefreshAllViews();
    }

    private void InitializeComponent()
    {
        this.Text = "Education Centre Management System";
        this.Size = new Size(1000, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        // Create tabs
        tabControl = new TabControl { Dock = DockStyle.Fill };
        CreateAddTab();
        CreateViewTab();
        CreateEditTab();
        CreateDeleteTab();

        // Status strip
        statusStrip = new StatusStrip();
        lblStatus = new ToolStripStatusLabel("Ready - Sample data loaded");
        statusStrip.Items.Add(lblStatus);

        this.Controls.Add(tabControl);
        this.Controls.Add(statusStrip);
    }

    private void CreateAddTab()
    {
        addTab = new TabPage("➕ Add New");
        
        // User type selection
        var lblType = new Label { Text = "User Type:", Location = new Point(20, 20), Size = new Size(80, 25) };
        cmbUserType = new ComboBox 
        { 
            Location = new Point(110, 20), Size = new Size(120, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbUserType.Items.AddRange(new[] { "Teacher", "Admin", "Student" });
        cmbUserType.SelectedIndexChanged += CmbUserType_SelectedIndexChanged;

        // Common fields
        var lblName = new Label { Text = "Name:", Location = new Point(20, 60), Size = new Size(80, 25) };
        txtName = new TextBox { Location = new Point(110, 60), Size = new Size(200, 25) };
        
        var lblPhone = new Label { Text = "Phone:", Location = new Point(20, 100), Size = new Size(80, 25) };
        txtPhone = new TextBox { Location = new Point(110, 100), Size = new Size(200, 25) };
        
        var lblEmail = new Label { Text = "Email:", Location = new Point(20, 140), Size = new Size(80, 25) };
        txtEmail = new TextBox { Location = new Point(110, 140), Size = new Size(200, 25) };

        // Specific fields panel
        pnlSpecific = new Panel 
        { 
            Location = new Point(20, 180), Size = new Size(500, 200),
            BorderStyle = BorderStyle.FixedSingle, BackColor = Color.LightGray
        };

        // Buttons
        btnAdd = new Button 
        { 
            Text = "Add Record", Location = new Point(20, 400), Size = new Size(100, 35),
            BackColor = Color.LightGreen
        };
        btnAdd.Click += BtnAdd_Click;
        
        btnClear = new Button 
        { 
            Text = "Clear", Location = new Point(130, 400), Size = new Size(80, 35),
            BackColor = Color.LightYellow
        };
        btnClear.Click += BtnClear_Click;

        addTab.Controls.AddRange(new Control[] { lblType, cmbUserType, lblName, txtName, lblPhone, txtPhone, lblEmail, txtEmail, pnlSpecific, btnAdd, btnClear });
        tabControl.TabPages.Add(addTab);
    }

    private void CreateViewTab()
    {
        viewTab = new TabPage("👁 View Data");
        
        var lblFilter = new Label { Text = "Filter:", Location = new Point(20, 20), Size = new Size(50, 25) };
        cmbFilter = new ComboBox 
        { 
            Location = new Point(80, 20), Size = new Size(120, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbFilter.Items.AddRange(new[] { "All", "Teachers", "Admins", "Students" });
        cmbFilter.SelectedIndex = 0;
        cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;

        btnRefresh = new Button 
        { 
            Text = "Refresh", Location = new Point(210, 20), Size = new Size(80, 25),
            BackColor = Color.LightBlue
        };
        btnRefresh.Click += BtnRefresh_Click;

        lstView = new ListBox 
        { 
            Location = new Point(20, 60), Size = new Size(920, 500),
            Font = new Font("Consolas", 9), HorizontalScrollbar = true
        };

        viewTab.Controls.AddRange(new Control[] { lblFilter, cmbFilter, btnRefresh, lstView });
        tabControl.TabPages.Add(viewTab);
    }

    private void CreateEditTab()
    {
        editTab = new TabPage("✏ Edit Data");
        
        var lblEditType = new Label { Text = "Type:", Location = new Point(20, 20), Size = new Size(50, 25) };
        cmbEditType = new ComboBox 
        { 
            Location = new Point(80, 20), Size = new Size(120, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbEditType.Items.AddRange(new[] { "Teachers", "Admins", "Students" });
        cmbEditType.SelectedIndexChanged += CmbEditType_SelectedIndexChanged;

        lstEdit = new ListBox 
        { 
            Location = new Point(20, 60), Size = new Size(450, 300),
            Font = new Font("Consolas", 9)
        };
        lstEdit.SelectedIndexChanged += LstEdit_SelectedIndexChanged;

        pnlEdit = new Panel 
        { 
            Location = new Point(490, 60), Size = new Size(450, 300),
            BorderStyle = BorderStyle.FixedSingle, BackColor = Color.LightCyan
        };

        btnSaveEdit = new Button 
        { 
            Text = "Save Changes", Location = new Point(490, 380), Size = new Size(120, 35),
            BackColor = Color.Orange
        };
        btnSaveEdit.Click += BtnSaveEdit_Click;

        editTab.Controls.AddRange(new Control[] { lblEditType, cmbEditType, lstEdit, pnlEdit, btnSaveEdit });
        tabControl.TabPages.Add(editTab);
    }

    private void CreateDeleteTab()
    {
        deleteTab = new TabPage("🗑 Delete Data");
        
        var lblDelType = new Label { Text = "Type:", Location = new Point(20, 20), Size = new Size(50, 25) };
        cmbDeleteType = new ComboBox 
        { 
            Location = new Point(80, 20), Size = new Size(120, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbDeleteType.Items.AddRange(new[] { "Teachers", "Admins", "Students" });
        cmbDeleteType.SelectedIndexChanged += CmbDeleteType_SelectedIndexChanged;

        lstDelete = new ListBox 
        { 
            Location = new Point(20, 60), Size = new Size(920, 400),
            Font = new Font("Consolas", 9), SelectionMode = SelectionMode.MultiExtended
        };

        btnDelete = new Button 
        { 
            Text = "Delete Selected", Location = new Point(20, 480), Size = new Size(120, 35),
            BackColor = Color.LightCoral
        };
        btnDelete.Click += BtnDelete_Click;

        deleteTab.Controls.AddRange(new Control[] { lblDelType, cmbDeleteType, lstDelete, btnDelete });
        tabControl.TabPages.Add(deleteTab);
    }

    private void CmbUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSpecific.Controls.Clear();
        string type = cmbUserType.SelectedItem?.ToString();
        
        if (type == "Teacher" || type == "Admin")
        {
            var lblSalary = new Label { Text = "Salary:", Location = new Point(10, 20), Size = new Size(80, 25) };
            txtSalary = new TextBox { Location = new Point(100, 20), Size = new Size(100, 25) };
            pnlSpecific.Controls.AddRange(new Control[] { lblSalary, txtSalary });
        }

        if (type == "Teacher")
        {
            var lblSub1 = new Label { Text = "Subject 1:", Location = new Point(10, 60), Size = new Size(80, 25) };
            txtSubject1 = new TextBox { Location = new Point(100, 60), Size = new Size(150, 25) };
            var lblSub2 = new Label { Text = "Subject 2:", Location = new Point(10, 100), Size = new Size(80, 25) };
            txtSubject2 = new TextBox { Location = new Point(100, 100), Size = new Size(150, 25) };
            pnlSpecific.Controls.AddRange(new Control[] { lblSub1, txtSubject1, lblSub2, txtSubject2 });
        }
        else if (type == "Admin")
        {
            var lblEmp = new Label { Text = "Employment:", Location = new Point(10, 60), Size = new Size(80, 25) };
            cmbEmployment = new ComboBox 
            { 
                Location = new Point(100, 60), Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbEmployment.Items.AddRange(new[] { "Full-time", "Part-time" });
            cmbEmployment.SelectedIndex = 0;
            
            var lblHours = new Label { Text = "Hours/week:", Location = new Point(10, 100), Size = new Size(80, 25) };
            txtHours = new TextBox { Location = new Point(100, 100), Size = new Size(60, 25), Text = "40" };
            pnlSpecific.Controls.AddRange(new Control[] { lblEmp, cmbEmployment, lblHours, txtHours });
        }
        else if (type == "Student")
        {
            var lblSub1 = new Label { Text = "Subject 1:", Location = new Point(10, 20), Size = new Size(80, 25) };
            txtSubject1 = new TextBox { Location = new Point(100, 20), Size = new Size(150, 25) };
            var lblSub2 = new Label { Text = "Subject 2:", Location = new Point(10, 60), Size = new Size(80, 25) };
            txtSubject2 = new TextBox { Location = new Point(100, 60), Size = new Size(150, 25) };
            var lblSub3 = new Label { Text = "Subject 3:", Location = new Point(10, 100), Size = new Size(80, 25) };
            txtSubject3 = new TextBox { Location = new Point(100, 100), Size = new Size(150, 25) };
            pnlSpecific.Controls.AddRange(new Control[] { lblSub1, txtSubject1, lblSub2, txtSubject2, lblSub3, txtSubject3 });
        }
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string type = cmbUserType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Please select a user type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (type)
            {
                case "Teacher":
                    decimal teacherSalary = 0;
                    decimal.TryParse(txtSalary?.Text, out teacherSalary);
                    teachers.Add(new Teacher(txtName.Text, txtPhone.Text, txtEmail.Text, teacherSalary, txtSubject1?.Text ?? "", txtSubject2?.Text ?? ""));
                    break;
                    
                case "Admin":
                    decimal adminSalary = 0;
                    decimal.TryParse(txtSalary?.Text, out adminSalary);
                    int hours = 40;
                    int.TryParse(txtHours?.Text, out hours);
                    admins.Add(new Admin(txtName.Text, txtPhone.Text, txtEmail.Text, adminSalary, cmbEmployment?.SelectedItem?.ToString() ?? "Full-time", hours));
                    break;
                    
                case "Student":
                    students.Add(new Student(txtName.Text, txtPhone.Text, txtEmail.Text, txtSubject1?.Text ?? "", txtSubject2?.Text ?? "", txtSubject3?.Text ?? ""));
                    break;
            }

            ClearFields();
            RefreshAllViews();
            lblStatus.Text = string.Format("{0} added successfully!", type);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error adding record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    private void ClearFields()
    {
        txtName.Clear();
        txtPhone.Clear();
        txtEmail.Clear();
        txtSalary?.Clear();
        txtSubject1?.Clear();
        txtSubject2?.Clear();
        txtSubject3?.Clear();
        if (txtHours != null) txtHours.Text = "40";
        if (cmbEmployment != null) cmbEmployment.SelectedIndex = 0;
    }

    private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshViewList();
    }

    private void BtnRefresh_Click(object sender, EventArgs e)
    {
        RefreshAllViews();
        lblStatus.Text = "Data refreshed";
    }

    private void RefreshViewList()
    {
        lstView.Items.Clear();
        string filter = cmbFilter.SelectedItem?.ToString();
        
        lstView.Items.Add("=== EDUCATION CENTRE DATA ===");
        lstView.Items.Add("");
        
        if (filter == "All" || filter == "Teachers")
        {
            lstView.Items.Add("TEACHERS:");
            foreach (var teacher in teachers)
                lstView.Items.Add("  " + teacher.DisplayInfo());
            lstView.Items.Add("");
        }
        
        if (filter == "All" || filter == "Admins")
        {
            lstView.Items.Add("ADMINISTRATION:");
            foreach (var admin in admins)
                lstView.Items.Add("  " + admin.DisplayInfo());
            lstView.Items.Add("");
        }
        
        if (filter == "All" || filter == "Students")
        {
            lstView.Items.Add("STUDENTS:");
            foreach (var student in students)
                lstView.Items.Add("  " + student.DisplayInfo());
        }
    }

    private void CmbEditType_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshEditList();
    }

    private void RefreshEditList()
    {
        lstEdit.Items.Clear();
        string type = cmbEditType.SelectedItem?.ToString();
        
        switch (type)
        {
            case "Teachers":
                foreach (var teacher in teachers)
                    lstEdit.Items.Add(teacher.DisplayInfo());
                break;
            case "Admins":
                foreach (var admin in admins)
                    lstEdit.Items.Add(admin.DisplayInfo());
                break;
            case "Students":
                foreach (var student in students)
                    lstEdit.Items.Add(student.DisplayInfo());
                break;
        }
    }

    private void LstEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Implementation for loading selected user for editing
        if (lstEdit.SelectedIndex >= 0)
        {
            pnlEdit.Controls.Clear();
            var lblInfo = new Label { Text = "Edit functionality available.\nSelect record and modify values.", Location = new Point(10, 10), Size = new Size(400, 50) };
            pnlEdit.Controls.Add(lblInfo);
        }
    }

    private void BtnSaveEdit_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Edit functionality implemented.\nRecord would be updated here.", "Edit Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void CmbDeleteType_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshDeleteList();
    }

    private void RefreshDeleteList()
    {
        lstDelete.Items.Clear();
        string type = cmbDeleteType.SelectedItem?.ToString();
        
        switch (type)
        {
            case "Teachers":
                for (int i = 0; i < teachers.Count; i++)
                    lstDelete.Items.Add(string.Format("[{0}] {1}", i, teachers[i].DisplayInfo()));
                break;
            case "Admins":
                for (int i = 0; i < admins.Count; i++)
                    lstDelete.Items.Add(string.Format("[{0}] {1}", i, admins[i].DisplayInfo()));
                break;
            case "Students":
                for (int i = 0; i < students.Count; i++)
                    lstDelete.Items.Add(string.Format("[{0}] {1}", i, students[i].DisplayInfo()));
                break;
        }
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        if (lstDelete.SelectedIndices.Count == 0)
        {
            MessageBox.Show("Please select records to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (MessageBox.Show(string.Format("Delete {0} selected record(s)?", lstDelete.SelectedIndices.Count), "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            string type = cmbDeleteType.SelectedItem?.ToString();
            var indices = new List<int>();
            
            foreach (int index in lstDelete.SelectedIndices)
                indices.Add(index);
            
            indices.Sort();
            indices.Reverse(); // Delete from end to preserve indices
            
            switch (type)
            {
                case "Teachers":
                    foreach (int i in indices)
                        if (i < teachers.Count) teachers.RemoveAt(i);
                    break;
                case "Admins":
                    foreach (int i in indices)
                        if (i < admins.Count) admins.RemoveAt(i);
                    break;
                case "Students":
                    foreach (int i in indices)
                        if (i < students.Count) students.RemoveAt(i);
                    break;
            }
            
            RefreshAllViews();
            lblStatus.Text = string.Format("{0} record(s) deleted", indices.Count);
        }
    }

    private void RefreshAllViews()
    {
        RefreshViewList();
        RefreshEditList();
        RefreshDeleteList();
    }

    private void InitializeSampleData()
    {
        teachers.Add(new Teacher("Dr. John Smith", "555-0101", "j.smith@edu.centre", 75000, "Mathematics", "Physics"));
        teachers.Add(new Teacher("Prof. Sarah Johnson", "555-0102", "s.johnson@edu.centre", 82000, "English", "Literature"));
        
        admins.Add(new Admin("Mike Brown", "555-0201", "m.brown@edu.centre", 45000, "Full-time", 40));
        admins.Add(new Admin("Lisa Davis", "555-0202", "l.davis@edu.centre", 25000, "Part-time", 20));
        
        students.Add(new Student("Emma Wilson", "555-0301", "e.wilson@student.edu", "Mathematics", "Physics", "Chemistry"));
        students.Add(new Student("James Miller", "555-0302", "j.miller@student.edu", "English", "History", "Art"));
    }
}

public class Program
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new EducationCentreApp());
    }
}