using HotelRoomBooking.DTOs;
using HotelRoomBooking.Models;
using HotelRoomBooking.Repositories;

namespace HotelRoomBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }

        public async Task<BookingResult> CreateBookingAsync(BookingRequestDto bookingRequestDto)
        {
            var allRooms = await _roomRepository.GetAllRoomsAsync();
            var room = allRooms.FirstOrDefault(r => r.Id == bookingRequestDto.RoomId);

            if (room == null ||!room.IsAvailable)
            {
                var availableRooms = allRooms.Where(r => r.IsAvailable);
                return BookingResult.Unavailable(availableRooms);
            }

            room.IsAvailable = false;

            Booking booking = new()
            {
                GuestName = bookingRequestDto.GuestName,
                RoomId = bookingRequestDto.RoomId,
                CheckInDate = bookingRequestDto.CheckInDate,
                CheckOutDate = bookingRequestDto.CheckOutDate
            };

            var createBook = await _bookingRepository.CreateBookingAsync(booking);
            return BookingResult.Success(createBook);
        }
    }
}
