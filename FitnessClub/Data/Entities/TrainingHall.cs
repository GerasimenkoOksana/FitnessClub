using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Entities
{
    public class TrainingHall
    {
        public int Id { get; set; }
        [DisplayName("Hall")]
        public string Name{ get; set; }

        List<Training> Trainings { get; set; } //тренировки в данном зале
    }
}
