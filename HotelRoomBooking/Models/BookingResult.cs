namespace HotelRoomBooking.Models
{
    public class BookingResult
    {
        public bool IsSuccess { get; set; }
        public Booking? Booking { get; set; }
        public IEnumerable<Room>? AvailableRooms { get; set; }
        public string Message { get; set; } = string.Empty;

        public static BookingResult Success(Booking booking) => new()
        {
            IsSuccess = true,
            Booking = booking,
        };

        public static BookingResult Unavailable(IEnumerable<Room> availableRooms) => new()
        {
            IsSuccess = false,
            AvailableRooms = availableRooms,
            Message = "The requested room is not available."
        };
    }
}
