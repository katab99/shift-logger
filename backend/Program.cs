using Microsoft.EntityFrameworkCore;
using backend.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ShiftLogs") ?? "Data Source=ShiftLogs.db";

builder.Services.AddSqlite<ShiftLogDb>(connectionString);

var app = builder.Build();

app.MapGet("/shiftLogs", async (ShiftLogDb db) => await db.ShiftLogs.ToListAsync());

app.MapPost("/shiftLogs", async (ShiftLogDb db, ShiftLog log) =>
{
    await db.ShiftLogs.AddAsync(log);
    await db.SaveChangesAsync();
    return Results.Created($"/logs/{log.Id}", log);
});

app.MapGet("/shiftLogs/{id}", async (ShiftLogDb db, int id) => await db.ShiftLogs.FindAsync(id));

app.MapPut("/shiftLogs/{id}", async (ShiftLogDb db, ShiftLog updateShiftLog, int id) =>
{
    var shiftLog = await db.ShiftLogs.FindAsync(id);
    if (shiftLog is null) return Results.NotFound();

    shiftLog.Date = updateShiftLog.Date;
    shiftLog.StartTime = updateShiftLog.StartTime;
    shiftLog.EndTime = updateShiftLog.EndTime;
    shiftLog.NumOfOrders = updateShiftLog.NumOfOrders;
    shiftLog.Earnings = updateShiftLog.Earnings;
    shiftLog.Bonus = updateShiftLog.Bonus;
    shiftLog.Distance = updateShiftLog.Distance;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/shiftLogs/{id}", async (ShiftLogDb db, int id) =>
{
    var shiftLog = await db.ShiftLogs.FindAsync(id);
    if (shiftLog is null) return Results.NotFound();

    db.ShiftLogs.Remove(shiftLog);
    await db.SaveChangesAsync();
    return Results.Ok();

});

app.Run();
