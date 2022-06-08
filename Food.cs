using System;

public class Food
{
    public double PriceOfChoiceFood { get; set; }
    public int HowManyOfFood { get; set; }
    public string NameOfFood { get; set; }

    //Hier worden de snacks aangemaakt. Aan de hand van het nummer dat wordt meegegeven, wordt bepaald welk soort snack er wordt aangemaakt.
    public Food(int whatChoiceFood, int howMany)
    {
        if (whatChoiceFood == 1)
        {
            this.NameOfFood = "Snickers";
            this.HowManyOfFood = howMany;
            this.PriceOfChoiceFood = 2.49;
        }
        else if (whatChoiceFood == 2)
        {
            this.NameOfFood = "M&M";
            this.HowManyOfFood = howMany;
            this.PriceOfChoiceFood = 2.79;
        }
        else if (whatChoiceFood == 3)
        {
            this.NameOfFood = "Autodrop";
            this.HowManyOfFood = howMany;
            this.PriceOfChoiceFood = 2.09;
        }
        else if (whatChoiceFood == 4)
        {
            this.NameOfFood = "Mars";
            this.HowManyOfFood = howMany;
            this.PriceOfChoiceFood = 2.49;
        }
        else if (whatChoiceFood == 5)
        {
            this.NameOfFood = "Twix";
            this.HowManyOfFood = howMany;
            this.PriceOfChoiceFood = 2.49;
        }
        else if (whatChoiceFood == 6)
        {
            this.NameOfFood = "KitKat";
            this.HowManyOfFood = howMany;
            this.PriceOfChoiceFood = 2.49;
        }
        else if (whatChoiceFood == 7)
        {
            this.NameOfFood = "Popcorn salt";
            this.HowManyOfFood = howMany;
            this.PriceOfChoiceFood = 6.50;
        }
        else if (whatChoiceFood == 8)
        {
            this.NameOfFood = "Popcorn sweet";
            this.HowManyOfFood = howMany;
            this.PriceOfChoiceFood = 6.50;
        }
        else if (whatChoiceFood == 9)
        {
            this.NameOfFood = "Dorritos";
            this.HowManyOfFood = howMany;
            this.PriceOfChoiceFood = 3.50;
        }
    }
}
