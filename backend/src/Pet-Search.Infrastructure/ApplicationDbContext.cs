﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pet_Search.Domain.Entities.PetContext;
using Pet_Search.Domain.Entities.SpeciesContext;
using Pet_Search.Domain.Entities.VolunteerContext;

namespace Pet_Search.Infrastructure;

public class ApplicationDbContext : DbContext
{
	private const string DATABASENAME = "DatabasePetSearch";
	private readonly IConfiguration _configuration;
	public DbSet<Volunteer> Volunteers { get; set; }
	public DbSet<Species> Species { get; set; }

	public ApplicationDbContext(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(_configuration.GetConnectionString(DATABASENAME));
		optionsBuilder.UseSnakeCaseNamingConvention();
		optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
	}

	private ILoggerFactory CreateLoggerFactory() =>
		LoggerFactory.Create(builder =>
		{
			builder.AddConsole();
		});
}
