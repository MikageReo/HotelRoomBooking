using HotelRoomBooking.DTOs;
using HotelRoomBooking.Models;
using HotelRoomBooking.Repositories;

namespace HotelRoomBooking.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        public async Task<IEnumerable<RoomResponseDto>> GetAllRoomsAsync()
        {
            var rooms = await _roomRepository.GetAllRoomsAsync();

            var roomDtos = rooms.Select(r => new RoomResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Type = r.Type,
                IsAvailable = r.IsAvailable
            });

            return roomDtos;
        }
    }
}
