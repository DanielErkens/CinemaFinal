using System;


public class Movie
{
    public string movieName { get; set; }
    public string playTime { get; set; }
    public string description { get; set; }
    public double moviePrice { get; set; }

    //Hier wordt Movie defined.
    public Movie(string movieName, string playTime, string description, double moviePrice)
    {
        this.movieName = movieName;
        this.playTime = playTime;
        this.description = description;
        this.moviePrice = moviePrice;
    }
}

