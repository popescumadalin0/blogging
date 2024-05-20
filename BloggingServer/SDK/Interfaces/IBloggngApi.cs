using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Request;
using Models.Response;
using Refit;

namespace SDK.Interfaces;

/// <summary>
/// Here we add the endpoints for the entire application
/// </summary>
public interface IBloggingApi
{
    [Get("/api/User")]
    [Headers("Authorization: Bearer")]
    Task<List<User>> GetUsersAsync();

    [Get("/api/User/{CNP}")]
    [Headers("Authorization: Bearer")]
    Task<User> GetUserAsync(string CNP);

    [Delete("/api/User/{CNP}")]
    [Headers("Authorization: Bearer")]
    Task DeleteUserAsync(string CNP);

    [Post("/api/User/register")]
    Task RegisterUserAsync(UserRegister user);

    [Post("/api/User/login")]
    Task<UserLoginResponse> LoginUserAsync(UserLogin user);

    [Put("/api/User")]
    [Headers("Authorization: Bearer")]
    Task UpdateUserAsync(UserUpdate user);

    [Get("/api/Ticket")]
    Task<List<Ticket>> GetTicketsAsync();

    [Get("/api/Ticket/{id}")]
    Task<TicketDetail> GetTicketAsync(Guid id);

    [Post("/api/Ticket")]
    [Headers("Authorization: Bearer")]
    Task CreateTicketAsync(AddTicket ticket);

    [Put("/api/Ticket")]
    [Headers("Authorization: Bearer")]
    Task UpdateTicketAsync(Ticket ticket);

    [Delete("/api/Ticket/{id}")]
    Task DeleteTicketAsync(Guid id);

    [Get("/api/Company")]
    Task<List<Company>> GetCompaniesAsync();

    [Get("/api/Company/{id}")]
    Task<Company> GetCompanyAsync(Guid id);

    [Post("/api/Company")]
    [Headers("Authorization: Bearer")]
    Task CreateCompanyAsync(Company company);

    [Put("/api/Company")]
    [Headers("Authorization: Bearer")]
    Task UpdateCompanyAsync(Company company);

    [Delete("/api/Company/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteCompanyAsync(Guid id);

    [Get("/api/Layover")]
    Task<List<Layover>> GetLayoversAsync();

    [Get("/api/Layover/{id}")]
    Task<Layover> GetLayoverAsync(Guid id);

    [Post("/api/Layover")]
    [Headers("Authorization: Bearer")]
    Task CreateLayoverAsync(Layover layover);

    [Put("/api/Layover")]
    [Headers("Authorization: Bearer")]
    Task UpdateLayoverAsync(Layover layover);

    [Delete("/api/Layover/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteLayoverAsync(Guid id);

    [Get("/api/PlaneFacility")]
    Task<List<PlaneFacility>> GetPlaneFacilitiesAsync();

    [Get("/api/PlaneFacility/{id}")]
    Task<PlaneFacility> GetPlaneFacilityAsync(Guid id);

    [Post("/api/PlaneFacility")]
    [Headers("Authorization: Bearer")]
    Task CreatePlaneFacilityAsync(PlaneFacility planeFacility);

    [Put("/api/PlaneFacility")]
    [Headers("Authorization: Bearer")]
    Task UpdatePlaneFacilityAsync(PlaneFacility planeFacility);

    [Delete("/api/PlaneFacility/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeletePlaneFacilityAsync(Guid id);

    [Get("/api/PlaneSeat")]
    Task<List<PlaneSeat>> GetPlaneSeatsAsync();

    [Get("/api/PlaneSeat/{id}")]
    Task<PlaneSeat> GetPlaneSeatAsync(Guid id);

    [Post("/api/PlaneSeat")]
    [Headers("Authorization: Bearer")]
    Task CreatePlaneSeatAsync(PlaneSeat planeSeat);

    [Put("/api/PlaneSeat")]
    [Headers("Authorization: Bearer")]
    Task UpdatePlaneSeatAsync(PlaneSeat planeSeat);

    [Delete("/api/PlaneSeat/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeletePlaneSeatAsync(Guid id);

    [Get("/api/Booking")]
    [Headers("Authorization: Bearer")]
    Task<List<Booking>> GetBookingsAsync();

    [Get("/api/Booking/{id}")]
    [Headers("Authorization: Bearer")]
    Task<Booking> GetBookingAsync(Guid id);

    [Post("/api/Booking")]
    [Headers("Authorization: Bearer")]
    Task CreateBookingAsync(Booking booking);

    [Put("/api/Booking")]
    [Headers("Authorization: Bearer")]
    Task UpdateBookingAsync(Booking booking);

    [Delete("/api/Booking/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteBookingAsync(Guid id);
}

