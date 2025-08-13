#nullable enable
using System;
using System.Collections.Generic;
using HealthcareSystem.Data;
using HealthcareSystem.Models;

namespace HealthcareSystem.App
{
    public class HealthSystemApp
    {
        // g) Fields
        private readonly Repository<Patient> _patientRepo = new();
        private readonly Repository<Prescription> _prescriptionRepo = new();
        private readonly Dictionary<int, List<Prescription>> _prescriptionMap = new();

        // d/e/f) Retrieval from dictionary map
        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            return _prescriptionMap.TryGetValue(patientId, out var list)
                ? new List<Prescription>(list)
                : new List<Prescription>();
        }

        // g) Methods
        public void SeedData()
        {
            // Patients (2–3)
            _patientRepo.Add(new Patient(1, "Ama Mensah", 32, "Female"));
            _patientRepo.Add(new Patient(2, "Kwame Boateng", 45, "Male"));
            _patientRepo.Add(new Patient(3, "Efua Adjei", 27, "Female"));

            // Prescriptions (4–5) with valid PatientIds
            _prescriptionRepo.Add(new Prescription(1001, 1, "Amoxicillin 500mg", DateTime.Today.AddDays(-7)));
            _prescriptionRepo.Add(new Prescription(1002, 1, "Ibuprofen 200mg", DateTime.Today.AddDays(-3)));
            _prescriptionRepo.Add(new Prescription(1003, 2, "Lisinopril 10mg", DateTime.Today.AddDays(-14)));
            _prescriptionRepo.Add(new Prescription(1004, 3, "Metformin 500mg", DateTime.Today.AddDays(-1)));
            _prescriptionRepo.Add(new Prescription(1005, 2, "Atorvastatin 20mg", DateTime.Today));
        }

        public void BuildPrescriptionMap()
        {
            _prescriptionMap.Clear();
            foreach (var rx in _prescriptionRepo.GetAll())
            {
                if (!_prescriptionMap.TryGetValue(rx.PatientId, out var list))
                {
                    list = new List<Prescription>();
                    _prescriptionMap[rx.PatientId] = list;
                }
                list.Add(rx);
            }
        }

        public void PrintAllPatients()
        {
            Console.WriteLine("=== Patients ===");
            foreach (var p in _patientRepo.GetAll())
                Console.WriteLine(p);
            Console.WriteLine();
        }

        public void PrintPrescriptionsForPatient(int id)
        {
            Console.WriteLine($"=== Prescriptions for PatientId {id} ===");
            var patient = _patientRepo.GetById(p => p.Id == id);
            if (patient is null)
            {
                Console.WriteLine("No such patient.\n");
                return;
            }

            var rxs = GetPrescriptionsByPatientId(id);
            if (rxs.Count == 0)
            {
                Console.WriteLine("No prescriptions found.\n");
                return;
            }

            foreach (var rx in rxs)
                Console.WriteLine(rx);
            Console.WriteLine();
        }

        // Optional: show repository ops
        public void DemoRepositoryOps()
        {
            bool removed = _prescriptionRepo.Remove(r => r.Id == 9999);
            Console.WriteLine($"Attempted to remove Rx#9999: {(removed ? "Removed" : "Not found")}");
        }
    }
}
