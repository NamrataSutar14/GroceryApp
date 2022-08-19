using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            char ch;
            Console.WriteLine("To Add Data Enter a And To Update Data Enter u");
            ch = Convert.ToChar(Console.ReadLine());
            switch (Char.ToLower(ch))
            {
                case 'a':
                    Console.WriteLine("Add Data.");
                    AddData.Add();
                    break;
                case 'u':
                    Console.WriteLine("Update Data.");
                    UpdateData.Update();
                    break;
            }
            Console.ReadLine();
        }
    }
    public static class AddData
    {
        public static void Add()
        {
            Console.WriteLine("Enter Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter date: ");
            DateTime expiryDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Cost: ");
            int cost = int.Parse(Console.ReadLine());

            using(SqlConnection con= new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = "insert into Grocery(id,name,expiryDate,cost)" +
                    "values(@id, @name, @expiryDate, @cost)";
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters.AddWithValue("@name",name);
                cmd.Parameters.AddWithValue("@expiryDate", expiryDate);
                cmd.Parameters.AddWithValue("@cost", cost);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                if (rows > 0)
                {
                    Console.WriteLine("Inserted Successfully");
                }
                else
                {
                    Console.WriteLine("Fail to Insert");
                }
            }
        }
    }
    public static class UpdateData
    {
        public static void Update()
        {
            Console.WriteLine("Enter Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Date: ");
            DateTime expiryDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Cost: ");
            int cost = int.Parse(Console.ReadLine());

            using(SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = "Update Grocery set name=@name,expiryDate=@expiryDate,Cost=@cost)" +
                    "values(@name,@expiryDate,@cost where id=@id)";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@expiryDate", expiryDate);
                cmd.Parameters.AddWithValue("@cost", cost);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                if (rows > 0)
                {
                    Console.WriteLine("Updated Successfully");
                }
                else
                {
                    Console.WriteLine("Fail to Update");
                }
            }
        }
    }
}
