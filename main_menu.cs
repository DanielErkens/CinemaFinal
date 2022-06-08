using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Main_Menu
{

    class Program
    {

        static void Main()
        {
            //Het begin van de code. Hier worden de json files uitgelezen.
            ConvertToJson jsonConverter = new ConvertToJson();
            List<User> Users = jsonConverter.loadJson1();
            Zaal[] Zalen = jsonConverter.loadJson2();
            Movie[] MovieList = jsonConverter.loadJson3();

            //Hier wordt de user gevraagd in te loggen of om een nieuw account te registreren. 
            // Het ingelogde of geregistreerde account wordt vanaf hier aan elke class en functie meegegeven.
            bool showMenu = true;
            User CurrentUser = logIn.logInUser(Users); 

            //Deze while loop blijft het hoofdmenu aanroepen, zolang de user niet aangeeft dat hij wil exiten.
            while (showMenu)
            {
                showMenu = MainMenu(CurrentUser, Zalen, Users, jsonConverter, MovieList);
            }



        }

        //Dit is het hoofdmenu. Hier wordt keer op keer gevraagd welke actie de user wil uitvoeren.
        private static bool MainMenu(User currentUser, Zaal[] Zalen, List<User> Users, ConvertToJson jsonConverter, Movie[] MovieList)
        {
            //Deze line schrijft tussendoor alle info naar de json files, zodat er nooit informatie verloren gaat.
            jsonConverter.WriteToJson(Users, Zalen, MovieList);


            Console.Clear();
            Console.WriteLine(@"
 _______   __    __  .___________.  ______  __    __       ______  __  .__   __.  _______ .___  ___.      ___     __     _______.
|       \ |  |  |  | |           | /      ||  |  |  |     /      ||  | |  \ |  | |   ____||   \/   |     /   \   (_ )   /       |
|  .--.  ||  |  |  | `---|  |----`|  ,----'|  |__|  |    |  ,----'|  | |   \|  | |  |__   |  \  /  |    /  ^  \   |/   |   (----`
|  |  |  ||  |  |  |     |  |     |  |     |   __   |    |  |     |  | |  . `  | |   __|  |  |\/|  |   /  /_\  \        \   \    
|  '--'  ||  `--'  |     |  |     |  `----.|  |  |  |    |  `----.|  | |  |\   | |  |____ |  |  |  |  /  _____  \   .----)   |   
|_______/  \______/      |__|      \______||__|  |__|     \______||__| |__| \__| |_______||__|  |__| /__/     \__\  |_______/    
                                                                                                                                 

");

            //Deze writeLines laten de user zien welke acties de user allemaal kan uitvoeren.
            Console.WriteLine("================================================\n");
            Console.WriteLine("Hello, welcome to the movie-application. Please choose what you want to do:");
            Console.WriteLine("[1] See the playing movies");
            Console.WriteLine("[2] Order Tickets");
            Console.WriteLine("[3] Bioscoop Info");
            Console.WriteLine("[4] Order from drink and food menu");
            Console.WriteLine("[5] See what your order list is at the moment");
            Console.WriteLine("[6] Cancel a specific order");
            Console.WriteLine("[7] Pay all orders");
            Console.WriteLine("[8] Exit\n");

            Console.Write("Please select an option: ");

            //Deze switch is de user input. Dit geeft aan welke actie de user wil uitvoeren.
            switch (Console.ReadLine())
            {
                case "1":
                    //Roept de code aan waarin de user alle spelende films kan zien.
                    Movies(MovieList);
                    return true;
                case "2":
                    //Roept de code aan waarin de user een film, zaal en stoelen kan reserveren.
                    Tickets(currentUser, Zalen, MovieList);
                    return true;
                case "3":
                    //Roept de code aan waarin de user informatie over de bioscoop kan lezen.
                    Info();
                    return true;
                case "4":
                    //Roept de code aan waain de user een snack of een drankje kan bestellen.
                    SnackMenu(currentUser);
                    return true;
                case "5":
                    //Roept de code aan waarin de user het overzicht van alle orders en de totaalprijs kan bekijken.
                    currentUser.CheckOrder(0);
                    return true;
                case "6":
                    //Roept de code aan waarin de user een specifieke order kan cancellen.
                    CancelReservation(currentUser, Zalen, MovieList);
                    return true;
                case "7":
                    //Roept de code aan waarin de user betaalt voor alle reserveringen.
                    currentUser.TimeToPay(MovieList, Zalen);
                    return true;
                case "8":
                    //Roept de code aan waarin de user het programma afsluit.
                    Exit_Program(currentUser, Users, jsonConverter, Zalen, MovieList);
                    return false;
                default:
                    //Indien er een onjuiste input wordt gegeven, blijft het hoofdmenu verversen.
                    return true;
            }
        }

        //Dit is de code waarin de user alle spelende films kan bekijken.
        private static void Movies(Movie[] MovieList)
        {
            //Deze code laat steeds 1 film tegelijk zien, met alle bijbehorende eigenschappen van die film.
            for (int i = 0; i < MovieList.Length; i++)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("Name: " + MovieList[i].movieName);
                Console.WriteLine("Description: " + MovieList[i].description);
                Console.WriteLine("Playtime: " + MovieList[i].playTime);
                Console.WriteLine("Price per ticket: $" + MovieList[i].moviePrice);
                Console.WriteLine("Press any key to go to the next movie:");
                Console.ReadLine();
            }

        }

        //Deze code is voor het reserveren van een ticket voor een film.
        public static void Tickets(User CurrentUser, Zaal[] Zalen, Movie[] MovieList)
        {
            //Deze code geeft nog een keer een overzicht van alle spelende films waaruit de user kan kiezen.
            Console.Clear();
            Console.WriteLine("Our movies currently playing are:");
            string[] MovieListChoice = { "[1] Lord of the Rings The Fellowship of the Ring", "[2] Lord of the Rings The Two Towers", "[3] Lord of the Rings The Return of the King", "[4] The Hobbit An Unexpected Journey", "[5] The Hobbit The Desolation of Smaug", "[6] The Hobbit The Battle of the Five Armies", "[7] Spider-man: No Way Home", "[8] 6 Underground", "[9] The Hitman's Bodyguard", "[10] Deadpool" };
            Console.WriteLine(String.Join("\n", MovieListChoice));
            Console.WriteLine("Please select a Movie");

            //Dit is de user input voor het kiezen van de film.
            switch (Console.ReadLine())
            {
                //Dit is de code voor het reserveren van film nummer 1.
                case "1":

                    //Deze code vraagt om de hoeveelheid tickets die de user wil bestellen.
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[0].movieName + ". How many tickets do you want to order (maximum 6)?");

                    //Dit is de user input voor het aantal tickets.
                    switch (Console.ReadLine())
                    {
                        //Dit is de code voor het reserveren van de tickets van de aangegeven stoelen.
                        case "1":
                            //Code voor het toevoegen van 1 ticket. De hieropvolgende cases zijn voor steeds 1 ticket meer reserveren.
                            CurrentUser.AddTicket(new Ticket(1, MovieList[0], Seat_reservation(1, Zalen[0])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[0], Seat_reservation(2, Zalen[0])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[0], Seat_reservation(3, Zalen[0])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[0], Seat_reservation(4, Zalen[0])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[0], Seat_reservation(5, Zalen[0])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[0], Seat_reservation(6, Zalen[0])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        //Bij een onjuiste user input voor het aantal tickts wordt er terugverwezen naar het hoofdmenu.
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;

                    }
                    break;

                //Code voor het reserveren van zaal 2. Alle code werkt hetzelfde als het reserveren van film 1, maar dan met een andere film.
                case "2":
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[1].movieName + ". How many tickets do you want to order (maximum 6)?");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CurrentUser.AddTicket(new Ticket(1, MovieList[1], Seat_reservation(1, Zalen[1])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[1], Seat_reservation(2, Zalen[1])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[1], Seat_reservation(3, Zalen[1])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[1], Seat_reservation(4, Zalen[1])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[1], Seat_reservation(5, Zalen[1])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[1], Seat_reservation(6, Zalen[1])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                //Code voor het reserveren van zaal 3. Alle code werkt hetzelfde als het reserveren van film 1, maar dan met een andere film.
                case "3":
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[2].movieName + ". How many tickets do you want to order (maximum 6)?");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CurrentUser.AddTicket(new Ticket(1, MovieList[2], Seat_reservation(1, Zalen[2])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[2], Seat_reservation(2, Zalen[2])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[2], Seat_reservation(3, Zalen[2])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[2], Seat_reservation(4, Zalen[2])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[2], Seat_reservation(5, Zalen[2])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[2], Seat_reservation(6, Zalen[2])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                //Code voor het reserveren van zaal 4. Alle code werkt hetzelfde als het reserveren van film 1, maar dan met een andere film.
                case "4":
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[3].movieName + ". How many tickets do you want to order (maximum 6)?");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CurrentUser.AddTicket(new Ticket(1, MovieList[3], Seat_reservation(1, Zalen[3])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[3], Seat_reservation(2, Zalen[3])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[3], Seat_reservation(3, Zalen[3])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[3], Seat_reservation(4, Zalen[3])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[3], Seat_reservation(5, Zalen[3])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[3], Seat_reservation(6, Zalen[3])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                //Code voor het reserveren van zaal 5. Alle code werkt hetzelfde als het reserveren van film 1, maar dan met een andere film.
                case "5":
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[4].movieName + ". How many tickets do you want to order (maximum 6)?");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CurrentUser.AddTicket(new Ticket(1, MovieList[4], Seat_reservation(1, Zalen[4])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[4], Seat_reservation(2, Zalen[4])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[4], Seat_reservation(3, Zalen[4])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[4], Seat_reservation(4, Zalen[4])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[4], Seat_reservation(5, Zalen[4])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[4], Seat_reservation(6, Zalen[4])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                //Code voor het reserveren van zaal 6. Alle code werkt hetzelfde als het reserveren van film 1, maar dan met een andere film.
                case "6":
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[5].movieName + ". How many tickets do you want to order (maximum 6)?");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CurrentUser.AddTicket(new Ticket(1, MovieList[5], Seat_reservation(1, Zalen[5])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[5], Seat_reservation(2, Zalen[5])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[5], Seat_reservation(3, Zalen[5])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[5], Seat_reservation(4, Zalen[5])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[5], Seat_reservation(5, Zalen[5])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[5], Seat_reservation(6, Zalen[5])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                //Code voor het reserveren van zaal 7. Alle code werkt hetzelfde als het reserveren van film 1, maar dan met een andere film.
                case "7":
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[6].movieName + ". How many tickets do you want to order (maximum 6)?");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CurrentUser.AddTicket(new Ticket(1, MovieList[6], Seat_reservation(1, Zalen[6])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[6], Seat_reservation(2, Zalen[6])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[6], Seat_reservation(3, Zalen[6])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[6], Seat_reservation(4, Zalen[6])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[6], Seat_reservation(5, Zalen[6])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[6], Seat_reservation(6, Zalen[6])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                //Code voor het reserveren van zaal 8. Alle code werkt hetzelfde als het reserveren van film 1, maar dan met een andere film.
                case "8":
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[7].movieName + ". How many tickets do you want to order (maximum 6)?");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CurrentUser.AddTicket(new Ticket(1, MovieList[7], Seat_reservation(1, Zalen[7])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[7], Seat_reservation(2, Zalen[7])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[7], Seat_reservation(3, Zalen[7])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[7], Seat_reservation(4, Zalen[7])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[7], Seat_reservation(5, Zalen[7])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[7], Seat_reservation(6, Zalen[7])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                //Code voor het reserveren van zaal 9. Alle code werkt hetzelfde als het reserveren van film 1, maar dan met een andere film.
                case "9":
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[8].movieName + ". How many tickets do you want to order (maximum 6)?");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CurrentUser.AddTicket(new Ticket(1, MovieList[8], Seat_reservation(1, Zalen[8])));
                            Console.WriteLine("Reservation succesful!");

                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[8], Seat_reservation(2, Zalen[8])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[8], Seat_reservation(3, Zalen[8])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[8], Seat_reservation(4, Zalen[8])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[8], Seat_reservation(5, Zalen[8])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[8], Seat_reservation(6, Zalen[8])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                //Code voor het reserveren van zaal 10. Alle code werkt hetzelfde als het reserveren van film 1, maar dan met een andere film.
                case "10":
                    Console.Clear();
                    Console.WriteLine("You chose the movie: " + MovieList[9].movieName + ". How many tickets do you want to order (maximum 6)?");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            CurrentUser.AddTicket(new Ticket(1, MovieList[9], Seat_reservation(1, Zalen[9])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "2":
                            CurrentUser.AddTicket(new Ticket(2, MovieList[9], Seat_reservation(2, Zalen[9])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "3":
                            CurrentUser.AddTicket(new Ticket(3, MovieList[9], Seat_reservation(3, Zalen[9])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "4":
                            CurrentUser.AddTicket(new Ticket(4, MovieList[9], Seat_reservation(4, Zalen[9])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "5":
                            CurrentUser.AddTicket(new Ticket(5, MovieList[9], Seat_reservation(5, Zalen[9])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        case "6":
                            CurrentUser.AddTicket(new Ticket(6, MovieList[9], Seat_reservation(6, Zalen[9])));
                            Console.WriteLine("Reservation succesful!");
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;

                //Bij een onjuiste input voor het kiezen van de film, wordt er terugverwezen naar het hoofdmenu.
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Returning to main menu...");
                    Console.ReadLine();
                    break;
            }
        }

        //Dit is de code voor het bestellen van een snack of een drankje.
        private static void SnackMenu(User currentUser)
        {
            //Laat de user de keuze zien uit drankjes en snacks.
            Console.Clear();
            string[] drinkmenu = { "[1] Drinks", "[2] Snacks" };
            Console.WriteLine(String.Join("\n", drinkmenu));

            //De user input voor het kiezen van een drankje of een snack bestellen.
            switch (Console.ReadLine())
            {

                //De code voor het keuzemenu van drankjes.
                case "1":
                    Console.Clear();
                    Console.WriteLine("Please select a drink:");
                    Console.WriteLine("Drinks menu: \n [1] Coca Cola \n [2] Cola Zero \n [3] Spa Rood \n [4] Fristi \n [5] Chocomel \n");

                    //De user input voor het gekozen drankje.
                    switch (Console.ReadLine())
                    {
                        //Dit is de code voor het kiezen hoeveel keer de user de eenheid coca cola wil bestellen.
                        case "1":
                            Console.Clear();
                            Console.WriteLine("You chose the drink: Coca Cola. How many drinks do you want to order (maximum 6)?");

                            //Dit is de user input voor de hoeveelheid van het gekozen drankje.
                            switch (Console.ReadLine())
                            {
                                case "1":

                                    //Deze code voegt 1 eenheid van het gekozen drankje toe aan de orderlijst.
                                    currentUser.AddToOrderDrink(new Drink(1, 1));
                                    break;
                                case "2":
                                    //Deze code voegt 2 eenheden van het gekozen drankje toe aan de orderlijst. De volgende case 3, enz.
                                    currentUser.AddToOrderDrink(new Drink(1, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderDrink(new Drink(1, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderDrink(new Drink(1, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderDrink(new Drink(1, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderDrink(new Drink(1, 6));
                                    break;
                                //Bij een incorrecte input wordt de user terugverwezen naar het hoofdscherm.
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Deze code werkt hetzelfde als de eerste case, maar dan met een andere eenheid.
                        case "2":
                            Console.Clear();
                            Console.WriteLine("You chose the drink: Coca Zero. How many drinks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderDrink(new Drink(2, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderDrink(new Drink(2, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderDrink(new Drink(2, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderDrink(new Drink(2, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderDrink(new Drink(2, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderDrink(new Drink(2, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Deze code werkt hetzelfde als de eerste case, maar dan met een andere eenheid.
                        case "3":
                            Console.Clear();
                            Console.WriteLine("You chose the drink: Spa Rood. How many drinks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderDrink(new Drink(3, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderDrink(new Drink(3, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderDrink(new Drink(3, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderDrink(new Drink(3, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderDrink(new Drink(3, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderDrink(new Drink(3, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Deze code werkt hetzelfde als de eerste case, maar dan met een andere eenheid.
                        case "4":
                            Console.Clear();
                            Console.WriteLine("You chose the drink: Fristi. How many drinks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderDrink(new Drink(4, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderDrink(new Drink(4, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderDrink(new Drink(4, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderDrink(new Drink(4, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderDrink(new Drink(4, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderDrink(new Drink(4, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Deze code werkt hetzelfde als de eerste case, maar dan met een andere eenheid.
                        case "5":
                            Console.Clear();
                            Console.WriteLine("You chose the drink: Chocomel. How many drinks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderDrink(new Drink(5, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderDrink(new Drink(5, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderDrink(new Drink(5, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderDrink(new Drink(5, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderDrink(new Drink(5, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderDrink(new Drink(5, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;
                    }
                    break;

                case "2":

                    //Dit is de code die de gebruiker de opties van het snack menu geeft.
                    Console.Clear();
                    Console.WriteLine("Please select a snack: ");
                    Console.WriteLine("snack menu: \n [1] Snickers \n [2] M&M \n [3] Autodrop \n [4] Mars \n [5] Twix \n [6] KitKat \n [7] Popcorn salt \n [8] Popcorn sweet \n [9] Dorritos \n");
                   
                    //Dit is de user input voor het gekozen snack.
                    switch (Console.ReadLine())
                    {
                        case "1":

                            //Dit is de code die de gebruiker laat kiezen hoeveel eenheden Snickers hij wil bestellen.
                            Console.Clear();
                            Console.WriteLine("You chose the snack: Snickers. How many snacks do you want to order (maximum 6)?");
                            
                            //Dit is de user input voor de hoeveelheid eenheden van de gekozen snack hij wil reserveren.
                            switch (Console.ReadLine())
                            {

                                //Deze case voegt 1 eenheid van de gekozen snack toe aan de orderlijst.
                                case "1":
                                    currentUser.AddToOrderFood(new Food(1, 1));
                                    break;

                                //Deze case voegt 2 eenheden van de gekozen snack toe aan de orderlijst. Elke case loopt dit eentje op.
                                case "2":
                                    currentUser.AddToOrderFood(new Food(1, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderFood(new Food(1, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderFood(new Food(1, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderFood(new Food(1, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderFood(new Food(1, 6));
                                    break;

                                //Bij een incorrecte input wordt de user terugverwezen naar het hoofdmenu.
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Dit is hetzelfde principe als de eerste case, maar dan met een andere eenheid.
                        case "2":
                            Console.Clear();
                            Console.WriteLine("You chose the snack: M&M. How many snacks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderFood(new Food(2, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderFood(new Food(2, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderFood(new Food(2, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderFood(new Food(2, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderFood(new Food(2, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderFood(new Food(2, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Dit is hetzelfde principe als de eerste case, maar dan met een andere eenheid.
                        case "3":
                            Console.Clear();
                            Console.WriteLine("You chose the snack: Autodrop. How many snacks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderFood(new Food(3, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderFood(new Food(3, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderFood(new Food(3, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderFood(new Food(3, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderFood(new Food(3, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderFood(new Food(3, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Dit is hetzelfde principe als de eerste case, maar dan met een andere eenheid.
                        case "4":
                            Console.Clear();
                            Console.WriteLine("You chose the snack: Mars. How many snacks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderFood(new Food(4, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderFood(new Food(4, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderFood(new Food(4, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderFood(new Food(4, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderFood(new Food(4, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderFood(new Food(4, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }

                            break;

                        //Dit is hetzelfde principe als de eerste case, maar dan met een andere eenheid.
                        case "5":
                            Console.Clear();
                            Console.WriteLine("You chose the snack: Twix. How many snacks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderFood(new Food(5, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderFood(new Food(5, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderFood(new Food(5, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderFood(new Food(5, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderFood(new Food(5, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderFood(new Food(5, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Dit is hetzelfde principe als de eerste case, maar dan met een andere eenheid.
                        case "6":
                            Console.Clear();
                            Console.WriteLine("You chose the snack: Kitkat. How many snacks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderFood(new Food(6, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderFood(new Food(6, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderFood(new Food(6, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderFood(new Food(6, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderFood(new Food(6, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderFood(new Food(6, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }

                            break;

                        //Dit is hetzelfde principe als de eerste case, maar dan met een andere eenheid.
                        case "7":
                            Console.Clear();
                            Console.WriteLine("You chose the snack: Popcorn salt. How many snacks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderFood(new Food(7, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderFood(new Food(7, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderFood(new Food(7, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderFood(new Food(7, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderFood(new Food(7, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderFood(new Food(7, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Dit is hetzelfde principe als de eerste case, maar dan met een andere eenheid.
                        case "8":
                            Console.Clear();
                            Console.WriteLine("You chose the snack: Popcorn sweet. How many snacks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderFood(new Food(8, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderFood(new Food(8, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderFood(new Food(8, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderFood(new Food(8, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderFood(new Food(8, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderFood(new Food(8, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Dit is hetzelfde principe als de eerste case, maar dan met een andere eenheid.
                        case "9":
                            Console.Clear();
                            Console.WriteLine("You chose the snack: Dorittos. How many snacks do you want to order (maximum 6)?");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    currentUser.AddToOrderFood(new Food(9, 1));
                                    break;
                                case "2":
                                    currentUser.AddToOrderFood(new Food(9, 2));
                                    break;
                                case "3":
                                    currentUser.AddToOrderFood(new Food(9, 3));
                                    break;
                                case "4":
                                    currentUser.AddToOrderFood(new Food(9, 4));
                                    break;
                                case "5":
                                    currentUser.AddToOrderFood(new Food(9, 5));
                                    break;
                                case "6":
                                    currentUser.AddToOrderFood(new Food(9, 6));
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Invalid input. Returning to main menu...");
                                    Console.ReadLine();
                                    break;
                            }
                            break;

                        //Bij een incorrecte userinput wordt de user teruggestuurd naar het hoofdmenu.
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input. Returning to main menu...");
                            Console.ReadLine();
                            break;
                    }
                    break;
            }
        }

        //Deze code is bedoeld voor het geven van informatie over de bioscoop aan de user.
        private static void Info()
        {
            //Deze code geeft de opties weer waarover de user informatie kan krijgen.
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("[1] To see the location of the cinema \n[2] To see our latest technologies \n[3] To see our rooms");

            //Dit is de userinput welke informatie de user wil weten.
            switch (Console.ReadLine())
            {
                //Deze case geeft de user informatie over de locatie van de bioscoop.
                case "1":
                    Console.Clear();
                    Console.WriteLine("=====================================");
                    Console.WriteLine("Location: Wijnhaven 107, 3011 WN in Rotterdam");
                    Console.ReadLine();
                    break;

                //Deze case geeft de user informatie over de voorzieningen van de bioscoopzalen.
                case "2":
                    Console.Clear();
                    Console.WriteLine("=====================================");
                    Console.WriteLine("Here we have the latest technologies such as: " +
                        "\n-Auro 3D certified cinema sound system " +
                        "\n-IMAX 3D digital certified visual projectors " +
                        "\n-Super comfortable (VIP) seats with plenty of legroom and space in between");
                    Console.ReadLine();
                    break;

                //Deze case geeft de user informatie over de voorzieningen van de bioscoop zelf.
                case "3":
                    Console.Clear();
                    Console.WriteLine("=====================================");
                    Console.WriteLine("In our cinema chain we are making going to the movies a experience " +
                        "and where lounging is a key word.\nOur cinema will consist of:\n-a bar\n-a lounge erea" +
                        "\n-three auditoriums with seats of 150 -, 300 - and 500 respectively");
                    Console.ReadLine();
                    break;

                //Bij incorrecte input van de user wordt de user teruggestuurd naar het hoofdmenu.
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid input. Returning to main menu...");
                    Console.ReadLine();
                    break;
            }
        }

        //Dit is een iets ingewikkeldere functie. Deze functie laat de zaal met daarin alle stoelen zien.
        //Ook kiest de user hier de stoelen, en de beschikbaarheid van die stoelen wordt hier gechecked.
        private static Tuple<int, int>[] Seat_reservation(int howManyTickets, Zaal zaal)
        {

            //Deze code geeft de user informatie over de hoeveelheid stoelen die de user heeft gereserveerd.
            Console.Clear();
            Tuple<int, int>[] stoelen = new Tuple<int, int>[howManyTickets];
            Console.WriteLine($"Your reservation is for {howManyTickets} Seats:");
            string showTheZaal = "";
            //In deze for loop worden de rijen opgezet in de vorm van een string.
            for (int i = 0; i < zaal.arr.Length; i++) 
            {
                //In deze loop worden de stoelen in een rij ingevuld.
                for (int j = 0; j < zaal.arr[i].Length; j++)
                {
                    //Deze code geeft een nette layout voor het begin van de rij, dat de user ziet in welke rij hij zit.
                    if (j == 0 && i < 10)
                    {
                        showTheZaal += "Row  " + i;
                    }
                    //Deze else if statement zet bij een rij groter dan 9 een spatie minder, omdat 10 en groter 2 digits heeft en de rij anders scheef zou zijn.
                    else if (j == 0)
                    {
                        showTheZaal += "Row " + i;
                    }
                    //Deze if-statement checkt bij elke stoel of de stoel beschikbaar is. Is de stoel beschikbaar, komt er het nummer van de stoel te staan.
                    if (zaal.arr[i][j].isAvailable == true)
                    {
                        int seatNumber = j;
                        showTheZaal += "| " + seatNumber + " ";
                    } 
                    //Is de stoel al bezet, komt er een kruisje bij de stoel te staan.
                    else
                    {
                        showTheZaal += "| x ";
                    }
                } 
                //Dit zet de enter nadat een rij compleet is.
                showTheZaal += "\n";
            }
            //Deze writeline laat de hele zaal zien als een string, inclusief index. Deze zaal hebben we hierboven opgebouwd.
            Console.WriteLine(showTheZaal);

            //Dit is de code voor het reserveren van meerdere stoelen.
            if (howManyTickets > 1) 
            {

                //Dit vraagt om user input voor de rij waarin hij stoelen gaat reserveren.
                Console.WriteLine("Please select the row You want to reserve your seats for");
                string rowString = Console.ReadLine();

                //Zolang de user input niet binnen de bestaande rijen valt, wordt de user opnieuw gevraagd om een rij te kiezen.
                while (rowString != "0" && rowString != "1" && rowString != "2" && rowString != "3" && rowString != "4" && rowString != "5" && rowString != "6" && rowString != "7" && rowString != "8" && rowString != "9" && rowString != "10" && rowString != "11" && rowString != "12")
                {
                    Console.WriteLine("Please select the row You want to reserve your seats for");
                    rowString = Console.ReadLine();
                }

                //Dit vraagt om de meest linker stoel die de gebruiker reserveert.
                //De gereserveerde stoelen worden dan vanaf de meest linker stoel tot aan: gereserveerde stoel + aantal tickets.
                //De user reserveert dus alle stoelen naast elkaar.
                int ChosenRow = Int32.Parse(rowString);
                Console.WriteLine("Please select your leftmost chair");
                string leftChairstring = Console.ReadLine();

                //Als de user een incorrect stoelnummer geeft, wordt er opnieuw om een stoelnummer gevraagd.
                while (leftChairstring != "0" && leftChairstring != "1" && leftChairstring != "2" && leftChairstring != "3" && leftChairstring != "4" && leftChairstring != "5" && leftChairstring != "6" && leftChairstring != "7" && leftChairstring != "8" && leftChairstring != "9" && leftChairstring != "10" && leftChairstring != "11" && leftChairstring != "12")
                {
                    Console.WriteLine("Please select your leftmost chair");
                    leftChairstring = Console.ReadLine();
                }

                int leftChair = Int32.Parse(leftChairstring);

                //Roept de CheckSeats aan, die controleert of geen van de gekozen stoelen al gereserveerd is, of buiten de rij valt.
                bool checktheseats = CheckSeats(zaal, ChosenRow, leftChair, howManyTickets);

                //Als één of meer van de gekozen stoelen al gereserveerd is of buiten de rij valt, wordt het proces hierboven herhaald.
                while (!checktheseats) 
                {
                    //Dit is een herhaling van de code hierboven, en het blijft herhaald worden totdat een geldige rij en stoelen zijn gekozen

                    Console.Clear();
                    Console.WriteLine($"Your reservation is for {howManyTickets} Seats:");
                    Console.WriteLine(showTheZaal);

                    Console.WriteLine("Please select the row You want to reserve your seats for");
                    rowString = Console.ReadLine();


                    while (rowString != "0" && rowString != "1" && rowString != "2" && rowString != "3" && rowString != "4" && rowString != "5" && rowString != "6" && rowString != "7" && rowString != "8" && rowString != "9" && rowString != "10" && rowString != "11" && rowString != "12")
                    {
                        Console.WriteLine("Please select the row You want to reserve your seats for");
                        rowString = Console.ReadLine();
                    }


                    ChosenRow = Int32.Parse(rowString);
                    Console.WriteLine("Please select your leftmost chair");
                    leftChairstring = Console.ReadLine();


                    while (leftChairstring != "0" && leftChairstring != "1" && leftChairstring != "2" && leftChairstring != "3" && leftChairstring != "4" && leftChairstring != "5" && leftChairstring != "6" && leftChairstring != "7" && leftChairstring != "8" && leftChairstring != "9" && leftChairstring != "10" && leftChairstring != "11" && leftChairstring != "12")
                    {
                        Console.WriteLine("Please select your leftmost chair");
                        leftChairstring = Console.ReadLine();
                    }

                    leftChair = Int32.Parse(leftChairstring);
                    checktheseats = CheckSeats(zaal, ChosenRow, leftChair, howManyTickets);
                }
                
                int indexForStoelen = 0;

                //Dit gedeelte van de code zet de beschikbaarheid van alle gekozen stoelen op false.
                for (int i = 0; i < stoelen.Length; i++)
                {
                    stoelen[i] = Tuple.Create(ChosenRow, leftChair + indexForStoelen);
                    zaal.arr[ChosenRow][leftChair + indexForStoelen].isAvailable = false;
                    indexForStoelen++;
                }
            }

            //Dit is code voor wanneer er maar één ticket wordt gereserveerd.
            else
            {
                //Deze code vraagt de user om een rij te kiezen.
                Console.WriteLine("Please select the row You want to reserve your seat for");
                string rowString = Console.ReadLine();

                //Zolang de user geen bestaande rij invoert, wordt er opnieuw gevraagd om een rij te kiezen.
                while (rowString != "0" && rowString != "1" && rowString != "2" && rowString != "3" && rowString != "4" && rowString != "5" && rowString != "6" && rowString != "7" && rowString != "8" && rowString != "9" && rowString != "10" && rowString != "11" && rowString != "12")
                {
                    Console.WriteLine("Please select the row You want to reserve your seat for");
                    rowString = Console.ReadLine();
                }

                //Deze code vraagt de user om een stoel te kiezen.
                int ChosenRow = Int32.Parse(rowString);
                Console.WriteLine("Please select your chair");
                string ChosenChairstring = Console.ReadLine();

                //Zolang de user geen bestaande stoel invoert, wordt er opnieuw gevraagd om een stoel te kiezen.
                while (ChosenChairstring != "0" && ChosenChairstring != "1" && ChosenChairstring != "2" && ChosenChairstring != "3" && ChosenChairstring != "4" && ChosenChairstring != "5" && ChosenChairstring != "6" && ChosenChairstring != "7" && ChosenChairstring != "8" && ChosenChairstring != "9" && ChosenChairstring != "10" && ChosenChairstring != "11" && ChosenChairstring != "12")
                {
                    Console.WriteLine("Please select your chair");
                    ChosenChairstring = Console.ReadLine();
                }

                int chosenChair = Int32.Parse(ChosenChairstring);

                //Deze code checkt of de stoel al gereserveerd is.
                bool checktheseats = CheckSeats(zaal, ChosenRow, chosenChair, howManyTickets);

                //Zolang er geen correcte rij en stoel zijn gegeven, wordt de user opniuw gevraagd om een rij en stoel te kiezen.
                while (!checktheseats)
                {
                    Console.Clear();
                    Console.WriteLine($"Your reservation is for {howManyTickets} Seats:");
                    Console.WriteLine(showTheZaal);


                    Console.WriteLine("Please select the row You want to reserve your seat for");
                    rowString = Console.ReadLine();


                    while (rowString != "0" && rowString != "1" && rowString != "2" && rowString != "3" && rowString != "4" && rowString != "5" && rowString != "6" && rowString != "7" && rowString != "8" && rowString != "9" && rowString != "10" && rowString != "11" && rowString != "12")
                    {
                        Console.WriteLine("Please select the row You want to reserve your seat for");
                        rowString = Console.ReadLine();
                    }

                    ChosenRow = Int32.Parse(rowString);

                    Console.WriteLine("Please select your chair");
                    ChosenChairstring = Console.ReadLine();

                    while (ChosenChairstring != "0" && ChosenChairstring != "1" && ChosenChairstring != "2" && ChosenChairstring != "3" && ChosenChairstring != "4" && ChosenChairstring != "5" && ChosenChairstring != "6" && ChosenChairstring != "7" && ChosenChairstring != "8" && ChosenChairstring != "9" && ChosenChairstring != "10" && ChosenChairstring != "11" && ChosenChairstring != "12")
                    {
                        Console.WriteLine("Please select your chair");
                        ChosenChairstring = Console.ReadLine();
                    }

                    chosenChair = Int32.Parse(ChosenChairstring);

                    checktheseats = CheckSeats(zaal, ChosenRow, chosenChair, howManyTickets);

                }
                //Deze code zet de beschikbaarheid van de gekozen stoel op false.
                stoelen[0] = Tuple.Create(ChosenRow, chosenChair);
                zaal.arr[ChosenRow][chosenChair].isAvailable = false;
            }
            return stoelen;
        }

        //Deze code checkt of de gekozen stoel(en) buiten de rij vallen en of de stoel(en) al zijn gereserveerd.
        public static bool CheckSeats(Zaal zaal, int row, int chair, int howManySeats)
        {
            //Deze loop gaat elke stoel langs, zolang het aantal gecheckte stoelen kleiner is als het aantal gereserveerde stoelen.
            for (int i = 0; i < howManySeats; i++)
            {
                //Als de stoel buiten de rij valt, wordt er false gereturned.
                if (chair + i > 12)
                {
                    //Geeft de juiste informatie mee, zodat de user weet wat er fout ging.
                    Console.WriteLine($"You chose row {row} seat {chair + i}, but the number of chairs in that row is restricted to 12, and your rightmost ticket exceeds chair 12. Please select another leftmostchair");
                    Console.ReadLine();
                    return false;
                }
                //Als er ook maar één van de gekozen stoelen gereserveerd is, wordt er false gereturned.
                else if (zaal.arr[row][chair + i].isAvailable == false)
                {
                    //Geeft de juiste informatie mee, zodat de user weet wat er fout ging.
                    Console.WriteLine($"You chose row {row} seat {chair + i}, but that seat is already taken. Please select another leftmostchair");
                    Console.ReadLine();
                    return false;

                }
            }
            return true;
        }

        //Deze code sluit het programma af.
        private static void Exit_Program(User currentUser, List<User> Users, ConvertToJson jsonConverter, Zaal[] Zalen, Movie[] MovieList)
        {
            Console.Clear();

            //Deze regel slaat alle gegevens op in de json files. Dit wordt ook tussendoor gedaan.
            jsonConverter.WriteToJson(Users, Zalen, MovieList);


            Console.WriteLine("=====================================");
            Console.WriteLine("You exit this app");
            Console.ReadLine();
        }

        //Deze code is voor het cancellen van een specifieke order.
        private static void CancelReservation(User CurrentUser, Zaal[] Zalen, Movie[] movieList)
        {
            //Dit gedeelte geeft een overzicht van alle soorten reserveringen die gecancelled kunnen worden.
            Console.Clear();
            Console.WriteLine("What kind of reservation do you want to cancel?");
            Console.WriteLine("[1] Movie reservation.");
            Console.WriteLine("[2] Drink reservation.");
            Console.WriteLine("[3] Snack reservation.");
            Console.WriteLine("[4] Return to main menu.");

            //Dit is de user input die aangeeft welk soort reservering de gebruiker wil cancellen.
            string whatCancel = Console.ReadLine();

            //Zolang de user input niet correct is, wordt er steeds opnieuw gevraagd om een gode input.
            while (whatCancel != "1" && whatCancel != "2" && whatCancel != "3" && whatCancel != "4")
            {
                Console.Clear();
                Console.WriteLine("Invalid Input.");
                Console.WriteLine("What kind of reservation do you want to cancel?");
                Console.WriteLine("[1] Movie reservation.");
                Console.WriteLine("[2] Drink reservation.");
                Console.WriteLine("[3] Snack reservation.");
                Console.WriteLine("[4] Return to main menu.");

                whatCancel = Console.ReadLine();
            }

            //Code voor het cancellen van een movie reservering.
            if (whatCancel == "1" && CurrentUser.ticketList.Count > 0)
            {

                Console.Clear();

                //Dit geeft een overzicht van alle filmorders van de user in het juiste format.
                CurrentUser.CheckOrder(1);

                //Aan de hand van de ordernummers kan de user het de te cancellen order kiezen.
                Console.WriteLine("Please select the order you want to cancel: ");
                int cancelOrderMovie;
                int zaalToRemoveReservation = 0;
                string orderCancelled = Console.ReadLine();

                //Dit checkt of de input een getal is of niet.
                bool succesful = int.TryParse(orderCancelled, out cancelOrderMovie);

                //Zolang de user input geen echt getal is, of de input is geen geldig ordernummer, wordt er opnieuw om user input gevraagd.
                while (!(succesful) || (cancelOrderMovie < 0 || cancelOrderMovie >= CurrentUser.ticketList.Count))
                {
                    Console.Clear();
                    Console.WriteLine("Please select the order you want to cancel: ");
                    orderCancelled = Console.ReadLine();
                    succesful = int.TryParse(orderCancelled, out cancelOrderMovie);
                }

                //Dit checkt in welke zaal de te cancellen stoelen zich bevinden.
                for(int i = 0; i < movieList.Length; i++)
                {
                    if(CurrentUser.ticketList[cancelOrderMovie].ChosenMovie.movieName == movieList[i].movieName)
                    {
                        zaalToRemoveReservation = i;
                    }
                }

                //Deze loop zet van alle stoelen van de betreffende order de beschikbaarheid weer op true.
                for(int j = 0; j < CurrentUser.ticketList[cancelOrderMovie].howManyOfMovie; j++)
                {
                    Zalen[zaalToRemoveReservation].arr[CurrentUser.ticketList[cancelOrderMovie].RowOfSeats][CurrentUser.ticketList[cancelOrderMovie].ChosenChairsTuple[j].Item2].isAvailable = true; // Ingewikkeldste Line lol
                }

                //Dit haalt de order uit de orderlijst.
                CurrentUser.ticketList.RemoveAt(cancelOrderMovie);
                
                Console.WriteLine("Order cancelled");
                Console.ReadLine();
            }

            //Code voor het cancellen van een order van drankjes.
            else if (whatCancel == "2" && CurrentUser.orderList.drinks.Count > 0)
            {
                Console.Clear();

                //Dit laat zien welke drankjes er allemaal zijn besteld.
                CurrentUser.CheckOrder(2);

                //Dit vraagt om de user input van welke drankjes order er gecancelled moet worden.
                Console.WriteLine("Please select the order you want to cancel: ");
                int cancelOrderMovie;
                string orderCancelled = Console.ReadLine();

                //Dit checkt of de user input een getal is.
                bool succesful = int.TryParse(orderCancelled, out cancelOrderMovie);

                //Zolang de user input geen getal is, of de user input is geen geldig ordernummer, wordt er opnieuw om input gevraagd.
                while (!(succesful) || (cancelOrderMovie < 0 || cancelOrderMovie >= CurrentUser.orderList.drinks.Count))
                {
                    Console.Clear();
                    Console.WriteLine("Please select the order you want to cancel: ");
                    orderCancelled = Console.ReadLine();
                    succesful = int.TryParse(orderCancelled, out cancelOrderMovie);
                }

                //Dit verwijdert de specifieke order van de orderlijst.
                CurrentUser.orderList.drinks.RemoveAt(cancelOrderMovie);


                Console.WriteLine("Order cancelled");
                Console.ReadLine();
            }

            //Code voor het cancellen van een order van snacks.
            else if (whatCancel == "3" && CurrentUser.orderList.food.Count > 0)
            {
                Console.Clear();

                //Dit laat zien welke drankjes er allemaal zijn besteld.
                CurrentUser.CheckOrder(3);
                Console.WriteLine("Please select the order you want to cancel: ");

                //Dit vraagt om de user input van welke drankjes order er gecancelled moet worden.
                int cancelOrderMovie;
                string orderCancelled = Console.ReadLine();

                //Dit checkt of de user input een getal is.
                bool succesful = int.TryParse(orderCancelled, out cancelOrderMovie);

                //Zolang de user input geen getal is, of de user input is geen geldig ordernummer, wordt er opnieuw om input gevraagd.
                while (!succesful || (cancelOrderMovie < 0 || cancelOrderMovie >= CurrentUser.orderList.food.Count))
                {
                    Console.Clear();
                    Console.WriteLine("Please select the order you want to cancel: ");
                    orderCancelled = Console.ReadLine();
                    succesful = int.TryParse(orderCancelled, out cancelOrderMovie);
                }

                //Dit verwijdert de specifieke order van de orderlijst.
                CurrentUser.orderList.food.RemoveAt(cancelOrderMovie);


                Console.WriteLine("Order cancelled");
                Console.ReadLine();
            }

            //Code voor als de gebruiker ervoor kiest om naar het hoofdmenu terug te gaan.
            else
            {
                Console.Clear();
                Console.WriteLine("Returning to main menu");
                Console.ReadLine();
                
            }
        }
    }
}