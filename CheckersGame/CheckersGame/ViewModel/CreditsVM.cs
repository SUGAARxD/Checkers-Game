using CheckersGame.Model;

namespace CheckersGame.ViewModel
{
    internal class CreditsVM
    {
        public CreditsVM()
        {

        }
        public CreditsVM(CustomSettings settings)
        {
            MySettings = settings;
            StudentName = "Frincu Marian";
            StudentGroup = "10LF222";
            StudentInstitutionalMail = "vasile.frincu@student.unitbv.ro";
        }

        #region Properties and members

        public CustomSettings MySettings { get; set; }
        public string StudentName { get; set; }
        public string StudentGroup { get; set; }
        public string StudentInstitutionalMail { get; set; }

        #endregion

    }
}
