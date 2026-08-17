using HotelRoomBooking.Models;

namespace HotelRoomBooking.Databases
{
    public static class Room1DataSeeder
    {
        public static void Seed(HotelDBContext hotelDBContext)
        {
            if (hotelDBContext.Rooms.Any())
                return;

            var rooms = new List<Room>
            {
                new() { Id = 1, Name = "101", Type = "Single",  IsAvailable = true },
                new() { Id = 2, Name = "102", Type = "Single",  IsAvailable = true },
                new() { Id = 3, Name = "201", Type = "Double",  IsAvailable = true },
                new() { Id = 4, Name = "202", Type = "Double",  IsAvailable = true },
                new() { Id = 5, Name = "301", Type = "Suite",   IsAvailable = true },
            };

            hotelDBContext.Rooms.AddRange(rooms);
            hotelDBContext.SaveChanges();
        }
    }
}
