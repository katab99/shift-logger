using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class ShiftLog
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int NumOfOrders { get; set; }
        public float Earnings { get; set; }
        public float Bonus { get; set; }
        public float Distance { get; set; }
    }

    class ShiftLogDb : DbContext
    {
        public ShiftLogDb(DbContextOptions options) : base(options) { }
        public DbSet<ShiftLog> ShiftLogs { get; set; } = null!;
    }
}