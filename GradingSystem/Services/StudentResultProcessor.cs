#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using SchoolGrading.Exceptions;
using SchoolGrading.Models;

namespace SchoolGrading.Services
{
    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();

            using var reader = new StreamReader(inputFilePath);
            string? line;
            int lineNumber = 0;

            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip blank lines

                var parts = line.Split(',', StringSplitOptions.TrimEntries);
                if (parts.Length < 3)
                    throw new SchoolGrading.Exceptions.MissingFieldException(
                        $"Line {lineNumber}: expected 3 fields (Id, FullName, Score) but found {parts.Length}.");

                string idRaw = parts[0];
                string nameRaw = parts[1];
                string scoreRaw = parts[2];

                if (string.IsNullOrWhiteSpace(nameRaw))
                    throw new SchoolGrading.Exceptions.MissingFieldException($"Line {lineNumber}: FullName is missing.");

                if (!int.TryParse(idRaw, out int id))
                    throw new FormatException($"Line {lineNumber}: invalid Id '{idRaw}' (must be an integer).");

                if (!int.TryParse(scoreRaw, out int score))
                    throw new InvalidScoreFormatException(
                        $"Line {lineNumber}: invalid Score '{scoreRaw}' (must be an integer).");

                students.Add(new Student(id, nameRaw, score));
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using var writer = new StreamWriter(outputFilePath, false);
            foreach (var s in students)
            {
                writer.WriteLine($"{s.FullName} (ID: {s.Id}): Score = {s.Score}, Grade = {s.GetGrade()}");
            }
        }
    }
}
