using SDF_2.data.models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDF_2.data.Repositories
{
    public class ExpenseTypeRepository
    {
        private readonly string _connectionString;

        public ExpenseTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Xərc növlərinin siyahısını almaq
        public List<ExpenseType> GetAll()
        {
            List<ExpenseType> expenseTypes = new List<ExpenseType>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM ExpenseTypes";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    expenseTypes.Add(new ExpenseType
                    {
                        Id = (int)reader["Id"],
                        Name = reader["ExpenseTypeName"].ToString()
                    });
                }
            }

            return expenseTypes;
        }

        // Yeni xərc növü əlavə etmək
        public void Add(ExpenseType expenseType)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO ExpenseTypes (ExpenseTypeName) VALUES (@ExpenseTypeName)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ExpenseTypeName", expenseType.Name);
                cmd.ExecuteNonQuery();
            }
        }
    }

}
