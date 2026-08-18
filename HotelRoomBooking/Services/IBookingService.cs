using HotelRoomBooking.DTOs;
using HotelRoomBooking.Models;

namespace HotelRoomBooking.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<BookingResult> CreateBookingAsync(BookingRequestDto bookingRequestDto);
    }
}
