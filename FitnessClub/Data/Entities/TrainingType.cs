using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Entities
{
    public class TrainingType
    {
        public int Id { get; set; }
        [DisplayName("Type of training")]
        public string Name { get; set; }
        List<Training> Trainings { get; set; } //тренировки данного типа
    }
}
