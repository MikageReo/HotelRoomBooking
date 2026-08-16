using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelRoomBooking.Models;
using HotelRoomBooking.Controllers;
using HotelRoomBooking.Databases;

namespace HotelRoomBookingTest
{
    public class BookingRoomTest
    {
        private HotelDBContext GetHotelDBContext()
        {
            var options = new DbContextOptionsBuilder<HotelDBContext>()
                .UseInMemoryDatabase(databaseName: "HotelDB")
                .Options;

            return new HotelDBContext(options);
        }

        //[Fact]
        //public void Constructor_HotelDBContextIsNull_ShouldThrowArgumentNullException()
        //{
        //    Assert.Throws<ArgumentNullException>(new BookingRoom(null);
        //}

        [Fact]
        public async Task CreateBooking_BookingRoomIdIs1WithValidData_ShouldReturn201CreatedAtActionResult()
        {
            var hotelDBContext = GetHotelDBContext();
            Room room = new()
            {
                Id = 1,
                Name = "101",
                Type = "Single",
                IsAvailable = true
            };
            hotelDBContext.Rooms.Add(room);
            await hotelDBContext.SaveChangesAsync();
            BookingRoom bookingRoom = new(hotelDBContext);
            Booking booking = new()
            {
                Id = 1,
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };

            var result = await bookingRoom.CreateBooking(booking);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
        }

        [Fact]
        public async Task CreateBooking_BookingRoomIdIs1WithValidData_ShouldSavesTheDataCorrectly()
        {
            var hotelDBContext = GetHotelDBContext();
            Room room = new()
            {
                Id = 1,
                Name = "101",
                Type = "Single",
                IsAvailable = true
            };
            hotelDBContext.Rooms.Add(room);
            await hotelDBContext.SaveChangesAsync();
            BookingRoom boookingRoom = new(hotelDBContext);
            Booking booking = new()
            {
                Id = 1,
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };

            var result = await boookingRoom.CreateBooking(booking);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdBooking = Assert.IsType<Booking>(createdAtActionResult.Value);
            Assert.Equal(booking.Id, createdBooking.Id);
            Assert.Equal(booking.GuestName, createdBooking.GuestName);
            Assert.Equal(booking.RoomId, createdBooking.RoomId);
            Assert.Equal(booking.CheckInDate, createdBooking.CheckInDate);
            Assert.Equal(booking.CheckOutDate, createdBooking.CheckOutDate);
        }
    }
}
