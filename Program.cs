using Microsoft.AspNetCore;
using VeriFacil;

var builder = WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>().Build();

builder.Run();
