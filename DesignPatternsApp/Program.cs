using System;
using System.Collections.Generic;
using DesignPatternsApp.Mediator;
using DesignPatternsApp.Memento;

namespace DesignPatternsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Mediator();
            Memento();
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

        private static void Memento()
        {
            // Memento pattern is used to save and restore the state of an object without affecting its internal structure.
            // In the example below we assume that in our application we have Loan Application form where borrower
            // have to fill a lot of data and we don't want to loose the data when borrower closes the form for example.
            // So we need to save the state of the form and restore it for example when user presses Ctrl + Z.
            
            var loanForm = new LoanInformationForm();
            var snapshots = new Stack<LoanInformationSnapshot> {};

            snapshots.Push(loanForm.CreateSnapshot());
            // user filling in the form:
            loanForm.BorrowerName = "John Doe";
            // After each field we can create a snapshot and save it to the stack.
            snapshots.Push(loanForm.CreateSnapshot());
            // repeat for each form field..
            loanForm.BorrowerAddress = "California, Irvine, Random Street, 123";
            snapshots.Push(loanForm.CreateSnapshot());
            loanForm.CreditRating = 700;
            snapshots.Push(loanForm.CreateSnapshot());
            loanForm.LoanAmount = 431_200;
            snapshots.Push(loanForm.CreateSnapshot());
            loanForm.LoanAmount = 331_200;
            
            // at this if we want to restore the form to the state before the last change (e.g borrower presses Ctrl + Z)
            var latestState = snapshots.Pop();
            latestState.Restore();
            // Now in loanForm.LoanAmount is restored back to 431_200
            
            Console.WriteLine(latestState.LoanAmount);
        }
    }
}