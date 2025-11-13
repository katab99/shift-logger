using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LogDb>(options =>
    options.UseSqlite("Data Source=logs.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LogDb>();
    db.Database.EnsureCreated();
}

// get all logs
app.MapGet("/logs", async (LogDb db) =>
{
    var logs = await db.Logs.ToListAsync();
    return Results.Ok(logs);
});

// get log by id
app.MapGet("/logs/{id}", async (int id, LogDb db) =>
{
    var log = await db.Logs.FindAsync(id);
    return log is not null ? Results.Ok(log) : Results.NotFound();
});

// create a new log
app.MapPost("/logs", async (Log log, LogDb db) =>
{
    db.Logs.Add(log);
    await db.SaveChangesAsync();
    return Results.Created($"/logs/{log.Id}", log);
});

// update log
app.MapPut("/logs/{id}", async (int id, Log inputLog, LogDb db) =>
{
    var log = await db.Logs.FindAsync(id);
    if (log is null) return Results.NotFound();

    log.Date = inputLog.Date;
    log.StartTime = inputLog.StartTime;
    log.EndTime = inputLog.EndTime;
    log.NumOfOrders = inputLog.NumOfOrders;
    log.Earnings = inputLog.Earnings;
    log.Bonus = inputLog.Bonus;
    log.Distance = inputLog.Distance;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// delete log
app.MapDelete("/logs/{id}", async (int id, LogDb db) =>
{
    if (await db.Logs.FindAsync(id) is Log log)
    {
        db.Logs.Remove(log);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();


// Models
public class Log
{
    public int Id { get; set; }
    public string? Date { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public int NumOfOrders { get; set; }
    public float Earnings { get; set; }
    public float Bonus { get; set; }
    public float Distance { get; set; }
}

public class LogDb : DbContext
{
    public LogDb(DbContextOptions<LogDb> options) 
        : base(options) { }

    public DbSet<Log> Logs => Set<Log>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("shiftLogs");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Date)
                .HasColumnName("date")
                .IsRequired();

            entity.Property(e => e.StartTime)
                .HasColumnName("startTime")
                .IsRequired();

            entity.Property(e => e.EndTime)
                .HasColumnName("endTime")
                .IsRequired();

            entity.Property(e => e.NumOfOrders)
                .HasColumnName("numOfOrders")
                .IsRequired();

            entity.Property(e => e.Earnings)
                .HasColumnName("earnings")
                .IsRequired();

            entity.Property(e => e.Bonus)
                .HasColumnName("bonus")
                .IsRequired();

            entity.Property(e => e.Distance)
                .HasColumnName("distance")
                .IsRequired();
        });
    }
}