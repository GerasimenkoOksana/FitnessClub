using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Entities
{
    public class Sport
    {
        public int Id { get; set; }
        [DisplayName("Sport")]
        public string Name { get; set; }

        List<Training> Trainings { get; set; }  //тренировки по данному виду спорта
        List<ProjectUser> Trainers { get; set; } //тренеры по данному виду спорта
    }
}
