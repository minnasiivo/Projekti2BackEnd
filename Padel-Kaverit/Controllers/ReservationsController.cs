using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padel_Kaverit.Middleware;
using Padel_Kaverit.Models;
using Padel_Kaverit.Repositories;
using Padel_Kaverit.Services;

namespace Padel_Kaverit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly PadelContext _context;
        private readonly IReservationService _service;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;

        public ReservationsController(PadelContext context, IReservationService service, IUserAuthenticationService authenticationService, IUserRepository userRepository)
        {
            _service = service;
            _authenticationService = authenticationService;
            _context = context;
            _userRepository = userRepository;
        }

        // GET: api/Reservations
        /// <summary>
        /// Get list of all reservations
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return Ok(await _service.GetAllReservations());
        }

        // GET: api/Reservations/5
        /// <summary>
        /// Get reservations based on reservation id
        /// </summary>
        [HttpGet("{username}")]
        [Authorize]
      
        public async Task<ActionResult<Reservation>> GetReservation(string username)
        {
            var reservation = await _service.GetReservationForUser(username);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// change reservation information
        /// </summary>
        [HttpPut]
        
        public async Task<IActionResult> PutReservation(ReservationDTO reservation)
        {
     


            ReservationDTO updateReservation = await _service.UpdateReservation(reservation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return NoContent(); ;
        }



        /// <summary>
        /// Make reservation to calendar
        /// </summary>
        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservation)
        {
            string username = this.User.FindFirst(ClaimTypes.Name).Value;
          

            bool isAllowed = await _authenticationService.IsAllowed(this.User.FindFirst(ClaimTypes.Name).Value, reservation);
            if (!isAllowed)
            { return Unauthorized(); }

            ReservationDTO newReservation = await _service.CreateReservationAsync(reservation,username);
            if (reservation == null)
            {
                return StatusCode(500);
            }

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }



        /// <summary>
        /// Delete reservation
        /// </summary>
        // DELETE: api/Reservations/5
        [HttpDelete]
        [Authorize]
      
        public async Task<IActionResult> DeleteReservation(ReservationDTO reservationDTO)
        {

            try
            {
                await _service.DeleteReservation(reservationDTO);
            }

            catch { return null; }

            return Ok("Deleted");
        }

        private bool ReservationExists(long id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
