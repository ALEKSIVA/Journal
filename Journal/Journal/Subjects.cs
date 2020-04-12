using System;
using SQLite;

namespace Journal
{
    [Table("Subject")]
    public class Subjects
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Subjectt { get; set; }
        public int Lessons { get; set; }
        public int Grade { get; set; }
        public DateTime dateGet { get; set; } = DateTime.Now;
        public DateTime dateToDo { get; set; } = DateTime.Now.AddDays(5);

    }
}
