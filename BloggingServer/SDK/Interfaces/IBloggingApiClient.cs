using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SDK.RefitModels;

namespace SDK.Interfaces;

/// <summary>
/// Here we add the endpoints for the entire application
/// </summary>
public interface IBloggingApiClient
{
    /// <summary>
    /// Get all users.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<List<User>>> GetUsersAsync();

    /// <summary>
    /// Get user by CNP.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<User>> GetUserAsync(string CNP);

    /// <summary>
    /// Delete user.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> DeleteUserAsync(string CNP);

    /// <summary>
    /// Create user.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> RegisterUserAsync(UserRegister user);

    /// <summary>
    /// Login user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<ApiResponseMessage<UserLoginResponse>> LoginUserAsync(UserLogin user);

    /// <summary>
    /// Update user.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> UpdateUserAsync(UserUpdate user);


    /// <summary>
    /// Get all air flights.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<List<Ticket>>> GetTicketsAsync();

    /// <summary>
    /// Get air flight by ID.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<TicketDetail>> GetTicketAsync(Guid id);

    /// <summary>
    /// Create ir flight.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> CreateTicketAsync(AddTicket ticket);

    /// <summary>
    /// Update air flight.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> UpdateTicketAsync(Ticket ticket);

    /// <summary>
    /// Delete air flight.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> DeleteTicketAsync(Guid id);


    /// <summary>
    /// Get all companies.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<List<Company>>> GetCompaniesAsync();

    /// <summary>
    /// Get Company by ID.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<Company>> GetCompanyAsync(Guid id);

    /// <summary>
    /// Create Company.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> CreateCompanyAsync(Company company);

    /// <summary>
    /// Update Company.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> UpdateCompanyAsync(Company company);

    /// <summary>
    /// Delete Company.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> DeleteCompanyAsync(Guid id);


    /// <summary>
    /// Get all layovers.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<List<Layover>>> GetLayoversAsync();

    /// <summary>
    /// Get layover by ID.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<Layover>> GetLayoverAsync(Guid id);

    /// <summary>
    /// Create layover.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> CreateLayoverAsync(Layover layover);

    /// <summary>
    /// Update layover.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> UpdateLayoverAsync(Layover layover);

    /// <summary>
    /// Delete layover
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> DeleteLayoverAsync(Guid id);


    /// <summary>
    /// Get all plane facilities.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<List<PlaneFacility>>> GetPlaneFacilitiesAsync();

    /// <summary>
    /// Get plane facility by ID.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<PlaneFacility>> GetPlaneFacilityAsync(Guid id);

    /// <summary>
    /// Create plane facility.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> CreatePlaneFacilityAsync(PlaneFacility planeFacility);

    /// <summary>
    /// Update plane facility.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> UpdatePlaneFacilityAsync(PlaneFacility planeFacility);

    /// <summary>
    /// Delete plane facility.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> DeletePlaneFacilityAsync(Guid id);

    /// <summary>
    /// Get all plane seats.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<List<PlaneSeat>>> GetPlaneSeatsAsync();

    /// <summary>
    /// Get plane seat by ID.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<PlaneSeat>> GetPlaneSeatAsync(Guid id);

    /// <summary>
    /// Create plane seat.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> CreatePlaneSeatAsync(PlaneSeat planeSeat);

    /// <summary>
    /// Update plane seat.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> UpdatePlaneSeatAsync(PlaneSeat planeSeat);

    /// <summary>
    /// Delete plane seat.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> DeletePlaneSeatAsync(Guid id);

    /// <summary>
    /// Get all bookings.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<List<Booking>>> GetBookingsAsync();

    /// <summary>
    /// Get booking by ID.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage<Booking>> GetBookingAsync(Guid id);

    /// <summary>
    /// Create booking.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> CreateBookingAsync(Booking booking);

    /// <summary>
    /// Update booking.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> UpdateBookingAsync(Booking booking);

    /// <summary>
    /// Delete booking.
    /// </summary>
    /// <returns></returns>
    Task<ApiResponseMessage> DeleteBookingAsync(Guid id);
}