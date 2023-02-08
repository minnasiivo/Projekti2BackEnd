using Padel_Kaverit.Models;
using Padel_Kaverit.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Padel_Kaverit.Services
{
    public class ReservationService : IReservationService

    {
        private readonly IReservationRepository _repository;
        private readonly IUserRepository _userRepository;

        public ReservationService(IUserRepository userRepository, IReservationRepository repository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }


        public async  Task<ReservationDTO> CreateReservationAsync(ReservationDTO res, string username)
        {
            res.Owner = username;

            if (res.Start >= res.End)
            {
                return null;
            }
            //lisää varaus

            Reservation newReservation = await DTOToReservation(res);

            await _repository.AddReservationAsync(newReservation);
            return ReservationToDTO(newReservation);
        }

        public async Task<ReservationDTO> DeleteReservation(ReservationDTO reservation)
        {
            Reservation res = await DTOToReservation(reservation);
            await _repository.RemoveReservation(res);
            return null;


        }

        public async Task<IEnumerable<ReservationDTO>> GetAllReservations()
        {
            IEnumerable<Reservation> reservations = await _repository.GetReservationAsync();
            List<ReservationDTO> reservationDTOs = new List<ReservationDTO>();
            foreach (Reservation r in reservations)
            {
                reservationDTOs.Add(ReservationToDTO(r));
            }
            return reservationDTOs;
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservation(long id)
        {
            IEnumerable<Reservation> reservations = await _repository.GetReservationAsync();
            List<ReservationDTO> reservationDTOs = new List<ReservationDTO>();
            foreach (Reservation r in reservations)
            {
                reservationDTOs.Add(ReservationToDTO(r));
            }
            return reservationDTOs;
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationForUser(string username)
        {
            User owner = await _userRepository.GetUserAsync(username);
            if (owner == null)
            {
                return null;
            }
            IEnumerable<Reservation> reservations = await _repository.GetReservationsAsync(owner);
            List<ReservationDTO> dTOs = new List<ReservationDTO>();
            foreach (Reservation r in reservations)
            {
                dTOs.Add(ReservationToDTO(r));
            }
            return dTOs;
        }

        public async Task<ReservationDTO> UpdateReservation(ReservationDTO reservation)
        {
            Reservation dbReservation = await _repository.GetReservationAsync(reservation.Id);
            dbReservation.Start = reservation.Start;
            dbReservation.End = reservation.End;
            dbReservation.Target = reservation.Target;

            Reservation updateReservation = await _repository.UpdateReservation(dbReservation);
            if (updateReservation == null)
            {
                return null;
            }
            return ReservationToDTO(updateReservation);
        }

        private ReservationDTO ReservationToDTO(Reservation res)
        {
            ReservationDTO reservationDTO = new ReservationDTO();
            reservationDTO.Id = res.Id;
            reservationDTO.Target = res.Target;
            reservationDTO.Owner = res.Owner.UserName;
            reservationDTO.Start = res.Start;
            reservationDTO.End = res.End;

            return reservationDTO;
        }

        private async Task<Reservation> DTOToReservation(ReservationDTO dto)
        {
            Reservation reservation = new Reservation();
            User owner = await _userRepository.GetUserAsync(dto.Owner);
          

            if (owner == null)
            {
                return null;
            }

            
            reservation.Target = dto.Target;
            reservation.Id = dto.Id;
            reservation.Start = dto.Start;
            reservation.End = dto.End;
            reservation.Owner = owner;

            return reservation;
        }
    }
}
