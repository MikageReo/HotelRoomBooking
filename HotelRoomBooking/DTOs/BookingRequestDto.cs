using System.ComponentModel.DataAnnotations;

namespace HotelRoomBooking.DTOs
{
    public class BookingRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "Guest name is required.")]
        public string GuestName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Room ID is required.")]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Check-in date is required.")]
        public DateTime CheckInDate { get; set; }
        [Required(ErrorMessage = "Check-out date is required.")]
        public DateTime CheckOutDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CheckOutDate < CheckInDate)
            {
                yield return new ValidationResult("Check-out date must be after check-in date.", new[] { nameof(CheckOutDate) });
            }
        }
    }
}
