using System;
using System.Linq;
using System.Threading.Tasks;
using rejseplanencore;
using Newtonsoft.Json;

namespace rejseplanenrunner
{
    class Program
    {
        private const string HomeStation = "hørning st";
        private const string AwayStation = "aarhus h";
        private static TimeSpan EarlyLeave = TimeSpan.FromHours(6.7);
        private static TimeSpan LateLeave = TimeSpan.FromHours(8.6);
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var m = new Program();

            m.DoStuff().Wait();
        }

        protected async Task DoStuff()
        {
            var api = new RejseplanenLocationService();
            var locationList = await api.GetLocations(HomeStation);
            var homeLoc = locationList.StopLocations
                           .First();

            var awayLoc = (await api.GetLocations(AwayStation)).StopLocations
                             .First();

            var tripService = new RejseplanenTripService();
            var datetime = new DateTime(2017, 06, 28, 07, 40, 0);
            //var blaaa = tripService.GetDepartureBoard(awayLoc.id, datetime, 20);
            var trips = await tripService.GetTrips(homeLoc.id, awayLoc.id, datetime);
            // var trip = trips.Trip.First();
            // var detail = await tripService.GetJourneyDetail(trip.Leg.First().JourneyDetailRef);
            // var allLegTasks = trips.Trip.Select(x => tripService.GetJourneyDetail(x.Leg.First()
            //                                                                    .JourneyDetailRef)).ToArray();
            // var allLegs = await Task.WhenAll(allLegTasks);


            foreach(var trip in trips.Trip)
            {
                var jsont = JsonConvert.SerializeObject(trip, Formatting.Indented);
                Console.WriteLine(jsont);
                var detail = await tripService.GetJourneyDetail(trip.Leg.First().JourneyDetailRef);
                var rt = detail.Stop.First(x => x.name == homeLoc.name);
                if (rt != null)
                {
                    var json = JsonConvert.SerializeObject(rt, Formatting.Indented);
                    Console.WriteLine(json);
                }
            }
            // var rt = detail.Stop.First(x => x.name == homeLoc.name);
            // if (rt != null)
            // {
            //     var json = JsonConvert.SerializeObject(rt, Formatting.Indented);
            //     Console.WriteLine(json);
            // }
        }
    }
}
