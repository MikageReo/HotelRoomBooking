using HotelRoomBooking.Databases;
using HotelRoomBooking.Models;
using HotelRoomBooking.Repositories;
using HotelRoomBooking.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HotelRoomBookingTest.UnitTest.ServicesTest
{
    public class RoomServiceTest
    {

        [Fact]
        public void Constructor_RoomRepositoryIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RoomService(null));
        }

        [Fact]
        public async Task GetAllRoomsAsync_OneSuiteTypeRoomWithName101ExistInTheRepository_ShouldReturnOneSuiteTypeRoomWithName101()
        {
            Room room = new()
            {
                Id = 1,
                Name = "101",
                Type = "Suite",
                IsAvailable = true
            };
            var mockRoomRepository = new Mock<IRoomRepository>();
            mockRoomRepository.Setup(repo => repo.GetAllRoomsAsync()).ReturnsAsync([room]);
            RoomService roomService = new(mockRoomRepository.Object);

            var result = (await roomService.GetAllRoomsAsync()).ToList();

            Assert.Single(result);
            Assert.Equal(room.Type, result[0].Type);
            Assert.Equal(room.Name, result[0].Name);
        }

        [Fact]
        public async Task GetAllRoomsAsync_RoomIsEmptyInTheRepository_ShouldReturnAnEmptyList()
        {
            var mockRoomRepository = new Mock<IRoomRepository>();
            mockRoomRepository.Setup(repo => repo.GetAllRoomsAsync()).ReturnsAsync([]);
            RoomService roomService = new(mockRoomRepository.Object);

            var result = await roomService.GetAllRoomsAsync();

            Assert.Empty(result);
        }
    }
}
