using FitnessClub.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessClub.Data{
   
    public class ApplicationDbContext : IdentityDbContext<ProjectUser>
    {
        public DbSet<TrainingHall> TrainingHalls { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
