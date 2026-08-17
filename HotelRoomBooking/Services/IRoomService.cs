using HotelRoomBooking.Models;

namespace HotelRoomBooking.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
    }
}
