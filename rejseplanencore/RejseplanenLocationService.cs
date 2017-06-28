using System.Collections.Generic;
using System.Threading.Tasks;
using rejseplanencore.Schemas;

namespace rejseplanencore
{
    public class RejseplanenLocationService : RejseplanenServiceBase
    {
        public async Task<LocationList> GetLocations(string query)
        {
            var request = new RestRequest("location")
                .AddQueryParameter("input", query);

            var response = await Client.Execute<LocationList>(request);
            return response.Data;
        }

        public async Task<LocationList> GetStopsNearby(int coordX, int coordY, int maxRadiusInMeters, int maxNumber)
        {
            var request = new RestRequest("stopsNearby")
                .AddQueryParameter("coordX", coordX.ToString())
                .AddQueryParameter("coordY", coordY.ToString())
                .AddQueryParameter("maxRadius", maxRadiusInMeters.ToString())
                .AddQueryParameter("maxNumber", maxNumber.ToString());


            var response = await Client.Execute<LocationList>(request);
            return response.Data;
        }
    }
}