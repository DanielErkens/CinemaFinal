using System;
using System.Collections.Generic;


public class logIn
{
    //Deze code is voor het inloggen en registreren van users.
    public static User logInUser(List<User> toCheck)
    {
        //Dit is het eerste scherm wat de user ziet. Hier wordt gevraagd of de user een nieuw account wil registreren, of dat de user wil inloggen.
        Console.WriteLine("Hello, welcome to Dutch Cinemas. This is our reservation system.");
        Console.WriteLine("Press 1 to log in");
        Console.WriteLine("Press 2 to register your new account");

        //Hier wordt de user input gegeven voor de actie.
        string choice = Console.ReadLine();

        //Zolang de user geen goede input geeft, wordt er opnieuw om user input gevraagd.
        while(choice != "1" && choice != "2")
        {
            Console.Clear();
            Console.WriteLine("Hello, welcome to Dutch Cinemas. This is our reservation system.");
            Console.WriteLine("Press 1 to log in");
            Console.WriteLine("Press 2 to register your new account");
            choice = Console.ReadLine();
        }

        //Code voor het inloggen van een user.
        if(choice == "1")
        {
            //Deze bool bepaalt of het inloggen succesvol is.
            bool logInAccepted = false;

            //Zolang er niet succesvol word ingelogd, wordt er gevraagd of de user opnieuw de username en password wil opgeven.
            while (!logInAccepted)
            {
                //Dit is de code voor het overzicht.
                Console.Clear();
                Console.WriteLine("Hello, welcome to Dutch Cinema's. In order to access our menu, please log in");
                Console.ReadLine();
                
                //Hier wordt gevraagd om de username.
                Console.WriteLine("Please enter your username:");
                string username = Console.ReadLine();
                string lowercaseusername = username.ToLower();

                //Hier wordt gevraagd om het password.
                Console.WriteLine("Please enter your password:");
                string password = Console.ReadLine();
                string lowercasepassword = password.ToLower();

                //Hier wordt er door de hele lijst met alle users geïtereerd.
                foreach (User person in toCheck)
                {
                    //Zodra de opgegeven username en password overeenkomen met die van een user in de database, is de inlog succesvol.
                    if (lowercaseusername == person.username && lowercasepassword == person.password)
                    {
                        logInAccepted = true;
                    }

                    //Dit geeft de confirmatie dat het inloggen succesvol was.
                    if (logInAccepted)
                    {
                        Console.WriteLine("You have successfully logged in !!!");
                        Console.ReadLine();
                        Console.Clear();
                        return person;
                    }
                }

                //Als de opgegeven username en password met geen enkele user in de database overeenkomt,
                //Wordt er gevraagd of de user opnieuw wil inloggen of wil exiten.
                Console.Clear();
                Console.WriteLine("Your username and password are incorrect.");
                Console.WriteLine("Press 1 to log in again");
                Console.WriteLine("Press 2 to exit the program");

                //Dit is de user input van wat de user wil doen.
                string whatToDo = Console.ReadLine();

                //Zolang de user geen correcte input geeft, wordt er opnieuw om input gevraagd.
                while(whatToDo != "1" && whatToDo != "2")
                {
                    Console.Clear();
                    Console.WriteLine("You provided an insufficient input.");
                    Console.WriteLine("Press 1 to log in again");
                    Console.WriteLine("Press 2 to exit the program");
                    whatToDo = Console.ReadLine();
                }
                Console.Clear();
            }
        }

        //Dit is de code voor het registreren van een nieuwe gebruiker.
        else
        {
            Console.Clear();
            int useless;

            //Hier wordt er gevraagd om de username van de nieuwe gebruiker.
            Console.WriteLine("Welcome new user! To create your account, Please enter your username");
            string newusername = Console.ReadLine();

            //Zolang de user een lege string geeft als username, wordt er opnieuw om een username gevraagd.
            while(newusername == "")
            {
                Console.Clear();
                Console.WriteLine("Welcome new user! To create your account, Please enter your username (no empty string)");
                newusername = Console.ReadLine();
            }

            //Hier wordt er gevraagd om de username van de nieuwe gebruiker.
            Console.WriteLine("Now enter Your password ");
            string newpassword = Console.ReadLine();

            //Zolang de user een lege string geeft als password, wordt er opnieuw om een password gevraagd.
            while (newpassword == "")
            {
                Console.WriteLine("Now enter Your password (no empty string)");
                newpassword = Console.ReadLine();
            }

            //Hier wordt er om de code gevraagd van de user, waarmee de user alles betaalt.
            Console.WriteLine("And at last, please enter your Payment Code (numbers only)");
            string newpaymentcode = Console.ReadLine();

            //Dit checkt of de user input ook echt een getal is.
            bool succesful = int.TryParse(newpaymentcode, out useless);

            //Zolang de user input geen getal is, wordt er opnieuw gevraagd om een paymentcode.
            while(!succesful)
            {
                Console.WriteLine("And at last, please enter your Payment Code (numbers only)");
                newpaymentcode = Console.ReadLine();
                succesful = int.TryParse(newpaymentcode, out useless);
            }

            //Met alle gegevens die we hebben gekregen, voegen we de nieuwe user toe aan de database.
            User CurrentUser = new User(newusername, newpassword, newpaymentcode);
            toCheck.Add(CurrentUser);
            Console.WriteLine("Registration succesfull!");
            Console.ReadLine();
            return CurrentUser;
            
        }
        return null;
    }
}
