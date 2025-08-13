using HealthcareSystem.App;

namespace HealthcareSystem
{
    public static class Program
    {
        public static void Main()
        {
            var app = new HealthSystemApp();

            app.SeedData();                 // ii
            app.BuildPrescriptionMap();     // iii
            app.PrintAllPatients();         // iv

            int selectedPatientId = 2;      // v
            app.PrintPrescriptionsForPatient(selectedPatientId);

            app.DemoRepositoryOps();        // optional demo
        }
    }
}
