using System;
using System.Text.Json;
using System.Collections.Generic;

public class User
{
    public string username { get; set; }
    public string password { get; set; }

    public string PaymentCode { get; set; }
    public Order orderList { get; set; }
    public List<Ticket> ticketList { get; set; }
    public int ticketListIndex { get; set; }

    //Hier wordt een nieuwe user aangemaakt.
    public User(string username, string password, string paymentCode)
    {
        this.username = username;
        this.password = password;
        this.orderList = new Order();
        this.ticketList = new List<Ticket>();
        this.ticketListIndex = 0;
        this.PaymentCode = paymentCode;
    }

    //Deze code voegt een nieuwe filmreservering toe aan de orderlijst.
    public void AddTicket(Ticket TicketToAdd)
    {
        this.ticketList.Add(TicketToAdd);
        this.ticketListIndex++;
    }

    //Deze code voegt een nieuwe drankjesreservering toe aan de orderlijst.
    public void AddToOrderDrink(Drink drinkToAdd)
    {
        this.orderList.AddDrink(drinkToAdd);
    }

    //Deze code voegt een nieuwesnacksreservering toe aan de orderlijst.
    public void AddToOrderFood(Food foodToAdd)
    {
        this.orderList.AddFood(foodToAdd);
    }

    //Deze functie geeft de user het overzicht van alle geplaatste orders.
    public void CheckOrder(int whatToCheck)
    {
        Console.Clear();

        //Maakt een nieuwe double aan om de totaalprijs te kunnen teruggeven.
        double totalPrice = 0.0;

        //Deze loop itereert door alle bestelde filmtickets.
        for (int i = 0; i < this.ticketList.Count; i++)
        {

            //WhatToCheck geeft aan wat de user wil bekijken. Als deze waarde 1 is, wil de user alleen de films bekijken.
            //Bij 0 wil de user alle soorten reserveringen bekijken.
            if (whatToCheck == 1 || whatToCheck == 0)
            {
                //Check of er films zijn gereserveerd door de user.
                if (this.ticketList[i] != null)
                {
                    //Dit is voor een goed overzicht als de user veel verschillende soorten reserveringen heeft.
                    if (i == 0)
                    {
                        Console.WriteLine("=====================================");
                        Console.WriteLine("Tickets ordered: ");
                    }

                    //Bij elke filmreservering wordt er laten zien wat de gegevens zijn van de reservering.
                    Console.WriteLine("=====================================");
                    Console.WriteLine("OrderNumber: " + i);
                    Console.WriteLine("Movie: " + this.ticketList[i].nameOfMovie);
                    Console.WriteLine("Ammount of tickets: " + this.ticketList[i].howManyOfMovie);
                    Console.WriteLine("Price of one ticket: " + this.ticketList[i].priceOfMovie);

                    //Dit is de string om alle gereserveerde stoelen in een overzichtelijk format weer te geven.
                    string seatNumbers = "";

                    //Dit itereert door alle stoelnummers van het ticket.
                    for (int k = 0; k < this.ticketList[i].ChosenChairs.Length; k++)
                    {

                        //Dit zet een mooie punt achter de opsomming van de stoelen.
                        if (k == this.ticketList[i].ChosenChairs.Length - 1)
                        {
                            seatNumbers += this.ticketList[i].ChosenChairs[k] + ".";
                        }

                        //Als er nog stoelen aankomen, wordt dit laten zien door een komma voor de overzichtelijkheid.
                        else
                        {
                            seatNumbers += this.ticketList[i].ChosenChairs[k] + ", ";
                        }
                    }

                    //Laat de tekst voor de reservering voor 1 stoel met enkelvoudige werkwoorden zien.
                    if (seatNumbers == "1")
                    {
                        Console.WriteLine("The seat is on row " + this.ticketList[i].RowOfSeats + " and the seat number is: " + seatNumbers);
                        totalPrice += this.ticketList[i].howManyOfMovie * this.ticketList[i].priceOfMovie;
                    }

                    //Laat de tekst voor de reservering voor meerdere stoelen met meervoudige werkwoorden zien.
                    else
                    {
                        Console.WriteLine("The seats are on row " + this.ticketList[i].RowOfSeats + " and the seat numbers are: " + seatNumbers);
                        totalPrice += this.ticketList[i].howManyOfMovie * this.ticketList[i].priceOfMovie;
                    }

                }
            }
        }

        //Deze loop itereert door alle bestelde drankjes.
        for (int j = 0; j < this.orderList.drinks.Count; j++)
        {

            //WhatToCheck geeft aan wat de user wil bekijken. Als deze waarde 2 is, wil de user alleen de drankjes bekijken.
            //Bij 0 wil de user alle soorten reserveringen bekijken.
            if (whatToCheck == 2 || whatToCheck == 0)
            {

                //Check of er drankjes zijn besteld door de user.
                if (this.orderList.drinks[j] != null)
                {

                    //Dit is voor een goed overzicht als de user veel verschillende soorten reserveringen heeft.
                    if (j == 0)
                    {
                        Console.WriteLine("=====================================");
                        Console.WriteLine("Drinks ordered: ");
                    }

                    //Dit laat netjes alle informatie zien van elke drankjesreservering.
                    Console.WriteLine("=====================================");
                    Console.WriteLine("OrderNumber: " + j);
                    Console.WriteLine("Drink: " + this.orderList.drinks[j].NameOfDrink);
                    Console.WriteLine("Ammount of drinks: " + this.orderList.drinks[j].HowManyOfDrink);
                    Console.WriteLine("Price of one drink: " + this.orderList.drinks[j].PriceOfChoiceDrink);
                    totalPrice += this.orderList.drinks[j].HowManyOfDrink * this.orderList.drinks[j].PriceOfChoiceDrink;
                }
            }
        }

        //Deze loop itereert door alle bestelde snacks.
        for (int k = 0; k < this.orderList.food.Count; k++)
        {

            //WhatToCheck geeft aan wat de user wil bekijken. Als deze waarde 3 is, wil de user alleen de snakcs bekijken.
            //Bij 0 wil de user alle soorten reserveringen bekijken.
            if (whatToCheck == 3 || whatToCheck == 0)
            {

                //Check of er snacks zijn besteld door de user.
                if (this.orderList.food[k] != null)
                {

                    //Dit is voor een goed overzicht als de user veel verschillende soorten reserveringen heeft.
                    if (k == 0)
                    {
                        Console.WriteLine("=====================================");
                        Console.WriteLine("Snacks ordered: ");
                    }

                    //Dit laat netjes alle informatie zien van elke snacksreservering.
                    Console.WriteLine("=====================================");
                    Console.WriteLine("OrderNumber: " + k);
                    Console.WriteLine("Snack: " + this.orderList.food[k].NameOfFood);
                    Console.WriteLine("Ammount of snacks: " + this.orderList.food[k].HowManyOfFood);
                    Console.WriteLine("Price of one snack: " + this.orderList.food[k].PriceOfChoiceFood);
                    totalPrice += this.orderList.food[k].HowManyOfFood * this.orderList.food[k].PriceOfChoiceFood;
                }
            }
        }

        //Als de user alle orders wilde zien, krijgt de user ook de totaalprijs te zien. 
        //Bij het cancellen van een specifieke order is dat niet belangrijk.
        if (whatToCheck == 0)
        {
            totalPrice = Math.Round(totalPrice, 2);
            Console.WriteLine("=====================================");
            Console.WriteLine("The total price of all your orders is: $" + totalPrice + ".");
            Console.ReadLine();
        }

        
    }

