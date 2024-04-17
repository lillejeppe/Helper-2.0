using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper_2._0
{
    public class Validation
    {
        public bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            if (name.Length < 2 || name.Length > 15) return false;
            else if (ValidateIsLetters(name)) return true;
            return false;
        }
        public bool ValidatePhoneNumber(string nrTest)
        {
            if (int.TryParse(nrTest, out int nr) && nrTest.Length == 8)
            {
                return true;
            }
            else return false;
        }

        public bool ValidateEmail(string Gmail)
        {
            string[] split = Splitter(Gmail, '@');
            if (split.Length != 1)
            {
                if (split[0].Length < 2 || split[1].Length < 2 || !Gmail.Contains('.'))
                {
                    return false;
                }
                else return true;
            }
            return false;
        }

        public string[] Splitter(string input, char splitChar)
        {
            string[] split;
            if (char.IsPunctuation(splitChar) || char.IsSeparator(splitChar) || char.IsWhiteSpace(splitChar))
            {
                split = input.Split(splitChar);
                if (split.Length > 1)
                {
                    return input.Split(splitChar);
                }
                else throw new Exception("A Split did not occure");
            }
            else throw new Exception("Invalid split");
        }

        public bool ValidateIsInt(string IsInt)
        {
            try
            {
                int.Parse(IsInt);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ValidateIsLetters(string isLetter)
        {
            try
            {
                foreach (char c in isLetter)
                {
                    if (!char.IsLetter(c))
                    {
                        return false;
                    }
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class CsvFileManager
    {

        string path;
        public CsvFileManager(string path)
        {
            this.path = path;
        }

        public void Creator(string fileName, List<string> dataSet)
        {
            try
            {
                File.WriteAllLines(path + Formater(fileName), dataSet);
            }
            catch (DirectoryNotFoundException fileCreatorExceptionDirectory)
            {
                Console.WriteLine("ERROR: The diretory was missing: " + fileCreatorExceptionDirectory.Message);
            }
            catch (Exception fileCreatorExceptionAll)
            {
                Console.WriteLine("ERROR: " + fileCreatorExceptionAll.Message);
            }

        }

        public List<string> Reader(string fileName)
        {
            // return File.ReadAllLines(path + FileFormat(fileName)).ToList();
            try
            {
                return File.ReadAllLines(path + Formater(fileName)).ToList();
            }
            catch (DirectoryNotFoundException fileCallerExceptionDirectory)
            {
                Console.WriteLine("ERROR: The diretory was missing: " + fileCallerExceptionDirectory.Message);
            }
            catch (FileNotFoundException fileCallerExceptionFile)
            {
                Console.WriteLine("ERROR: The file was missing: " + fileCallerExceptionFile.Message);
            }
            catch (Exception fileCallerExceptionAll)
            {
                Console.WriteLine("ERROR: " + fileCallerExceptionAll.Message);
            }
            return new List<string>();
        }

        string Formater(string fileName)
        {
            return "\\" + fileName + ".csv";
        }

        public void Combiner(string fileName1, string fileName2, string newFileName)
        {
            List<string> combinedList = Reader(fileName1);
            combinedList.AddRange(Reader(fileName2));
            File.WriteAllLines(path + Formater(newFileName), combinedList);
        }
    }
}
