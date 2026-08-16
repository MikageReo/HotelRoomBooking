using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HotelRoomBooking.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Type { get; set; }

        public bool IsAvailable { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
