using SDF_2.data.models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDF_2.data.Repositories
{
    public class ExpenseRepository
    {
        private readonly string _connectionString;

        public ExpenseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Xərclərin siyahısını almaq
        public IEnumerable<Expense> GetAll()
        {
            List<Expense> expenses = new List<Expense>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Expenses";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    expenses.Add(new Expense
                    {
                        Id = (int)reader["Id"],
                        UserId = (int)reader["UserId"],
                        ExpenseType = reader["ExpenseType"].ToString(),
                        Amount = (decimal)reader["Amount"],
                        Date = (DateTime)reader["Date"]
                    });
                }
            }

            return expenses;
        }

        // Yeni xərc əlavə etmək
        public void Add(Expense expense)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Expenses (UserId, ExpenseType, Amount, Date) VALUES (@UserId, @ExpenseType, @Amount, @Date)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", expense.UserId);
                cmd.Parameters.AddWithValue("@ExpenseType", expense.ExpenseType);
                cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                cmd.Parameters.AddWithValue("@Date", expense.Date);
                cmd.ExecuteNonQuery();
            }
        }
    }


}
