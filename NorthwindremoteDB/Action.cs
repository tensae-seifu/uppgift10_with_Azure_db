using Microsoft.EntityFrameworkCore;
using NorthwindremoteDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindremoteDB
{
    internal class Action
    {
        public static void AddCustomer()
        {

            var context = new NorWindContext();

            string name;


            Random res = new Random();


            String str = "ABCDEFGHUJKLMNOPQRSTUVWXYZ";
            int size = 5;


            string ran = "";

            for (int i = 0; i < size; i++)
            {

                int x = res.Next(26);


                ran = ran + str[x];
            }



            Console.WriteLine("Enter Company Name");
            name = Console.ReadLine();
            Console.WriteLine("Enter Contact Name");
            string Cname = Console.ReadLine();
            Console.WriteLine("Enter Contact Title");
            string Ctitle = Console.ReadLine();
            Console.WriteLine("Enter Customer's Address");
            string Caddress = Console.ReadLine();
            Console.WriteLine("Enter Customers's City");
            string Ccity = Console.ReadLine();
            Console.WriteLine("Enter Customer's Region");
            string Cregion = Console.ReadLine();
            Console.WriteLine("Enter Customer's Post Code");
            string Cpost = Console.ReadLine();
            Console.WriteLine("Enter Customer's Country");
            string Ccountry = Console.ReadLine();
            Console.WriteLine("Enter Customer's Phone Number");
            string Cphone = Console.ReadLine();
            Console.WriteLine("Enter Customer's Fax Number");
            string Cfax = Console.ReadLine();

            var existingCustomer = context.Customers
                .FirstOrDefault(customer => customer.CustomerId == ran);

            if (existingCustomer != null)
            {
                Console.WriteLine("Customer with the specified ID already exists.");
            }
            else
            {


                var newCustomer = new Customers
                {
                    CustomerId = ran,
                    CompanyName = name,
                    ContactName = Cname,
                    ContactTitle = Ctitle,
                    Address = Caddress,
                    City = Ccity,
                    Region = Cregion,
                    PostalCode = Cpost,
                    Country = Ccountry,
                    Phone = Cphone,
                    Fax = Cfax,


                };


                context.Customers.Add(newCustomer);

                context.SaveChanges();

                Console.WriteLine("\nCustomer added successfully.");
            }





        }
        public static void GetAllCustomer()
        {
            using (NorWindContext context = new NorWindContext())
            {



                var cust = context.Customers
                .Include(c => c.Orders)
                .Select(customer => new
                {
                    customer.CompanyName,
                    customer.Country,
                    customer.Region,
                    customer.Phone,
                    OrderCount = customer.Orders.Count()
                })
                .ToList();


                Console.WriteLine("Enter 'A' for ascending or 'D' for descending sorting:");
                string svar = Console.ReadLine();



                if (svar == "D")
                {
                    cust = cust.OrderByDescending(p => p.CompanyName).ToList();
                }
                else
                {
                    cust = cust.OrderBy(p => p.CompanyName).ToList();
                }



                Console.WriteLine("Customer Information:");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"{"Company Name",-25}\t\t{"Country",-15}\t\t{"Region",-15}\t{"Phone",-15}\t{"Total Orders",-15}");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

                foreach (var customer in cust)
                {

                    Console.WriteLine($"{customer.CompanyName,-25}\t\t {customer.Country,-15}\t{customer.Region,-15}\t{customer.Phone,-15}\t\t{customer.OrderCount,-15}");


                }
            }

        }
        public static void GetSingleCustomer()
        {
            Console.WriteLine("Enter the Company's Name\n");
            string name = Console.ReadLine();
            Console.WriteLine("\n");

            using (NorWindContext context = new NorWindContext())
            {
                List<Orders> ord = context.Customers
                    .Where(u => u.CompanyName == name)
                    .Include(c => c.Orders) 
                    .Single()
                    .Orders
                    .ToList();

                var cust = context.Customers
               .Where(u => u.CompanyName == name)
               .Select(u => new { u.CompanyName, u.Country, u.Region, u.Address, u.Phone })

               .FirstOrDefault();


                Console.WriteLine($"\t{cust.CompanyName}\t\t{cust.Country}\t\t{cust.Region}\t{cust.Address}\t\t{cust.Phone}\n");
                Console.WriteLine("\t\t\t\tHas made the following Orders");
                Console.WriteLine("\t\t\t\t==============================\n");



                foreach (var o in ord)

                {


                    Console.WriteLine($"{o.OrderId}\t{o.Customer.ContactName}\t{o.OrderDate}\t{o.ShipCity}\t{o.ShipAddress}\t{o.RequiredDate}\t{o.Freight}\n");

                }



                Console.WriteLine("==================================================================\n");



                var customer = context.Customers
                    .Where(c => c.CompanyName == name)
                    .FirstOrDefault();

                if (customer != null)
                {
                    var shippedOrderCount = context.Orders
                        .Where(o => o.CustomerId == customer.CustomerId && o.ShippedDate != null)
                        .Select(o => o.OrderId)
                        .Count();

                    Console.WriteLine($"Total orders shipped for {name}: {shippedOrderCount}");
                }
                if (customer != null)
                {
                    var shippedOrderCount = context.Orders
                        .Where(o => o.CustomerId == customer.CustomerId && o.ShippedDate == null)
                        .Select(o => o.OrderId)
                        .Count();

                    Console.WriteLine($"Total orders NOT shipped for {name}: {shippedOrderCount}");

                }
                else
                {
                    Console.WriteLine($"Customer with Company Name '{name}' not found.");
                }

            }


            Console.WriteLine("==================================================================\n");

        }
    }
}
