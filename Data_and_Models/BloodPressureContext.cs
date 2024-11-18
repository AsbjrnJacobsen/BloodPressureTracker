using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data_and_Models;

public class BloodPressureContext : DbContext
{
    public BloodPressureContext()
    { }
    
    public BloodPressureContext(DbContextOptions<BloodPressureContext> options) : base(options)
    { }
    
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Measurement> Measurements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("ConString")!);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   //DateTime conversion ::Credit:Esben::
        var dateOnlyToFromDateTimeConverter = new ValueConverter<DateOnly, DateTime>(
            dateOnlyValue => new DateTime(dateOnlyValue, new TimeOnly(0, 0)),
            dateTimeValue => DateOnly.FromDateTime(dateTimeValue));
        
        modelBuilder.Entity<Patient>(entity =>
        {   //HAS PK SSN - ONE to MANY
            entity.HasKey(p => p.SSN);
            entity.Property(p => p.SSN).IsRequired();
            entity.Property(p => p.Name).IsRequired();
            entity.Property(p => p.Email).IsRequired();

            
            entity.HasMany(p => p.Measurements)
                .WithOne(m => m.Patient)
                .HasForeignKey(m => m.PatientSSN)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Measurement>(entity =>
        {   //HAS PK ID - MANY (measurements) - ONE (patient) -- Converting To DATEONLY
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(p => p.Date)
                .IsRequired()
                .HasConversion(dateOnlyToFromDateTimeConverter);
            entity.Property(p => p.Systolic).IsRequired();
            entity.Property(p => p.Diastolic).IsRequired();
            entity.Property(p => p.Seen).IsRequired();
            
            entity.HasOne(m => m.Patient)
                .WithMany(p => p.Measurements)
                .HasForeignKey(m => m.PatientSSN)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}