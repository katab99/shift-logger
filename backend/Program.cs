var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


var shiftLogs = new List<ShiftLog>();

app.MapGet("/logs", () => shiftLogs);

app.MapPost("/log/new", (ShiftLog log) =>
{
    shiftLogs.Add(log);
    return log;
});

app.Run();

public record ShiftLog(int Id, string Date, string StartTime, string EndTime, int NumOfOrders, float Earnings, float Bonus, float Distance);