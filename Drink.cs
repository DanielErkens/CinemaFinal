using System;

public class Drink
{
    public double PriceOfChoiceDrink { get; set; }
    public int HowManyOfDrink { get; set; }
    public string NameOfDrink { get; set; }

    //Hier worden de drankjes aangemaakt. Aan de hand van het nummer dat wordt meegegeven, wordt bepaald welk soort drinken er wordt aangemaakt.
    public Drink(int whatChoiceDrink, int howMany)
    {
        if (whatChoiceDrink == 1)
        {
            this.NameOfDrink = "Coca Cola";
            this.HowManyOfDrink = howMany;
            this.PriceOfChoiceDrink = 2.49;
        }
        else if (whatChoiceDrink == 2)
        {
            this.NameOfDrink = "Cola Zero";
            this.HowManyOfDrink = howMany;
            this.PriceOfChoiceDrink = 2.49;
        }
        else if (whatChoiceDrink == 3)
        {
            this.NameOfDrink = "Spa Rood";
            this.HowManyOfDrink = howMany;
            this.PriceOfChoiceDrink = 2.49;
        }
        else if (whatChoiceDrink == 4)
        {
            this.NameOfDrink = "Fristi";
            this.HowManyOfDrink = howMany;
            this.PriceOfChoiceDrink = 2.29;
        }
        else if (whatChoiceDrink == 5)
        {
            this.NameOfDrink = "Chocomel";
            this.HowManyOfDrink = howMany;
            this.PriceOfChoiceDrink = 2.19;
        }
    }
}
