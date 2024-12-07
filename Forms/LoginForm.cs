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
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepository;

        public LoginForm()
        {
            InitializeComponent();
            _userRepository = new UserRepository("YourConnectionStringHere");  // DB bağlantısı
        }

        // Giriş düyməsinə basıldığında işləyəcək metod
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Giriş uğurludur!");
                this.Hide(); // Login formunu gizlətmək
                var mainForm = new MainForm();  // Ana forma keçid
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("İstifadəçi adı və ya şifrə səhvdir!");
            }
        }

        // İstifadəçi doğrulama metodu
        private bool AuthenticateUser(string username, string password)
        {
            // Verilənlər bazasından istifadəçi məlumatlarını alırıq
            var users = _userRepository.GetAll();

            // Şifrəni müqayisə edirik (real dünyada şifrələri hash şəklində müqayisə etmək daha yaxşıdır)
            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password) // Şifrəni burada düz saxlayırıq
                {
                    return true;
                }
            }
            return false;
        }

        // Şifrəni göstərmə/gizlətmə düyməsi
        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }
    }

}
