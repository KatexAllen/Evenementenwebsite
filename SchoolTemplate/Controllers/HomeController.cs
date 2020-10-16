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

        private void SaveKlant(Klant klant)
        {
           
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(naam, achternaam, emailadres, geboortedatum, bericht) VALUES (?naam, ?achternaam, ?emailadres, ?geboortedatum, ?bericht)", conn);

                cmd.Parameters.Add("?naam", MySqlDbType.Text).Value = klant.Naam;
                cmd.Parameters.Add("?achternaam", MySqlDbType.Text).Value = klant.Achternaam;                
                cmd.Parameters.Add("?emailadres", MySqlDbType.Text).Value = klant.Emailadres;
                cmd.Parameters.Add("?geboortedatum", MySqlDbType.DateTime).Value = klant.Geboortedatum;
                cmd.Parameters.Add("?bericht", MySqlDbType.Text).Value = klant.Bericht;
                cmd.ExecuteNonQuery();
            }
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
                            Naam = reader["Naam"].ToString(),
                            Datum = DateTime.Parse(reader["Datum"].ToString()),
                            Prijs = Decimal.Parse(reader["Prijs"].ToString())
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
        [HttpPost]
        public IActionResult Contact(Klant model)
        {
            if (!ModelState.IsValid)
                return View(model);

            SaveKlant(model);
            return View();
        }

        [Route("contact")]
        public IActionResult contact()
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

        [Route("festival/{id}")]
        public IActionResult Festival(string id)
        {
            var model = GetFestivals(id);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
