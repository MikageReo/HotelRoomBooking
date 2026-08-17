using Microsoft.AspNetCore.Mvc;

using HotelRoomBooking.Databases;
using HotelRoomBooking.Models;
using HotelRoomBooking.Services;

namespace HotelRoomBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {

        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            var created = await _bookingService.CreateBookingAsync(booking);

            if (!created.IsSuccess)
            {
                return BadRequest(new { created.Message, created.AvailableRooms });
            }

            return CreatedAtAction(nameof(GetAllBookings), new { id = created.Booking!.Id }, created.Booking);

        }
    }
}