    //Dit is de code voor het betalen van alle orders.
    public void TimeToPay(Movie[] movies, Zaal[] Zalen)
    {
        Console.Clear();

        //Dit is de string die aan de mail wordt meegegeven met daarin alle betalingsgegevns.
        string messageToSendToEmail = "";
        messageToSendToEmail += " User: " + this.username + "\n===================================== \n";

        //Hier wordt de double aangemaakt voor het weergeven van de totaalprijs van alle orders.
        double totalPrice = 0.0;

        //Deze loop itereert door alle bestelde filmtickets.
        for (int i = 0; i < this.ticketList.Count; i++)
        {

            //Check of er films zijn besteld door de user.
            if (this.ticketList[i] != null)
            {

                //Dit is voor een goed overzicht als de user veel verschillende soorten reserveringen heeft.
                if (i == 0)
                {
                    messageToSendToEmail += "Tickets ordered: \n";
                }

                //Dit laat netjes alle informatie zien van elke filmsreservering.
                messageToSendToEmail += "=====================================\n";
                messageToSendToEmail += "OrderNumber: " + i + "\n";
                messageToSendToEmail += "Movie: " + this.ticketList[i].nameOfMovie + "\n";
                messageToSendToEmail += "Ammount of tickets: " + this.ticketList[i].howManyOfMovie + "\n";
                messageToSendToEmail += "Price of one ticket: " + this.ticketList[i].priceOfMovie + "\n";

                //Hier worden de stoelen van het ticket in een net format laten zien.
                string seatNumbers = "";

                //Dit itereert door alle stoelnummers van het ticket.
                for (int k = 0; k < this.ticketList[i].ChosenChairs.Length; k++)
                {

                    //Dit zet een mooie punt achter de opsomming van de stoelen.
                    if (k == this.ticketList[i].ChosenChairs.Length - 1)
                    {
                        seatNumbers += this.ticketList[i].ChosenChairs[k] + ".";
                    }

                    //Als er nog stoelen aankomen, wordt dit laten zien door een komma voor de overzichtelijkheid.
                    else
                    {
                        seatNumbers += this.ticketList[i].ChosenChairs[k] + ", ";
                    }
                }

                //Laat de tekst voor de reservering voor 1 stoel met enkelvoudige werkwoorden zien.
                if (seatNumbers == "1")
                {
                    messageToSendToEmail += "The seat is on row " + this.ticketList[i].RowOfSeats + " and the seat number is: " + seatNumbers + "\n";
                    totalPrice += this.ticketList[i].howManyOfMovie * this.ticketList[i].priceOfMovie;
                }

                //Laat de tekst voor de reservering voor meerdere stoelen met meervoudige werkwoorden zien.
                else
                {
                    messageToSendToEmail += "The seats are on row " + this.ticketList[i].RowOfSeats + " and the seat numbers are: " + seatNumbers + "\n";
                    totalPrice += this.ticketList[i].howManyOfMovie * this.ticketList[i].priceOfMovie;
                }
            }
        }

        //Deze loop itereert door alle bestelde drankjes.
        for (int j = 0; j < this.orderList.drinks.Count; j++)
        {

            //Check of er drankjes zijn besteld door de user.
            if (this.orderList.drinks[j] != null)
            {

                //Dit is voor een goed overzicht als de user veel verschillende soorten reserveringen heeft.
                if (j == 0)
                {
                    messageToSendToEmail += "\n";
                    messageToSendToEmail += "=====================================" + "\n";
                    messageToSendToEmail += "Drinks ordered: \n";
                }

                //Dit laat netjes alle informatie zien van elke drankjesreservering.
                messageToSendToEmail += "=====================================\n";
                messageToSendToEmail += "OrderNumber: " + j + "\n";
                messageToSendToEmail += "Drink: " + this.orderList.drinks[j].NameOfDrink + "\n";
                messageToSendToEmail += "Ammount of drinks: " + this.orderList.drinks[j].HowManyOfDrink + "\n";
                messageToSendToEmail += "Price of one drink: " + this.orderList.drinks[j].PriceOfChoiceDrink + "\n";
                totalPrice += this.orderList.drinks[j].HowManyOfDrink * this.orderList.drinks[j].PriceOfChoiceDrink;
            }
        }

        //Deze loop itereert door alle bestelde snacks.
        for (int k = 0; k < this.orderList.food.Count; k++)
        {

            //Check of er snacks zijn besteld door de user.
            if (this.orderList.food[k] != null)
            {

                //Dit is voor een goed overzicht als de user veel verschillende soorten reserveringen heeft.
                if (k == 0)
                {
                    messageToSendToEmail += "\n";
                    messageToSendToEmail += "=====================================\n";
                    messageToSendToEmail += "Snacks ordered: \n";
                }

                //Dit laat netjes alle informatie zien van elke snacksreservering.
                messageToSendToEmail += "=====================================\n";
                messageToSendToEmail += "OrderNumber: " + k + "\n";
                messageToSendToEmail += "Snack: " + this.orderList.food[k].NameOfFood + "\n";
                messageToSendToEmail += "Ammount of snacks: " + this.orderList.food[k].HowManyOfFood + "\n";
                messageToSendToEmail += "Price of one snack: " + this.orderList.food[k].PriceOfChoiceFood + "\n";
                totalPrice += this.orderList.food[k].HowManyOfFood * this.orderList.food[k].PriceOfChoiceFood;
            }
        }

        //Dit rond de totaalprijs af op 2 decimalen.
        totalPrice = Math.Round(totalPrice, 2);

        //Dit is nog de eindinformatie van de mail.
        messageToSendToEmail += "\n";
        messageToSendToEmail += "\n";
        messageToSendToEmail += "The total price of your order was $" + totalPrice + "\n";
        messageToSendToEmail += "Payed with MasterCard.\n\n";
        messageToSendToEmail += "Have a nice day and thank you for visiting Dutch Cinema's!!";

        //Dit laat de user zien dat het tijd is om te betalen.
        Console.WriteLine("Time to pay for all orders!!");
        Console.ReadLine();

        //Dit vraagt om de paymentcode van de user.
        Console.WriteLine("Please enter your paymentcode:");
        string PaymentCodeToCheck = Console.ReadLine();

        //Zolang de opgegeven paymentcode niet overeenkomt met de paymentcode die de user zelf heeft opgeslagen bij het registreren,
        //Wordt de user opnieuw gevraagd om de paymentcode in te voeren.
        while(PaymentCodeToCheck != this.PaymentCode)
        {
            Console.WriteLine("Paymentcode incorrect.");
            Console.WriteLine("Please enter your paymentcode:");
            PaymentCodeToCheck = Console.ReadLine();
        }

        //Als de paymentcode correct is ingevoerd, gaan we door alle orders itereren.

        //Hier wordt er geïtereerd door alle bestelde filmtickets.
        while(this.ticketList.Count > 0)
        {

            //Hier wordt er per ticket door alle movies geïtereerd.
            for(int j = 0; j < movies.Length; j++)
            {

                //Nog een laatste check of er tickets openstaan.
                if(this.ticketList.Count > 0)
                {

                    //Als de naam van de movie van de ticket overeenkomt met een naam van een movie in de movielist,
                    //Wordt de index van die movie gepakt. Dit is gelijk het zaalnummer.
                    if (this.ticketList[0].ChosenMovie.movieName == movies[j].movieName)
                    {
                        int ZaalToChoose = j;

                        //Hier worden alle gereserveerde stoelen van het ticket op available gezet.
                        for (int k = 0; k < this.ticketList[0].howManyOfMovie; k++)
                        {
                            Zalen[ZaalToChoose].arr[this.ticketList[0].RowOfSeats][this.ticketList[0].ChosenChairsTuple[k].Item2].isAvailable = true;
                        }

                        //De ticket wordt van de ticketlist verwijderd.
                        this.ticketList.RemoveAt(0);
                    }
                }
                
            }
        }

        //Hier worden alle drankjesreserveringen verwijderd uit de orderlijst.
        while(this.orderList.drinks.Count > 0)
        {
            this.orderList.drinks.RemoveAt(0);
        }

        //Hier worden alle snacksreserveringen verwijderd uit de orderlijst.
        while (this.orderList.food.Count > 0)
        {
            this.orderList.food.RemoveAt(0);
        }

        //Hier wordt de mail verzonden met de messageToSendToEmail, waarin alle informatie van alle reserveringen staat.
        email emailtowrite = new email();
        email.writeEmail(messageToSendToEmail);

        //Laat de gebruiker zien dat de betaling is geslaagd.
        Console.WriteLine("");
        Console.WriteLine("Payment succesful!");
        Console.WriteLine("");
        Console.WriteLine("You have received your payment confirmation in your email");
        Console.ReadLine();
    }
    
}
