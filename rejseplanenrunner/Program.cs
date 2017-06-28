using System;
using System.Linq;
using System.Threading.Tasks;
using rejseplanencore;

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
            //var datetime = new DateTime(2017, 04, 25, 20, 40, 0);
            //var blaaa = tripService.GetDepartureBoard(awayLoc.id, datetime, 20);
            var trips = await tripService.GetTrips(homeLoc.id, awayLoc.id);
            var trip = trips.Trip.First();
            var detail = await tripService.GetJourneyDetail(trip.Leg.First().JourneyDetailRef);
            var allLegs = trips.Trip.Select(x => tripService.GetJourneyDetail(x.Leg.First()
                                                                               .JourneyDetailRef)).ToList();
            Task.WaitAll(allLegs);
            var rt = detail.Stop.First(x => x.name == homeLoc.name);
            if (rt != null)
            {
                // Console.WriteLine(rt.)
                int i = 0;
            }
        }
    }
}
