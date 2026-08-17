namespace HotelRoomBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public Room? Room { get; set; }
    }
}
