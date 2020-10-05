using System;

namespace SchoolTemplate.Database
{
  public class Festival
  {
    public int Id { get; set; }
    
    public string Naam { get; set; }

    public string Routebeschrijving { get; set; }    

    public DateTime Datum { get; set; }

    public Decimal Prijs { get; set; }

    public string Huisregels { get; set; }

    public string Nieuws { get; set; }

    public string Plattegrond { get; set; }
    } 
}

