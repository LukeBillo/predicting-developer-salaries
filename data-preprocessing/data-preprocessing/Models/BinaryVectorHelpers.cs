using System;
using System.Collections.Generic;
using DataPreprocessing.Models.Enums;

namespace DataPreprocessing.Models
{
    public static class BinaryVectorHelpers
    {
        public static List<bool> CreateBinaryVector(Country country)
        {
            // gets list of enums and initialises a new
            // list of bools (representing 0 & 1)
            // initialised to false in all positions.
            var enumList = Enum.GetValues(typeof(Country));
            var binaryVector = new List<bool>(new bool[enumList.Length]);

            // countries are just 0..n so cast to int
            // should return their index in enumList array.
            var countryIndex = (int) country;
            binaryVector[countryIndex] = true;

            return binaryVector;
        }

        public static List<bool> CreateBinaryVector(StudentStatus status)
        {
            var enumList = Enum.GetValues(typeof(StudentStatus));
            var binaryVector = new List<bool>(new bool[enumList.Length]);

            var statusIndex = (int)status;
            binaryVector[statusIndex] = true;

            return binaryVector;
        }

        public static List<bool> CreateBinaryVector(EmploymentStatus status)
        {
            var enumList = Enum.GetValues(typeof(EmploymentStatus));
            var binaryVector = new List<bool>(new bool[enumList.Length]);

            var statusIndex = (int)status;
            binaryVector[statusIndex] = true;

            return binaryVector;
        }

        public static List<bool> CreateBinaryVector(EducationLevel level)
        {
            var enumList = Enum.GetValues(typeof(EducationLevel));
            var binaryVector = new List<bool>(new bool[enumList.Length]);

            var levelIndex = (int)level;
            binaryVector[levelIndex] = true;

            return binaryVector;
        }

        public static List<bool> CreateBinaryVector(UndergraduateMajor major)
        {
            var enumList = Enum.GetValues(typeof(Country));
            var binaryVector = new List<bool>(new bool[enumList.Length]);

            var majorIndex = (int)major;
            binaryVector[majorIndex] = true;

            return binaryVector;
        }

        public static List<bool> CreateBinaryVector(List<DevelopmentType> devTypes)
        {
            var enumList = Enum.GetValues(typeof(Country));
            var binaryVector = new List<bool>(new bool[enumList.Length]);

            foreach (var developmentType in devTypes)
            {
                var devTypeIndex = (int)developmentType;
                binaryVector[devTypeIndex] = true;
            }

            return binaryVector;
        }
    }
}
