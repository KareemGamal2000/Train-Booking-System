using Data.Models;
using Data.Models.Tickets;
using Data.Repository.UnitOfWork;
using Domain.Dtos.BookingDto;
using Domain.Dtos.BookingDtos;
using Domain.Dtos.TicketDtos;
using Domain.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services.BookingService  
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<BookingConfirmationDto> CreateBookingAsync(Guid userId, BookingCreateDto dto)
        {
            Console.WriteLine($"[BookingService] Starting booking creation for user {userId}");
            Console.WriteLine($"[BookingService] DTO - TripID: {dto.TripID}, ClassID: {dto.ClassID}, DepartureStopID: {dto.DepartureStopID}, ArrivalStopID: {dto.ArrivalStopID}");
            
            // التحقق من الرحلة
            var trip = await _unitOfWork.Trip.GetTripDetailsAsync(dto.TripID);
            if (trip == null)
            {
                Console.WriteLine($"[BookingService] Trip {dto.TripID} not found");
                throw new Exception("Trip not found");
            }
            
            Console.WriteLine($"[BookingService] Trip found: {trip.TripID}");

            var segmentPrice = trip.SegmentPrices?
                .FirstOrDefault(p => p.StartStopID == dto.DepartureStopID
                                  && p.EndStopID == dto.ArrivalStopID
                                  && p.ClassID == dto.ClassID);

            if (segmentPrice == null)
            {
                Console.WriteLine($"[BookingService] Segment price not found for: StartStopID={dto.DepartureStopID}, EndStopID={dto.ArrivalStopID}, ClassID={dto.ClassID}");
                Console.WriteLine($"[BookingService] Available segment prices:");
                if (trip.SegmentPrices != null)
                {
                    foreach (var sp in trip.SegmentPrices)
                    {
                        Console.WriteLine($"  - StartStopID={sp.StartStopID}, EndStopID={sp.EndStopID}, ClassID={sp.ClassID}, Price={sp.Price}");
                    }
                }
                else
                {
                    Console.WriteLine("  - No segment prices available");
                }
                throw new Exception("لا يوجد سعر محدد لهذا المسار");
            }

            if (segmentPrice.Price == null)
                throw new Exception("سعر المسار غير محدد");

            decimal totalPrice = segmentPrice.Price.Value * dto.NumberOfSeats;

            var departureStop = trip.Stops?.FirstOrDefault(s => s.TripStopID == dto.DepartureStopID);
            var arrivalStop = trip.Stops?.FirstOrDefault(s => s.TripStopID == dto.ArrivalStopID);

            if (departureStop == null || arrivalStop == null)
                throw new Exception("محطات الرحلة غير صحيحة");

            // التحقق من الدرجة
            var classInfo = await _unitOfWork.Class.GetByIdAsync(dto.ClassID);
            if (classInfo == null)
                throw new Exception("الدرجة غير موجودة");

            // إنشاء الحجز باستخدام manual mapping
            var booking = new Booking
            {
                Booking_ID = Guid.NewGuid(),
                UserID = userId,
                TripID = dto.TripID,
                DepartureStopID = dto.DepartureStopID,
                ArrivalStopID = dto.ArrivalStopID,
                BookingDate = DateTime.UtcNow,
                BookingStatus = "Pending",
                TotalPrice = totalPrice,
                Tickets = new List<Ticket>()
            };

            // إضافة التذاكر
            if (dto.SelectedSeatIDs != null && dto.SelectedSeatIDs.Any())
            {
                Console.WriteLine($"[BookingService] Processing {dto.SelectedSeatIDs.Count} seats");
                
                foreach (var seatId in dto.SelectedSeatIDs)
                {
                    Console.WriteLine($"[BookingService] Checking seat {seatId}");
                    
                    // التحقق من وجود المقعد في قاعدة البيانات
                    var seat = await _unitOfWork.Seat.GetSeatByIdAsync(seatId);
                    if (seat == null)
                    {
                        Console.WriteLine($"[BookingService] ERROR: Seat {seatId} not found in database");
                        throw new Exception($"المقعد رقم {seatId} غير موجود في النظام. يرجى التأكد من أن المقاعد تم إنشاؤها في قاعدة البيانات.");
                    }
                    
                    Console.WriteLine($"[BookingService] Seat {seatId} found, SeatNumber: {seat.SeatNumber}, CoachID: {seat.CoachID}");
                    
                    // التحقق من توفر المقعد
                    var isAvailable = await _unitOfWork.Ticket.IsSeatAvailableAsync(seatId, dto.TripID);
                    if (!isAvailable)
                    {
                        Console.WriteLine($"[BookingService] Seat {seatId} is not available for trip {dto.TripID}");
                        throw new Exception($"المقعد رقم {seatId} غير متاح");
                    }
                    
                    Console.WriteLine($"[BookingService] Seat {seatId} is available, creating ticket");

                    var ticket = new Ticket
                    {
                        Ticket_ID = Guid.NewGuid(),
                        Booking_ID = booking.Booking_ID,
                        SeatID = seatId,
                        ClassID = dto.ClassID,
                        Price = segmentPrice.Price.Value
                    };
                    
                    booking.Tickets.Add(ticket);
                    Console.WriteLine($"[BookingService] Ticket added for seat {seatId}");
                }
            }

            Console.WriteLine($"[BookingService] Saving booking with {booking.Tickets.Count} tickets");
            
            // حفظ الحجز
            try
            {
                await _unitOfWork.Booking.AddBookingAsync(booking);
                Console.WriteLine("[BookingService] Booking added to context");
                
                await _unitOfWork.SaveChangesAsync();
                Console.WriteLine("[BookingService] Changes saved successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BookingService] ERROR saving booking: {ex.Message}");
                Console.WriteLine($"[BookingService] Inner exception: {ex.InnerException?.Message}");
                Console.WriteLine($"[BookingService] Stack trace: {ex.StackTrace}");
                throw new Exception($"فشل حفظ الحجز: {ex.InnerException?.Message ?? ex.Message}");
            }

           
            var savedBooking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(booking.Booking_ID);

            return new BookingConfirmationDto
            {
                BookingID = savedBooking.Booking_ID,
                BookingReference = savedBooking.Booking_ID.ToString().Substring(0, 8).ToUpper(),
                BookingDate = savedBooking.BookingDate,
                BookingStatus = savedBooking.BookingStatus,
                TotalPrice = savedBooking.TotalPrice,
                DepartureStation = departureStop.Station?.StationNameAR ?? "غير محدد",
                ArrivalStation = arrivalStop.Station?.StationNameAR ?? "غير محدد",
                DepartureTime = departureStop.DepartureTime ?? TimeSpan.Zero,
                ArrivalTime = arrivalStop.ArrivalTime ?? TimeSpan.Zero,
                ClassName = classInfo.ClassNameAR ?? "غير محدد",
                Tickets = savedBooking.Tickets?.Select(t => TicketProfile.ToTicketReadDto(t)).ToList() ?? new List<TicketReadDto>()
            };
        }

        // ===================== Cancel Booking =======================
        public async Task<bool> CancelBookingAsync(BookingCancelDto dto)
        {
            try
            {
                Console.WriteLine($"[BookingService] Canceling booking {dto.BookingId}");
                
                var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(dto.BookingId);
                
                if (booking == null)
                {
                    Console.WriteLine($"[BookingService] Booking {dto.BookingId} not found");
                    throw new InvalidOperationException("الحجز غير موجود");
                }

                // التحقق من حالة الحجز الحالية
                if (booking.BookingStatus == "Cancelled")
                {
                    Console.WriteLine($"[BookingService] Booking {dto.BookingId} is already cancelled");
                    throw new InvalidOperationException("الحجز ملغي بالفعل");
                }

                if (booking.BookingStatus != "Pending" && booking.BookingStatus != "Confirmed")
                {
                    Console.WriteLine($"[BookingService] Cannot cancel booking with status: {booking.BookingStatus}");
                    throw new InvalidOperationException($"لا يمكن إلغاء الحجز في حالة {booking.BookingStatus}");
                }

                Console.WriteLine($"[BookingService] Current booking status: {booking.BookingStatus}");
                
                // تحديث حالة الحجز
                booking.BookingStatus = "Cancelled";
                
                // حذف جميع التذاكر المرتبطة بالحجز لتحرير المقاعد
                if (booking.Tickets != null && booking.Tickets.Any())
                {
                    Console.WriteLine($"[BookingService] Removing {booking.Tickets.Count} tickets");
                    
                    // إزالة التذاكر من الحجز
                    booking.Tickets.Clear();
                }

                // تحديث الحجز في قاعدة البيانات
                _unitOfWork.Booking.Update(booking);
                
                // حفظ التغييرات
                var result = await _unitOfWork.SaveChangesAsync();
                
                Console.WriteLine($"[BookingService] Booking cancelled successfully: {result}");
                
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BookingService] ERROR canceling booking: {ex.Message}");
                Console.WriteLine($"[BookingService] Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        // ===================== Get Booking By ID =======================
        public async Task<BookingReadDto> GetBookingByIdAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(bookingId);
            
            if (booking == null)
                return null;

            return booking.ToBookingReadDto();
        }

        // ==================== Get User Bookings =========================
        public async Task<IEnumerable<BookingReadDto>> GetUserBookingsAsync(Guid userId)
        {
            var bookings = await _unitOfWork.Booking.GetBookingsByUserAsync(userId);

            return bookings.ToBookingReadDtoList();
        }

        // ===================== Select Seats ==============================
        public async Task<bool> SelectSeatsAsync(Guid bookingId, BookingSeatSelectionDto dto)
        {
            var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            // الحصول على ClassID من الحجز الموجود
            var classId = booking.Tickets?.FirstOrDefault()?.ClassID ?? 0;
            
            // إذا لم تكن هناك تذاكر، استخدم CoachId من DTO (وهو في الحقيقة ClassID)
            if (classId == 0)
            {
                classId = dto.CoachId;
            }

            booking.Tickets.Clear();

            decimal total = 0;

            foreach (var seatId in dto.SelectedSeatIDs)
            {
                var seat = await _unitOfWork.Seat.GetSeatByIdAsync(seatId);
                if (seat == null)
                    throw new Exception($"Seat {seatId} not found");

                booking.Tickets.Add(new Ticket
                {
                    Ticket_ID = Guid.NewGuid(),
                    Booking_ID = booking.Booking_ID,
                    SeatID = seatId,
                    ClassID = classId, // إضافة ClassID
                    Price = dto.PricePerSeat,
                });

                total += dto.PricePerSeat;
            }

            booking.TotalPrice = total;
            booking.BookingStatus = "Confirmed";

            return await _unitOfWork.SaveChangesAsync();
        }

        // ====================== Booking Summary ===========================
        public async Task<BookingSummaryDto> GetBookingSummaryAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(bookingId);
            
            if (booking == null)
                return null;

            return booking.ToBookingSummaryDto();
        }

        // ====================== Confirm Booking ===========================
        public async Task<bool> ConfirmBookingAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.Booking.GetBookingWithDetailsAsync(bookingId);
            if (booking == null)
                return false;

            booking.BookingStatus = "Confirmed";
            await _unitOfWork.Booking.UpdateBookingAsync(booking);
            return await _unitOfWork.SaveChangesAsync();
        }
        
        // ====================== Get Available Seats ===========================
        public async Task<AvailableSeatsDto> GetAvailableSeatsAsync(int tripId, long classId, int departureStopId, int arrivalStopId)
        {
            var availableSeats = await _unitOfWork.Seat.GetAvailableSeatsByTripAsync(tripId, classId, departureStopId, arrivalStopId);
            
            return availableSeats.ToAvailableSeatsDto(tripId, classId);
        }

        // ===================== Generate Seats for Coach (Debug) =======================
        public async Task<int> GenerateSeatsForCoachAsync(long coachId)
        {
            Console.WriteLine($"[BookingService] Generating seats for coach {coachId}");
            
            var result = await _unitOfWork.Seat.GenerateSeatsForCoachAsync(coachId, 60);
            
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
                var seats = await _unitOfWork.Seat.GetSeatsByCoachIdAsync(coachId);
                return seats.Count();
            }
            
            return 0;
        }
    }
}