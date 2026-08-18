using HotelRoomBooking.DTOs;
using HotelRoomBooking.Models;

namespace HotelRoomBooking.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomResponseDto>> GetAllRoomsAsync();
    }
}
