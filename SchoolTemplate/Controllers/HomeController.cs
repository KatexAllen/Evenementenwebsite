using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;


namespace SchoolTemplate.Controllers
{
    public class HomeController : Controller
    {
        // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
        string connectionString = "Server=172.16.160.21;Port=3306;Database=110273;Uid=110273;Pwd=ACKpiCIr;";
        public IActionResult Index()
        {
            return View(GetFestivals());
        }

        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from product", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product p = new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Calorieen = float.Parse(reader["calorieen"].ToString()),
                            Formaat = reader["Formaat"].ToString(),
                            Gewicht = Convert.ToInt32(reader["Gewicht"].ToString()),
                            Prijs = Decimal.Parse(reader["Prijs"].ToString())
                        };
                        products.Add(p);
                    }
                }
            }

            return products;
        }

        private List<Festival> GetFestivals()
        {
            List<Festival> Festival = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Festival", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString()
                        };
                        Festival.Add(p);
                    }
                }
            }

            return Festival;
        }

        [Route("Tickets")]
        public IActionResult Tickets()
        {
            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
       
        [Route("contact")]
        [HttpPost]
        public IActionResult contact(string voornaam, string achternaam)
        {
            return View();
        }

        [Route("Muntverkoop")]
        public IActionResult Muntverkoop()
        {
            return View();
        }

        [Route("Inloggen")]
        public IActionResult Inloggen()
        {
            return View();
        }

        [Route("Festival/{id}")]
        public IActionResult Festival(string id)
        {
            // haal festival op met nummertje {id}

            ViewData["id"] = id;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
