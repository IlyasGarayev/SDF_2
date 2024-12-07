using SDF_2.data.models;
using SDF_2.data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDF_2.Forms
{
    public partial class MainForm : Form
    {
        private string _connectionString = "Server=localhost;Database=ExpenceManager;Integrated Security=True;";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // İstifadəçi adı və saatı göstərin
            lblHi.Text = $"Xoş gəldin {GetUserName()}"; // İstifadəçi adı alma funksiyası
            lblTimer.Text = DateTime.Now.ToString("HH:mm:ss");

            // Saatı yeniləmək üçün timer
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 saniyəlik interval
            timer.Tick += (s, ev) => lblTimer.Text = DateTime.Now.ToString("HH:mm:ss");
            timer.Start();

            // Xərclərin növlərini və siyahısını yükləyin
            LoadExpenseTypes();
            LoadExpenses();
        }

        private string GetUserName()
        {
            // İstifadəçi adı alınacaq (bu nümunədə sadəcə göstəririk)
            return "İsmayıl";
        }

        private void LoadExpenseTypes()
        {
            // Xərc növlərini yükləmək üçün repo istifadə edilir
            var expenseTypeRepo = new ExpenseRepository(_connectionString);
            var expenseTypes = expenseTypeRepo.GetAll();

            comboBox.DataSource = expenseTypes;
            comboBox.DisplayMember = "ExpenseTypeName"; // ComboBox-da görünəcək məlumat
            comboBox.ValueMember = "Id"; // Seçim dəyəri
        }

        private void LoadExpenses()
        {
            // Xərcləri cədvəldə göstərmək üçün repo istifadə edirik
            var expenseRepo = new ExpenseRepository(_connectionString);
            var expenses = expenseRepo.GetAll();
            dgv.DataSource = expenses.ToList();
        }

        // Yeni xərc növü əlavə etmək üçün açılacaq form
        private void btnAddExpenseType_Click(object sender, EventArgs e)
        {
            var addExpenseTypeForm = new AddExpenseTypeForm();
            addExpenseTypeForm.ShowDialog();
            LoadExpenseTypes(); // Xərc növləri siyahısını yenilə
        }

        // Xərci yadda saxlamaq üçün kliklənən button
        private void btnSaveExpense_Click(object sender, EventArgs e)
        {
            // Xərcin məlumatlarını alırıq
            var newExpense = new Expense
            {
                UserId = 1, // İstifadəçi məlumatını sadələşdirmək üçün
                ExpenseType = comboBox.SelectedItem.ToString(),
                Amount = Convert.ToDecimal(txtPrice.Text),
                Date = dateTimePicker.Value // Seçilən tarix
            };

            // Xərci bazaya əlavə edirik
            var expenseRepo = new ExpenseRepository(_connectionString);
            expenseRepo.Add(newExpense);

            // Yeni əlavə olunan xərcləri yükləyirik
            LoadExpenses();
        }
    }


}
