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
    public class RoomControllerTest
    {
        [Fact]
        public void Constructor_RoomServiceIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RoomsController(null));
        }

        [Fact]
        public async Task GetAllRooms_OneRoomExistInRepository_ShouldReturn200OkStatusCode()
        {
            var mockRoomService = new Mock<IRoomService>();
            Room room = new()
            {
                Id = 1,
                Name = "101",
                Type = "Suite",
                IsAvailable = true
            };
            mockRoomService.Setup(service => service.GetAllRoomsAsync()).ReturnsAsync(new List<Room> { room });
            RoomsController roomsController = new(mockRoomService.Object);

            var result = await roomsController.GetAllRooms();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllRooms_NoRoomExistInRepository_ShouldReturn200OkStatusCode()
        {
            var mockRoomService = new Mock<IRoomService>();
            mockRoomService.Setup(service => service.GetAllRoomsAsync()).ReturnsAsync(new List<Room>());
            RoomsController roomsController = new(mockRoomService.Object);

            var result = await roomsController.GetAllRooms();

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}
