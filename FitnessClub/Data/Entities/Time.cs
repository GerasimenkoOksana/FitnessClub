using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Entities
{
    public class Time
    {
        public int Id { get; set; }
        public string DayTime { get; set; }

        List<Training> Trainings { get; set; } //тренировки в данное время
        List<ProjectUser>Trainers { get; set; } //тренеры, работающие в данное время
    }
}
