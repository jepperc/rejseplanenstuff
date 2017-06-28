using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using rejseplanencore.Schemas;

namespace rejseplanencore
{
    

    public class RejseplanenTripService : RejseplanenServiceBase
    {
        private readonly bool _useBus;
        private readonly bool _useTog;
        private readonly bool _useMetro;

        public RejseplanenTripService()
        {
            _useBus = false;
            _useTog = true;
            _useMetro = false;
        }
        public async Task<TripList> GetTrips(string originId, string destId, DateTime? datetime = null)
        {
            var request = new RestRequest("trip")
                .AddQueryParameter("originId", originId)
                .AddQueryParameter("destId", destId)
                .AddQueryParameter("useTog", _useTog.ToIntString())
                .AddQueryParameter("useBus", _useBus.ToIntString())
                .AddQueryParameter("useMetro", _useMetro.ToIntString());

            if (datetime.HasValue)
            {
                request = request.AddQueryParameter("time", datetime.Value.ToString("hh\\:mm"));

                request = request.AddQueryParameter("date", datetime.Value.ToString("dd.MM.yy"));
            }

            var response = await Client.Execute<TripList>(request);
            return response.Data;
        }

        public async Task<JourneyDetail> GetJourneyDetail(JourneyDetailRef detRef)
        {
            var url = detRef.@ref;

            var request = new RestRequest(new Uri(url, UriKind.Absolute));

            var response = await Client.Execute<JourneyDetail>(request);
            return response.Data;
        }

        public async Task<MultiDepartureBoard> GetDepartureBoard(string[] ids, DateTime? dateAndTime = null, int? offsetTime = null)
        {
            var request = new RestRequest("multiDepartureBoard");
            for (var index = 0; index < ids.Length; index++)
            {
                request = request.AddQueryParameter("id" + (index + 1), ids[index]);
            }
            if (dateAndTime.HasValue)
            {
                request = request.AddQueryParameter("date", dateAndTime.Value.ToString("dd.MM.yy"));

                if (offsetTime.HasValue)
                    request = request.AddQueryParameter("offsetTime", offsetTime.Value.ToString());
                else
                    request = request.AddQueryParameter("time", dateAndTime.Value.ToString("hh\\:mm"));
            }
            request = request.AddQueryParameter("useTog", _useTog.ToIntString())
                             .AddQueryParameter("useBus", _useBus.ToIntString())
                             .AddQueryParameter("useMetro", _useMetro.ToIntString());

            var response = await Client.Execute<MultiDepartureBoard>(request);
            return response.Data;
        }

        public Task<MultiDepartureBoard> GetDepartureBoard(string id, DateTime? dateAndTime = null, int? offsetTime = null)
        {
            return GetDepartureBoard(new[] { id }, dateAndTime, offsetTime);
        }
    }
}
