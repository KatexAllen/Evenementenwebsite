using System;

namespace SchoolTemplate.Database
{
  public class Klant
  {
    public int Id { get; set; }
    
    public string Naam { get; set; }

    public string Achternaam { get; set; }

   public string Emailadres { get; set; }

   public DateTime Geboortedatum { get; set; }

   public string Bericht { get; set; }
    }
}
