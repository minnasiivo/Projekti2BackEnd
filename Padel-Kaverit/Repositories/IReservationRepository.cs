using Padel_Kaverit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Repositories
{
    public interface IReservationRepository
    {
        public Task<Reservation> AddReservationAsync(Reservation res);

        public Task<Reservation> GetReservationAsync(long id);
        public Task<IEnumerable<Reservation>> GetReservationAsync();
       public Task<IEnumerable<Reservation>> GetResevationAsync(string target, DateTime start, DateTime end);
        public Task<IEnumerable<Reservation>> GetReservationsAsync(User user);
        public Task<Reservation> UpdateReservation(Reservation reservation);

        public Task<Reservation> RemoveReservation(Reservation reservation);
    }
}
