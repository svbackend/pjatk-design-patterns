using System;

namespace DesignPatternsApp.Mediator
{
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }

    public interface IMediatorUser
    {
        void SetMediator(IMediator mediator);
    }

    // Concrete Mediators implement cooperative behavior by coordinating several
    // components.
    class UserRegistrationMediator : IMediator
    {
        private UserRegistrationService _userRegistrationService;
        private EmailService _emailService;
        private PhoneVerificationService _phoneVerificationService;

        public UserRegistrationMediator(
            UserRegistrationService userRegistrationService,
            EmailService emailService,
            PhoneVerificationService phoneVerificationService
        )
        {
            this._userRegistrationService = userRegistrationService;
            this._userRegistrationService.SetMediator(this);
            this._emailService = emailService;
            this._emailService.SetMediator(this);
            this._phoneVerificationService = phoneVerificationService;
            this._phoneVerificationService.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == UserRegistrationService.EventUserRegistered)
            {
                Console.WriteLine("Mediator reacts on EventUserRegistered and triggers folowing operations:");
                this._emailService.SendWelcomeEmail();
            }

            if (ev == PhoneVerificationService.EventPhoneNumberConfirmed)
            {
                Console.WriteLine("Mediator reacts on EventPhoneNumberConfirmed and triggers following operations:");
                this._userRegistrationService.ConfirmPhoneNumber();
            }
        }
    }

    abstract class AbstractService : IMediatorUser
    {
        protected IMediator _mediator;

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    // Concrete Components implement various functionality. They don't depend on
    // other components. They also don't depend on any concrete mediator
    // classes.
    class UserRegistrationService : AbstractService
    {
        public const string EventUserRegistered = "UserRegistered";

        public void RegisterVisitor()
        {
            Console.WriteLine("UserRegistrationService just registered new visitor");
            this._mediator.Notify(this, EventUserRegistered);
        }

        public void ConfirmPhoneNumber()
        {
            Console.WriteLine(
                "UserRegistrationService::ConfirmPhoneNumber - service marks user's number as confirmed/verified");
        }
    }

    class EmailService : AbstractService
    {
        public void SendWelcomeEmail()
        {
            Console.WriteLine("EmailService sends welcome email");
        }
    }

    class PhoneVerificationService : AbstractService
    {
        public const string EventPhoneNumberConfirmed = "PhoneNumberConfirmed";

        public void SendPhoneVerificationMessage()
        {
            Console.WriteLine(
                "PhoneVerificationService sends phone verification message and stores it to the database");
        }

        public void VisitorConfirmsPhoneNumber()
        {
            Console.WriteLine(
                "VisitorConfirmsPhoneNumber. PhoneVerificationService removes confirmation code from the db");
            this._mediator.Notify(this, EventPhoneNumberConfirmed);
        }
    }

    public class Mediator
    {
    }
}