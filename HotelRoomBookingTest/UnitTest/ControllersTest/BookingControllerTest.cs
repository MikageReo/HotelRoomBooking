using HotelRoomBooking.Controllers;
using HotelRoomBooking.Models;
using HotelRoomBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomBookingTest.UnitTest.ControllersTest
{
    public class BookingControllerTest
    {
        [Fact]
        public void Constructor_BookingServiceIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new BookingsController(null));
        }

        [Fact]
        public async Task GetAllBookings_OneBookingExistInRepository_ShouldReturn200OkStatusCode()
        {
            var mockBookingService = new Mock<IBookingService>();
            Booking booking = new()
            {
                Id = 1,
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            mockBookingService.Setup(service => service.GetAllBookingsAsync()).ReturnsAsync([booking]);
            BookingsController bookingsController = new(mockBookingService.Object);

            var result = await bookingsController.GetAllBookings();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllBookings_NoBookingExistInRepository_ShouldReturn200OkStatusCode()
        {
            var mockBookingService = new Mock<IBookingService>();
            mockBookingService.Setup(service => service.GetAllBookingsAsync()).ReturnsAsync([]);
            BookingsController bookingsController = new(mockBookingService.Object);

            var result = await bookingsController.GetAllBookings();

            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateBooking_OneRoomIsAvailable_ShouldReturn201CreatedStatusCode()
        {
            var mockBookingService = new Mock<IBookingService>();
            Booking booking = new()
            {
                Id = 1,
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            mockBookingService.Setup(service => service.CreateBookingAsync(booking)).ReturnsAsync(BookingResult.Success(booking));
            BookingsController bookingsController = new(mockBookingService.Object);

            var result = await bookingsController.CreateBooking(booking);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task CreateBooking_RoomIsNotAvailable_ShouldReturn400BadRequestStatusCode()
        {
            var mockBookingService = new Mock<IBookingService>();
            var mockRoomService = new Mock<IRoomService>();
            Booking booking = new()
            {
                Id = 1,
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            Room availableRoom = new()
            {
                Id = 2,
                Name = "102",
                Type = "Single",
                IsAvailable = true
            };
            mockBookingService.Setup(service => service.CreateBookingAsync(booking)).ReturnsAsync(BookingResult.Unavailable([availableRoom]));

            var result = await new BookingsController(mockBookingService.Object).CreateBooking(booking);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
