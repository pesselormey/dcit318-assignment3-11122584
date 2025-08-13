using System;
using System.IO;
using System.Collections.Generic;
using SchoolGrading.Exceptions;
using SchoolGrading.Services;

namespace SchoolGrading
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Provide paths via args or use defaults
            string inputPath  = args.Length > 0 ? args[0] : "students_input.txt";
            string outputPath = args.Length > 1 ? args[1] : "students_report.txt";

            try
            {
                var processor = new StudentResultProcessor();

                // Read and validate
                List<Models.Student> students = processor.ReadStudentsFromFile(inputPath);

                // Write formatted report
                processor.WriteReportToFile(students, outputPath);

                Console.WriteLine($"Report written to: {outputPath}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"[FileNotFound] {ex.Message}");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($"[InvalidScoreFormat] {ex.Message}");
            }
            catch (SchoolGrading.Exceptions.MissingFieldException ex)
            {
                Console.WriteLine($"[MissingField] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Unexpected] {ex.Message}");
            }
        }
    }
}
