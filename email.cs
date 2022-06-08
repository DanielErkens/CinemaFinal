using MailKit.Net.Smtp;
using MimeKit;
using System;


public class email
{
    //Deze code is voor het schrijven van de betalingsbevestiging van de mail.
    public static void writeEmail(string message)
    {
        //Hier wordt de informatie van de verzendende email gegeven.
        string FromAddress = "dutchcinemas2022@gmail.com";
        string FromAdressTitle = "Dutch Cinema's";


        //Hier wordt de informatie van de ontavngende email gegeven.
        string ToAddress = "cinemaapp.example.com@gmail.com";
        string ToAdressTitle = "CinemaTest";

        //Hier wordt de content van de mail gegeven.
        string Subject = "Payment Confirmation";
        string BodyContent = message;

        //Smtp Server 
        string SmtpServer = "smtp.gmail.com";
        //Smtp Port Number 
        int SmtpPortNumber = 587;

        //Hier wordt basisinfo meegegeven. Dit hoort bij de format die gebruikt wordt.
        var mimeMessage = new MimeMessage();
        mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
        mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
        mimeMessage.Subject = Subject;
        mimeMessage.Body = new TextPart("plain")
        {
            Text = BodyContent
        };

        //Hier wordt de mail verstuurd, met het wachtwoord van het verzendende mailadres als confirmatie.
        using (var client = new SmtpClient())
        {
            client.Connect(SmtpServer, SmtpPortNumber, false);
            client.Authenticate("dutchcinemas2022@gmail.com", "CinemaApp");
            client.Send(mimeMessage);
            client.Disconnect(true);
        }
    }
}

    
