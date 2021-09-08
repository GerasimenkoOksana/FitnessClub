using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Entities
{
    public class Training
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int SportId { get; set; }
        public Sport Sport { get; set; } //вид сопрта

        public int TrainerId { get; set; }
        public ProjectUser Trainer { get; set; }  //тренер

        public int HallId { get; set; }
        public TrainingHall Hall { get; set; }  //зал

        public int TrainingTypeId { get; set; }
        public TrainingType TrainingType { get; set; }  //тип тренировки (индивид, группов)

        List<Time>TimeTable { get; set; }  //расписание данной тренировки
        List<ProjectUser> Clients { get; set; } //клиенты, записанный на данную тренировку
    }
}
