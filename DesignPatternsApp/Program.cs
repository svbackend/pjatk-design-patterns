using System;
using DesignPatternsApp.Mediator;

namespace DesignPatternsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Mediator();
        }

        private static void Mediator()
        {
            // Idea of mediator - to allow communication between different objects without knowing about each other
            // Mediator pattern is used to decouple sender and receiver objects of a communication.
            // To demonstrate mediator pattern, I created a fake user registration process where different services can communicate with each other.
            
            // UserRegistrationService -> event: New Visitor Has been registered
            // EmailService catches "UserHasBeenRegistered" -> Sends welcome email
            // PhoneVerificationService catches "UserHasBeenRegistered" -> send confirmation code and save it to the database
            // User confirms phone number phoneVerificationService.VisitorConfirmsPhoneNumber called
            // UserRegistrationService -> confirms user phone number

            // The client code.
            var userRegistrationService = new UserRegistrationService();
            var emailService = new EmailService();
            var phoneVerificationService = new PhoneVerificationService();
            new UserRegistrationMediator(userRegistrationService, emailService, phoneVerificationService);

            Console.WriteLine("Client code triggets operation userRegistrationService.RegisterVisitor.");
            userRegistrationService.RegisterVisitor();

            Console.WriteLine();

            Console.WriteLine("Client code triggers operation phoneVerificationService.VisitorConfirmsPhoneNumber.");
            phoneVerificationService.VisitorConfirmsPhoneNumber();
        }
    }
}