using Infra.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkiTripController(SkiTripService skiTripService) : ControllerBase
    {

        [HttpGet(nameof(GetAllTrips))]
        public List<SkiTrip> GetAllTrips()
        {
            return skiTripService.GetAllTrips();
        }

        [HttpGet(nameof(GetTripById))]
        public SkiTrip GetTripById(int id)
        {
            return skiTripService.GetTripById(id);
        }

        [HttpPost(nameof(CreateTrip))]
        public SkiTrip CreateTrip(SkiTrip trip)
        {
            return skiTripService.CreateTrip(trip);
        }

        [HttpPut(nameof(UpdateTrip))]
        public SkiTrip UpdateTrip(int id, SkiTrip trip)
        {
            return skiTripService.UpdateTrip(id, trip);
        }

        [HttpDelete(nameof(DeleteTrip))]
        public void DeleteTrip(int id)
        {
            skiTripService.DeleteTrip(id);
        }

        [HttpGet(nameof(GetTripsByUserId))]
        public List<SkiTrip> GetTripsByUserId(int userId)
        {
            return skiTripService.GetTripsByUserId(userId);
        }

        [HttpGet(nameof(GetTripsByLocation))]
        public List<SkiTrip> GetTripsByLocation(string location)
        {
            return skiTripService.GetTripsByLocation(location);
        }
    }


}
