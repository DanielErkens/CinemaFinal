using System;

public class Ticket
{
    public int howManyOfMovie { get; set; }
    public string nameOfMovie { get; set; }
    public double priceOfMovie { get; set; }
    public int RowOfSeats { get; set; }
    public int[] ChosenChairs { get; set; }
    public Movie ChosenMovie { get; set; }
    public Tuple<int,int>[] ChosenChairsTuple { get; set; }

    //Hier wordt een nieuw ticket aangemaakt. Dit ticket wordt vervolgens toegevoegd aan de orderList.
    public Ticket(int howManyOfMovie, Movie chosenMovie, Tuple<int, int>[] ChosenChairsTuple)
    {
        this.howManyOfMovie = howManyOfMovie;
        this.nameOfMovie = chosenMovie.movieName;
        this.priceOfMovie = chosenMovie.moviePrice;
        this.ChosenChairsTuple = ChosenChairsTuple;
        this.RowOfSeats = this.ChosenChairsTuple[0].Item1;
        this.ChosenMovie = chosenMovie;

        //Als de user maar één stoel heeft gereserveerd, wordt er maar 1 nieuwe stoel toegevoegd.
        if (ChosenChairsTuple.Length == 1)
        {
            this.ChosenChairs = new int[1] { ChosenChairsTuple[0].Item2 };
        }

        //Bij een reservering van meerdere stoelen, wordt er een array van stoelen aangemaakt, overeenkomend met het aantal bestelde tickets.
        else
        {
            this.ChosenChairs = new int[ChosenChairsTuple.Length];
            for (int i = 0; i < ChosenChairsTuple.Length; i++)
            {
                this.ChosenChairs[i] = ChosenChairsTuple[i].Item2;
            }
        }
    }
}
