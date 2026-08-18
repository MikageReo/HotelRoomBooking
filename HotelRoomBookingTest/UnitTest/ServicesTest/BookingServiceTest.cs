using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelRoomBooking.Models;
using HotelRoomBooking.Controllers;
using HotelRoomBooking.Databases;
using HotelRoomBooking.Services;
using HotelRoomBooking.Repositories;
using Moq;
using HotelRoomBooking.DTOs;

namespace HotelRoomBookingTest.UnitTest.ServicesTest
{
    public class BookingServiceTest
    {

        [Fact]
        public void Constructor_BookingRepositoryAndRoomRepositoryIsNull_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new BookingService(null, null));
        }

        [Fact]
        public void Constructor_BookingRepositoryIsNull_ShouldThrowArgumentNullException()
        {
            var mockRoomRepository = new Mock<IRoomRepository>();
            Assert.Throws<ArgumentNullException>(() => new BookingService(null, mockRoomRepository.Object));
        }

        [Fact]
        public void Constructor_RoomRepositoryIsNull_ShouldThrowArgumentNullException()
        {
            var mockBookingRepository = new Mock<IBookingRepository>();
            Assert.Throws<ArgumentNullException>(() => new BookingService(mockBookingRepository.Object, null));
        }

        [Fact]
        public async Task GetAllBookingAsync_BookingRoomIsEmptyInTheRepository_ShouldReturnAnEmptyList()
        {
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockRoomRepository = new Mock<IRoomRepository>();
            mockBookingRepository.Setup(repo => repo.GetAllBookingsAsync()).ReturnsAsync(new List<Booking>());
            BookingService bookingService = new(mockBookingRepository.Object, mockRoomRepository.Object);

            var result = await bookingService.GetAllBookingsAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllBookingsAsync_RoomOneAndRoomTwoIsBooked_ShouldReturnTwoBookingsRoom()
        {
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockRoomRepository = new Mock<IRoomRepository>();
            Booking booking1 = new()
            {
                Id = 1,
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            Booking booking2 = new()
            {
                Id = 2,
                GuestName = "Alice",
                RoomId = 2,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            mockBookingRepository.Setup(repo => repo.GetAllBookingsAsync()).ReturnsAsync(new List<Booking> { booking1, booking2 });
            BookingService bookingService = new(mockBookingRepository.Object, mockRoomRepository.Object);

            var result = await bookingService.GetAllBookingsAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllBookingsAsync_RoomOneIsBooked_ShouldReturnBookingInformations()
        {
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockRoomRepository = new Mock<IRoomRepository>();
            Booking booking = new()
            {
                Id = 1,
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            mockBookingRepository.Setup(repo => repo.GetAllBookingsAsync()).ReturnsAsync([booking]);
            BookingService bookingService = new(mockBookingRepository.Object, mockRoomRepository.Object);

            var result = (await bookingService.GetAllBookingsAsync()).ToList();

            Assert.Single(result);
            Assert.Equal(booking.Id, result[0].Id);
            Assert.Equal(booking.GuestName, result[0].GuestName);
            Assert.Equal(booking.RoomId, result[0].RoomId);
            Assert.Equal(booking.CheckInDate, result[0].CheckInDate);
            Assert.Equal(booking.CheckOutDate, result[0].CheckOutDate);
        }

        [Fact]
        public async Task CreateBookingAsync_RoomOneIsAvailable_ShouldReturnSuccess()
        {
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockRoomRepository = new Mock<IRoomRepository>();
            Room room = new()
            {
                Id = 1,
                Name = "101",
                Type = "Single",
                IsAvailable = true
            };
            BookingRequestDto bookingRequestDto = new()
            {
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            Booking createBookingFromDB = new()
            {
                Id = 1,
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = bookingRequestDto.CheckInDate,
                CheckOutDate = bookingRequestDto.CheckOutDate
            };
            mockRoomRepository.Setup(repo => repo.GetAllRoomsAsync()).ReturnsAsync(new List<Room> { room });
            mockBookingRepository.Setup(repo => repo.CreateBookingAsync(createBookingFromDB)).ReturnsAsync(createBookingFromDB);

            BookingService bookingService = new(mockBookingRepository.Object, mockRoomRepository.Object);

            var result = await bookingService.CreateBookingAsync(bookingRequestDto);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task CreateBookingAsync_RoomOneIsAvailable_ShouldChangeTheRoomIsAvailableToFalse()
        {
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockRoomRepository = new Mock<IRoomRepository>();
            Room room = new()
            {
                Id = 1,
                Name = "101",
                Type = "Single",
                IsAvailable = true
            };
            BookingRequestDto bookingRequestDto = new()
            {
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            Booking createBookingFromDB = new()
            {
                Id = 1,
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            mockRoomRepository.Setup(repo => repo.GetAllRoomsAsync()).ReturnsAsync(new List<Room> { room });
            mockBookingRepository.Setup(repo => repo.CreateBookingAsync(createBookingFromDB)).ReturnsAsync(createBookingFromDB);

            BookingService bookingService = new(mockBookingRepository.Object, mockRoomRepository.Object);

            var result = await bookingService.CreateBookingAsync(bookingRequestDto);

            Assert.False(room.IsAvailable);
        }

        [Fact]
        public async Task CreateBookingAsync_RoomOneIsNotAvailable_ShouldReturnNotSuccessAndShowAnAvailableRooms()
        {
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockRoomRepository = new Mock<IRoomRepository>();
            Room unavailableRoom = new()
            {
                Id = 1,
                Name = "101",
                Type = "Single",
                IsAvailable = false
            };
            Room availableRoom = new()
            {
                Id = 2,
                Name = "102",
                Type = "Single",
                IsAvailable = true
            };
            BookingRequestDto bookingRequestDto = new()
            {
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            mockRoomRepository.Setup(repo => repo.GetAllRoomsAsync()).ReturnsAsync(new List<Room> { unavailableRoom, availableRoom });
            BookingService bookingService = new(mockBookingRepository.Object, mockRoomRepository.Object);

            var result = await bookingService.CreateBookingAsync(bookingRequestDto);

            Assert.False(result.IsSuccess);
            Assert.Contains(availableRoom, result.AvailableRooms!);
        }

        [Fact]
        public async Task CreateBookingAsync_CheckInDateIsLaterThanCheckOutDate_ShouldReturnNotSuccess()
        {
            var mockBookingRepository = new Mock<IBookingRepository>();
            var mockRoomRepository = new Mock<IRoomRepository>();
            Room unavailableRoom = new()
            {
                Id = 1,
                Name = "101",
                Type = "Single",
                IsAvailable = false
            };
            Room availableRoom = new()
            {
                Id = 2,
                Name = "102",
                Type = "Single",
                IsAvailable = true
            };
            BookingRequestDto bookingRequestDto = new()
            {
                GuestName = "Hariz",
                RoomId = 1,
                CheckInDate = DateTime.Now.AddDays(3),
                CheckOutDate = DateTime.Now.AddDays(2)
            };
            mockRoomRepository.Setup(repo => repo.GetAllRoomsAsync()).ReturnsAsync(new List<Room> { unavailableRoom, availableRoom });
            BookingService bookingService = new(mockBookingRepository.Object, mockRoomRepository.Object);

            var result = await bookingService.CreateBookingAsync(bookingRequestDto);

            Assert.False(result.IsSuccess);
        }

    }
}
