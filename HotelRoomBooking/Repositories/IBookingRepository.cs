using HotelRoomBooking.Models;

namespace HotelRoomBooking.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> CreateBookingAsync(Booking booking);
    }
}
