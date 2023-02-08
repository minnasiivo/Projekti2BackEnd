using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
   public interface IReservationService
    {
        public Task<ReservationDTO> CreateReservationAsync(ReservationDTO res, string username);
     
        public Task<IEnumerable<ReservationDTO>> GetAllReservations();
        public Task<IEnumerable<ReservationDTO>> GetReservationForUser(String username);

        public Task<ReservationDTO> UpdateReservation(ReservationDTO reservation);

        public Task<IEnumerable<ReservationDTO>> GetReservation(long id);

        public Task<ReservationDTO> DeleteReservation(ReservationDTO reservation);
   



    }
}
