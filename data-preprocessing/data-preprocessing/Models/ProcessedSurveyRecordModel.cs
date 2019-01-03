using System.Collections.Generic;
using DataPreprocessing.Models.Enums;

namespace DataPreprocessing.Models
{
    public class ProcessedSurveyRecordModel
    {
        public ProcessedSurveyRecordModel(IntermediateProcessedRecordModel intermediateModel)
        {
            // these remain the same, so just set them
            Id = intermediateModel.Id;
            YearsCoding = intermediateModel.YearsCoding;
            YearsProfessionalCoding = intermediateModel.YearsProfessionalCoding;
            Salary = intermediateModel.Salary;
            HasAdditionalEducation = intermediateModel.HasAdditionalEducation;

            // below have been separated into functions for readability
            // in the constructor. They're horrible to read.
            Country = BinaryVectorHelpers.CreateBinaryVector(intermediateModel.Country);
            StudentStatus = BinaryVectorHelpers.CreateBinaryVector(intermediateModel.StudentStatus);
            EmploymentStatus = BinaryVectorHelpers.CreateBinaryVector(intermediateModel.EmploymentStatus);
            EducationLevel = BinaryVectorHelpers.CreateBinaryVector(intermediateModel.EducationLevel);
            UndergraduateMajor = BinaryVectorHelpers.CreateBinaryVector(intermediateModel.UndergraduateMajor);
            DevelopmentTypes = BinaryVectorHelpers.CreateBinaryVector(intermediateModel.DevelopmentTypes);
        }

        public int Id { get; set; }

        // below are all binary vector formats of the
        // values stored in IntermediateProcessedRecordModel
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
