using HotelRoomBooking.Models;

namespace HotelRoomBooking.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
    }
}
