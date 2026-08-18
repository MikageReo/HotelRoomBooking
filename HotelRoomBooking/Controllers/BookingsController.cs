using HotelRoomBooking.Databases;
using HotelRoomBooking.DTOs;
using HotelRoomBooking.Models;
using HotelRoomBooking.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateBooking(BookingRequestDto bookingRequestDto)
        {
            var createdBooking = await _bookingService.CreateBookingAsync(bookingRequestDto);

            if (!createdBooking.IsSuccess)
            {
                var selectedRoom = createdBooking.AvailableRooms?.Select(room => new RoomResponseDto
                {
                    Id = room.Id,
                    Name = room.Name,
                    Type = room.Type,
                    IsAvailable = room.IsAvailable
                });

                return BadRequest(new { createdBooking.Message, AvailableRooms = selectedRoom });
            }

            BookingResponseDto bookingResponseDto = new()
            {
                Id = createdBooking.Booking!.Id,
                GuestName = createdBooking.Booking.GuestName,
                RoomId = createdBooking.Booking.RoomId,
                CheckInDate = createdBooking.Booking.CheckInDate,
                CheckOutDate = createdBooking.Booking.CheckOutDate
            };

            return CreatedAtAction(nameof(GetAllBookings), new { id = createdBooking.Booking!.Id }, createdBooking.Booking);

        }
    }
}
