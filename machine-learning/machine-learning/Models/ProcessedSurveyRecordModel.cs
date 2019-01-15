using System.Collections.Generic;
using Benchmarks.Models.Enums;

namespace Benchmarks.Models
{
    public class ProcessedSurveyRecordModel
    {
        public int Id { get; set; }

        public IEnumerable<bool> Country { get; set; }
        public IEnumerable<bool> StudentStatus { get; set; }
        public IEnumerable<bool> EmploymentStatus { get; set; }
        public IEnumerable<bool> EducationLevel { get; set; }
        public IEnumerable<bool> UndergraduateMajor { get; set; }
        public IEnumerable<bool> DevelopmentTypes { get; set; }

        // below kept as before
        public YearBand YearsCoding { get; set; }
        public YearBand YearsProfessionalCoding { get; set; }
        public decimal Salary { get; set; }
        public bool HasAdditionalEducation { get; set; }
    }
}
