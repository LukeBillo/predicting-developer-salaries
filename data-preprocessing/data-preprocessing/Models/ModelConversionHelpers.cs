using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using DataPreprocessing.Models.Enums;

namespace DataPreprocessing.Models
{
    // While I imagine there is a better way of
    // doing this, this was the quickest and
    // easiest way to throw stuff together.
    public static class ModelConversionHelpers
    {
        public static ProcessedSurveyRecordModel ProcessStackOverflowSurveyRecordModel(StackOverflowSurveyRecordModel model)
        {
            return new ProcessedSurveyRecordModel
            {
                Id = model.Respondent,
                Country = ParseCountryString(model.Country),
                StudentStatus = ParseStudentStatus(model.Student),
                EmploymentStatus = ParseEmploymentStatus(model.Employment),
                EducationLevel = ParseEducationLevel(model.FormalEducation),
                UndergraduateMajor = ParseUndergraduateMajor(model.UndergradMajor),
                DevelopmentTypes = ParseDevelopmentTypes(model.DevType),
                YearsCoding = ParseYearBand(model.YearsCoding),
                YearsProfessionalCoding = ParseYearBand(model.YearsCodingProf),
                Salary = decimal.Parse(model.ConvertedSalary, NumberStyles.Float),
                HasAdditionalEducation = model.HasAdditionalEducation
            };
        }

        private static YearBand ParseYearBand(string yearBand)
        {
            switch (yearBand)
            {
                case "0-2 years":
                    return YearBand.ZeroToTwo;
                case "3-5 years":
                    return YearBand.ThreeToFive;
                case "6-8 years":
                    return YearBand.SixToEight;
                case "9-11 years":
                    return YearBand.NineToEleven;
                case "12-14 years":
                    return YearBand.TwelveToFourteen;
                case "15-17 years":
                    return YearBand.FifteenToSeventeen;
                case "18-20 years":
                    return YearBand.EighteenToTwenty;
                case "21-23 years":
                    return YearBand.TwentyOneToTwentyThree;
                case "24-26 years":
                    return YearBand.TwentyFourToTwentySix;
                case "27-29 years":
                    return YearBand.TwentySevenToTwentyNine;
                case "30 or more years":
                    return YearBand.ThirtyPlus;
                default:
                    throw new ArgumentException("Unknown year band.");
            }
        }

        private static List<DevelopmentType> ParseDevelopmentTypes(string developmentType)
        {
            var developmentTypes = developmentType.Split(',');
            var devTypesList = new List<DevelopmentType>();

            var unknowns = new List<string>();

            foreach (var type in developmentTypes)
            {
                var trimmedType = type.Replace(" ", string.Empty).Replace("-", string.Empty);

                try
                {
                    var devTypeParsed = Enum.Parse<DevelopmentType>(trimmedType, true);
                    devTypesList.Add(devTypeParsed);
                }
                catch (ArgumentException)
                {
                    unknowns.Add(trimmedType);
                }
            }

            return devTypesList;
        }

        private static UndergraduateMajor ParseUndergraduateMajor(string undergradMajor)
        {
            if (undergradMajor.Contains("business"))
            {
                return UndergraduateMajor.Business;
            }

            if (undergradMajor.Contains("health"))
            {
                return UndergraduateMajor.HealthScience;
            }

            if (undergradMajor.Contains("humanities"))
            {
                return UndergraduateMajor.Humanities;
            }

            if (undergradMajor.Contains("natural"))
            {
                return UndergraduateMajor.NaturalScience;
            }

            if (undergradMajor.Contains("social"))
            {
                return UndergraduateMajor.SocialScience;
            }

            if (undergradMajor.Contains("Another"))
            {
                return UndergraduateMajor.Engineering;
            }

            if (undergradMajor.Contains("Computer"))
            {
                return UndergraduateMajor.ComputerScience;
            }

            if (undergradMajor.Contains("arts"))
            {
                return UndergraduateMajor.Arts;
            }

            if (undergradMajor.Contains("never declared"))
            {
                return UndergraduateMajor.Undeclared;
            }

            if (undergradMajor.Contains("Information"))
            {
                return UndergraduateMajor.Information;
            }

            if (undergradMajor.Contains("Mathematics"))
            {
                return UndergraduateMajor.Maths;
            }

            if (undergradMajor.Contains("NA"))
            {
                return UndergraduateMajor.None;
            }

            if (undergradMajor.Contains("Web"))
            {
                return UndergraduateMajor.Web;
            }

            throw new ArgumentException("Unknown degree.");
        }

        private static EducationLevel ParseEducationLevel(string educationLevel)
        {
            if (educationLevel.Contains("never completed"))
                return EducationLevel.None;

            if (educationLevel.Contains("Primary"))
                return EducationLevel.Primary;

            if (educationLevel.Contains("Secondary"))
                return EducationLevel.Secondary;

            if (educationLevel.Contains("college/university"))
                return EducationLevel.Foundation;

            if (educationLevel.Contains("Associate"))
                return EducationLevel.Associate;

            if (educationLevel.Contains("Professional"))
                return EducationLevel.Professional;

            if (educationLevel.Contains("Bachelor"))
                return EducationLevel.Bachelors;

            if (educationLevel.Contains("Master"))
                return EducationLevel.Masters;

            if (educationLevel.Contains("doctoral"))
                return EducationLevel.Doctoral;

            throw new ArgumentException("Unknown education level.");
        }

        private static EmploymentStatus ParseEmploymentStatus(string employmentStatus)
        {
            switch (employmentStatus)
            {
                case "Employed part-time":
                    return EmploymentStatus.PartTime;
                case "Employed full-time":
                    return EmploymentStatus.FullTime;
                case "Independent contractor, freelancer, or self-employed":
                    return EmploymentStatus.Independent;
                case "Retired":
                    return EmploymentStatus.Retired;
                default:
                    throw new ArgumentException("Unknown employment status.");
            }
        }

        private static StudentStatus ParseStudentStatus(string studentStatus)
        {
            switch (studentStatus)
            {
                case "No":
                    return StudentStatus.No;
                case "Yes, full-time":
                    return StudentStatus.FullTime;
                case "Yes, part-time":
                    return StudentStatus.PartTime;
                default:
                    throw new ArgumentException("Unknown student status.");
            }
        }

        private static Country ParseCountryString(string country)
        {
            var trimmedCountry = country.Replace(" ", string.Empty);

            try
            {
                return Enum.Parse<Country>(trimmedCountry, true);
            }
            catch (ArgumentException)
            {
                // could not parse, so one of the following..
                switch (country)
                {
                    case "Iran, Islamic Republic of...":
                        return Country.IslamicRepublicOfIran;
                    case "Hong Kong (S.A.R.)":
                        return Country.HongKongSar;
                    case "Venezuela, Bolivarian Republic of...":
                        return Country.BolivarianRepublicOfVenezuela;
                    case "Other Country (Not Listed Above)":
                        return Country.OtherCountry;
                    case "Micronesia, Federated States of...":
                        return Country.FederatedStatesOfMicronesia;
                    case "Côte d'Ivoire":
                        return Country.CoteDIvoire;
                    case "Congo, Republic of the...":
                        return Country.DemocraticRepublicOfTheCongo;
                    case "Timor-Leste":
                        return Country.TimorLeste;
                    case "Democratic People's Republic of Korea":
                        return Country.DemocraticPeoplesRepublicOfKorea;
                    case "Guinea-Bissau":
                        return Country.GuineaBissau;
                    default:
                        throw new ArgumentException($"Country parsing failed for {country}.");
                }
            }
        }
    }
}
