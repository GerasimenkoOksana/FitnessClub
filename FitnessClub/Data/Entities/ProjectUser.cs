using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Entities
{
    public enum Sex  {male, female }
    public class ProjectUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime Moment { get; set; } //дата регистрации
        public Sex Sex { get; set; }

        List<Training>TrainingsClient { get; set; } //тренировки как клиента

        List<Training> TrainingsTrainer { get; set; } //тренировки как тренера - доступно для роли тренер
        List<Sport> Sports { get; set; }  //виды спорта, которые может вести тренер - доступно для роли тренер
        List<Time> FreeTimeTable { get; set; } //доступное время у тренера - доступно для роли тренер
    }
}
