﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    class DataContextFactory: IDesignTimeDbContextFactory<StackOverflowDataContext>
    {
            public StackOverflowDataContext CreateDbContext(string[] args)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}StackOverflow.Web"))
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

                return new StackOverflowDataContext(config.GetConnectionString("ConStr"));
            }
        }
}
