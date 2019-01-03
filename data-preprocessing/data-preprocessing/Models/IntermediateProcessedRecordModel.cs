using System.Collections.Generic;
using DataPreprocessing.Models.Enums;

namespace DataPreprocessing.Models
{
    public class IntermediateProcessedRecordModel
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        public StudentStatus StudentStatus { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public EducationLevel EducationLevel { get; set; }
        public UndergraduateMajor UndergraduateMajor { get; set; }
        public List<DevelopmentType> DevelopmentTypes { get; set; }
        public YearBand YearsCoding { get; set; }
        public YearBand YearsProfessionalCoding { get; set; }
        public decimal Salary { get; set; }
        public bool HasAdditionalEducation { get; set; }
    }
}
