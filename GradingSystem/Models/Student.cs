#nullable enable
using System;

namespace SchoolGrading.Models
{
    public class Student
    {
        public int Id { get; }
        public string FullName { get; }
        public int Score { get; }

        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Score = score;
        }

        public string GetGrade()
        {
            return Score switch
            {
                >= 80 and <= 100 => "A",
                >= 70 and <= 79  => "B",
                >= 60 and <= 69  => "C",
                >= 50 and <= 59  => "D",
                < 50             => "F",
                _                => "F"
            };
        }

        public override string ToString() =>
            $"{FullName} (ID: {Id}): Score = {Score}, Grade = {GetGrade()}";
    }
}
