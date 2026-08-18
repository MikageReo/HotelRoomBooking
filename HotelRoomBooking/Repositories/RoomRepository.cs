using HotelRoomBooking.Databases;
using HotelRoomBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelRoomBooking.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDBContext _hotelDBContext;

        public RoomRepository(HotelDBContext hotelDBContext)
        {
            _hotelDBContext = hotelDBContext ?? throw new ArgumentNullException(nameof(hotelDBContext));
        }
        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
            return await _hotelDBContext.Rooms.ToListAsync();
        }
    }
}
