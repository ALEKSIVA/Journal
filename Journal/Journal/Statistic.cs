using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Journal
{
    [Table("Statistics")]
    public class Statistic
    {
        public static List<int> grades = new List<int>();
        public static int? debtAmount;
        public static int? lastGrade;
        public static int? lastGradeDate;
        public static int? LessonsCount;
        public static double? GradeAverage;
        public static void getAverage()
        {
            int countgrades = 0;
            if(grades.Count!=0)
            {
                foreach (var grd in grades)
                {
                    if(grd.ToString().Length!=0)
                    {
                        countgrades += grd;
                    }
                }
                GradeAverage = countgrades / grades.Count;
            }
        }
    }
}
