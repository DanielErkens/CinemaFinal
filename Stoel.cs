using System;

public class Stoel
{
    public int Row { get; set; }
    public int Seat { get; set; }
    public bool isAvailable { get; set; }

    //Hier wordt een stoel aangemaakt in een zaal.
    public Stoel(int Row, int Seat)
    {
        this.Row = Row;
        this.Seat = Seat;
        this.isAvailable = true;
    }
}
