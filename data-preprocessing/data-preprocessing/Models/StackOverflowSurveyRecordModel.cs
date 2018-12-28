namespace DataPreprocessing.Models
{
    public class StackOverflowSurveyRecordModel
    {
        public int Respondent { get; set; }
        public string Hobby { get; set; }
        public string OpenSource { get; set; }
        public string Country { get; set; }
        public string Student { get; set; }
        public string Employment { get; set; }
        public string FormalEducation { get; set; }
        public string UndergradMajor { get; set; }
        public string CompanySize { get; set; }
        public string DevType { get; set; }
        public string YearsCoding { get; set; }
        public string YearsCodingProf { get; set; }
        public string JobSatisfaction { get; set; }
        public string CareerSatisfaction { get; set; }
        public string HopeFiveYears { get; set; }
        public string JobSearchStatus { get; set; }
        public string LastNewJob { get; set; }
        public string AssessJob1 { get; set; }
        public string AssessJob2 { get; set; }
        public string AssessJob3 { get; set; }
        public string AssessJob4 { get; set; }
        public string AssessJob5 { get; set; }
        public string AssessJob6 { get; set; }
        public string AssessJob7 { get; set; }
        public string AssessJob8 { get; set; }
        public string AssessJob9 { get; set; }
        public string AssessJob10 { get; set; }
        public string AssessBenefits1 { get; set; }
        public string AssessBenefits2 { get; set; }
        public string AssessBenefits3 { get; set; }
        public string AssessBenefits4 { get; set; }
        public string AssessBenefits5 { get; set; }
        public string AssessBenefits6 { get; set; }
        public string AssessBenefits7 { get; set; }
        public string AssessBenefits8 { get; set; }
        public string AssessBenefits9 { get; set; }
        public string AssessBenefits10 { get; set; }
        public string AssessBenefits11 { get; set; }
        public string JobContactPriorities1 { get; set; }
        public string JobContactPriorities2 { get; set; }
        public string JobContactPriorities3 { get; set; }
        public string JobContactPriorities4 { get; set; }
        public string JobContactPriorities5 { get; set; }
        public string JobEmailPriorities1 { get; set; }
        public string JobEmailPriorities2 { get; set; }
        public string JobEmailPriorities3 { get; set; }
        public string JobEmailPriorities4 { get; set; }
        public string JobEmailPriorities5 { get; set; }
        public string JobEmailPriorities6 { get; set; }
        public string JobEmailPriorities7 { get; set; }
        public string UpdateCV { get; set; }
        public string Currency { get; set; }
        public string Salary { get; set; }
        public string SalaryType { get; set; }
        public string ConvertedSalary { get; set; }
        public string CurrencySymbol { get; set; }
        public string CommunicationTools { get; set; }
        public string TimeFullyProductive { get; set; }
        public string EducationTypes { get; set; }
        public string SelfTaughtTypes { get; set; }
        public string TimeAfterBootcamp { get; set; }
        public string HackathonReasons { get; set; }
        public string AgreeDisagree1 { get; set; }
        public string AgreeDisagree2 { get; set; }
        public string AgreeDisagree3 { get; set; }
        public string LanguageWorkedWith { get; set; }
        public string LanguageDesireNextYear { get; set; }
        public string DatabaseWorkedWith { get; set; }
        public string DatabaseDesireNextYear { get; set; }
        public string PlatformWorkedWith { get; set; }
        public string PlatformDesireNextYear { get; set; }
        public string FrameworkWorkedWith { get; set; }
        public string FrameworkDesireNextYear { get; set; }
        public string IDE { get; set; }
        public string OperatingSystem { get; set; }
        public string NumberMonitors { get; set; }
        public string Methodology { get; set; }
        public string VersionControl { get; set; }
        public string CheckInCode { get; set; }
        public string AdBlocker { get; set; }
        public string AdBlockerDisable { get; set; }
        public string AdBlockerReasons { get; set; }
        public string AdsAgreeDisagree1 { get; set; }
        public string AdsAgreeDisagree2 { get; set; }
        public string AdsAgreeDisagree3 { get; set; }
        public string AdsActions { get; set; }
        public string AdsPriorities1 { get; set; }
        public string AdsPriorities2 { get; set; }
        public string AdsPriorities3 { get; set; }
        public string AdsPriorities4 { get; set; }
        public string AdsPriorities5 { get; set; }
        public string AdsPriorities6 { get; set; }
        public string AdsPriorities7 { get; set; }
        public string AIDangerous { get; set; }
        public string AIInteresting { get; set; }
        public string AIResponsible { get; set; }
        public string AIFuture { get; set; }
        public string EthicsChoice { get; set; }
        public string EthicsReport { get; set; }
        public string EthicsResponsible { get; set; }
        public string EthicalImplications { get; set; }
        public string StackOverflowRecommend { get; set; }
        public string StackOverflowVisit { get; set; }
        public string StackOverflowHasAccount { get; set; }
        public string StackOverflowParticipate { get; set; }
        public string StackOverflowJobs { get; set; }
        public string StackOverflowDevStory { get; set; }
        public string StackOverflowJobsRecommend { get; set; }
        public string StackOverflowConsiderMember { get; set; }
        public string HypotheticalTools1 { get; set; }
        public string HypotheticalTools2 { get; set; }
        public string HypotheticalTools3 { get; set; }
        public string HypotheticalTools4 { get; set; }
        public string HypotheticalTools5 { get; set; }
        public string WakeTime { get; set; }
        public string HoursComputer { get; set; }
        public string HoursOutside { get; set; }
        public string SkipMeals { get; set; }
        public string ErgonomicDevices { get; set; }
        public string Exercise { get; set; }
        public string Gender { get; set; }
        public string SexualOrientation { get; set; }
        public string EducationParents { get; set; }
        public string RaceEthnicity { get; set; }
        public string Age { get; set; }
        public string Dependents { get; set; }
        public string MilitaryUS { get; set; }
        public string SurveyTooLong { get; set; }
        public string SurveyEasy { get; set; }

        private bool HasUndergraduateDegree => FormalEducation.Contains("Bachelor") ||
                                               FormalEducation.Contains("Master") ||
                                               FormalEducation.Contains("Associate") ||
                                               FormalEducation.Contains("Professional") ||
                                               FormalEducation.Contains("doctoral");

        public bool HasValidCountry => Country != "NA";
        public bool HasValidStudentStatus => Student != "NA";
        public bool IsEmployed => Employment != "NA" && !Employment.Contains("Not employed");
        public bool HasValidFormalEducation => FormalEducation != "NA";
        public bool HasValidUndergradMajor => HasUndergraduateDegree && UndergradMajor != "NA";
        public bool HasValidSalary => ConvertedSalary != "NA";
        public bool HasValidYearsWorked => YearsCoding != "NA" && YearsCodingProf != "NA";
        public bool HasAdditionalEducation => EducationTypes != "NA";
    }
}