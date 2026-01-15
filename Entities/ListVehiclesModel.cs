using api_dotNet_vehicles.Models;

namespace API_VehiclesAPP.Models
{
    public class ListVehiclesModel
    {
        public List<CarsDetModel> CarsDetModel { get; set; }
        public List<BikesDetModel> BikesDetModel { get; set; }

    }
}
