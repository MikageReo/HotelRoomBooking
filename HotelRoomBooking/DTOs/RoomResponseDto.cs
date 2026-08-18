namespace HotelRoomBooking.DTOs
{
    public class RoomResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Type { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
