using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelRoomBooking.Databases;
using HotelRoomBooking.Models;

namespace HotelRoomBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingRoom : ControllerBase
    {

        private readonly HotelDBContext _hotelDBContext;
        public BookingRoom(HotelDBContext hotelDBContext)
        {
            _hotelDBContext = hotelDBContext ?? throw new ArgumentNullException (nameof(hotelDBContext));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {

            var booking = await _hotelDBContext.Bookings.FindAsync(id);

            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking), "Booking cannot be null.");
            }

            _hotelDBContext.Bookings.Add(booking);
            await _hotelDBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateBooking), new { id = booking.Id }, booking);
        }
    }
}
