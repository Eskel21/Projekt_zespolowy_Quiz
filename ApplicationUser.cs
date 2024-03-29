﻿using Microsoft.AspNetCore.Identity;
namespace Projekt_Quizy
{
    public class ApplicationUser: IdentityUser
    {
       public string Name { get; set; }
        public string Surname { get; set; }
        public byte[]? Picture { get; set; }

        public int Points { get; set; }
        public int PointsOverall { get; set; }
    }
}
