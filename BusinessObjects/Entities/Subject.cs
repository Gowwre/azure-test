namespace BusinessObjects.Entities;

public class Subject
{
    public string SubjectId { get; set; } = null!;

    public string SubjectName { get; set; } = null!;

    public int Semester { get; set; }

    public string TeacherEmail { get; set; } = null!;

    public virtual Account TeacherEmailNavigation { get; set; } = null!;
}