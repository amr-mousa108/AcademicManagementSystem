namespace AcademicManagementSystem.ViewModel
{
    public class TraineeInCourse
    {
        public int TraineeId { get; set; }
        public string TraineeName { get; set; }
        public int CrsId { get; set; }
        public string CrsName { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }      // ← الدرجة الصغرى للنجاح
        public int MaxDegree { get; set; }      // ← الدرجة العظمى
        public string Color { get; set; }
        public string Status { get; set; }
        public double Percentage { get; set; }   // ← النسبة المئوية
        
        public int CrsResultId { get; set; }
    }
}