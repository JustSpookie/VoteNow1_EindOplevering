#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP_View.Models;

namespace ASP_View.Data
{
    public class ASP_ViewContext : DbContext
    {
        public ASP_ViewContext (DbContextOptions<ASP_ViewContext> options)
            : base(options)
        {
        }

        public DbSet<ASP_View.Models.KandidaatViewModel> KandidaatViewModel { get; set; }

        public DbSet<ASP_View.Models.VerkiezingViewModel> VerkiezingViewModel { get; set; }

        public DbSet<ASP_View.Models.StemmenViewModel>? StemViewModel { get; set; }

        public DbSet<ASP_View.Models.UserModel>? UserModel { get; set; }

    }
}
