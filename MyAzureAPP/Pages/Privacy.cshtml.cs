using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAzureAPP.Model;
using System.Data.SqlClient;

namespace MyAzureAPP.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public List<Employee> employeesList = new List<Employee>();
        public List<Employee> GetData()
        {
            //List<Employee> employees = new List<Employee>() {
            //new Employee()
            //{
            //    Id = 1,
            //    Name = "Employee 1",
            //    Department = "Admin",
            //    Salary = 15000,
            //},
            //     new Employee()
            //     {
            //         Id = 2,
            //         Name = "Employee 2",
            //         Department = "Admin",
            //         Salary = 18000,
            //     },
            //};

            List<Employee> employees = new List<Employee>();

            SqlConnection con = new SqlConnection("Server=tcp:azuredatabase1265.database.windows.net,1433;Initial Catalog=AzureDatabase;Persist Security Info=False;User ID=Gagan;Password=Password@1265;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand("Select * from Employee", con);
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee obj = new Employee();
                    obj.Id = Convert.ToInt32(dr["EmployeeId"].ToString());
                    obj.Name = dr["Name"].ToString();
                    obj.Department = dr["DepartmentName"].ToString();
                    obj.Salary = Convert.ToInt32(dr["Salary"].ToString());

                    employees.Add(obj);
                }
            }

            return employees;
        }

        public void OnGet()
        {
            employeesList = GetData();

        }
    }
}