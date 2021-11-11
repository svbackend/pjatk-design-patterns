namespace DesignPatternsApp.Memento
{
    public interface IMementoSnapshot
    {
        public void Restore();
    }
    
    public interface IMemento<SnapshotType>
    {
        public SnapshotType CreateSnapshot();
    }

    public class LoanInformationForm : IMemento<LoanInformationSnapshot>
    {
        public string BorrowerName { get; set; }
        public string BorrowerAddress { get; set; }
        public int LoanAmount { get; set; }
        public int CreditRating { get; set; }

        public LoanInformationSnapshot CreateSnapshot()
        {
            return new LoanInformationSnapshot()
            {
                LoanInformationForm = this,
                BorrowerName = BorrowerName,
                BorrowerAddress = BorrowerAddress,
                LoanAmount = LoanAmount,
                CreditRating = CreditRating
            };
        }
    }

    public class LoanInformationSnapshot : IMementoSnapshot
    {
        public LoanInformationForm LoanInformationForm { get; set; }
        public string BorrowerName { get; set; }
        public string BorrowerAddress { get; set; }
        public int LoanAmount { get; set; }
        public int CreditRating { get; set; }
        
        public void Restore()
        {
            LoanInformationForm.BorrowerName = BorrowerName;
            LoanInformationForm.BorrowerAddress = BorrowerAddress;
            LoanInformationForm.LoanAmount = LoanAmount;
            LoanInformationForm.CreditRating = CreditRating;
        }
    }

    // example of Memento pattern
    public class Memento
    {
    }
}