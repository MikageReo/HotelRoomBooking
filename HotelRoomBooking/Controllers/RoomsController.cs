using HotelRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelRoomBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }
    }
}
