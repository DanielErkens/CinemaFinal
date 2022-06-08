using System;
using System.Collections.Generic;

public class Order
{
    public List<Drink> drinks { get; set; }
    public List<Food> food { get; set; }
    public int foodcounter { get; set; }
    public int drinkcounter { get; set; }

    //Hier wordt de order van een user voor het eerst aangemaakt.
    public Order()
    {
        this.food = new List<Food>();
        this.drinks = new List<Drink>();
        this.foodcounter = 0;
        this.drinkcounter = 0;
    }

    //Deze code voegt een gegeven aantal drankjes toe aan de drankjeslijst.
    public void AddDrink(Drink drink)
    {
        this.drinks.Add(drink);
    }

    //Deze code voegt een gegeven aantal snacks toe aan de snackslijst.
    public void AddFood(Food foodToAdd)
    {
        this.food.Add(foodToAdd);
    }
}
