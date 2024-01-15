using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusStation.MODEL;
using BusStation.UI;

namespace BusStation.DAL
{
    public class BusStationDbStorage
    {
        private readonly BusStationContext _context;


        public BusStationDbStorage(BusStationContext context)
        {
            _context = context;
        }

        public List<Bus> GetAllBuses()
        {
            return _context.Buses.ToList<Bus>();
        }

        public List<Passenger> GetAllPassengers()
        {
            return _context.Passengers.ToList<Passenger>();
        }
        public List<Route> GetAllRoutes()
        {
            return _context.Routes.ToList<Route>();
        }

        public List<Run> GetAllRuns()
        {
            return _context.Runs.ToList<Run>();
        }

        public Bus GetBusById(int BusId)
        {
            foreach(Bus b in _context.Buses)
            {
                if (b.BusId == BusId)
                   return b;
            }
            throw new ArgumentNullException();
        }

        public Passenger GetPassengerById(int PassengerId)
        {
            foreach(Passenger p in _context.Passengers)
            {
                if (p.PassengerId == PassengerId)
                    return p;
            }
            throw new ArgumentNullException();
        }

        public Route GetRouteById(int RouteId)
        {
            foreach(Route r in _context.Routes)
            {
                if (r.RouteId == RouteId)
                    return r;
            }
            throw new ArgumentNullException();
        }

        public Run GetRunById(int RunId)
        {
            foreach(Run r in _context.Runs)
            {
                if(r.RunId == RunId)
                    return r;
            }
            throw new ArgumentNullException();
        }

        public Bus AddBus(Bus Bus)
        {
            _context.Add(Bus);
            _context.SaveChanges();
            return _context.Buses.ToList<Bus>()[_context.Buses.Count() - 1];
        }

        public Passenger AddPassenger(Passenger Passenger)
        {
            _context.Add(Passenger);
            _context.SaveChanges();
            return _context.Passengers.ToList<Passenger>()[_context.Passengers.Count() - 1];
        }

        public Route AddRoute(Route Route)
        {
            _context.Add(Route);
            _context.SaveChanges();
            return _context.Routes.ToList<Route>()[_context.Routes.Count() - 1];
        }

        public Run AddRun(Run Run)
        {
            _context.Add(Run);
            _context.SaveChanges();
            return _context.Runs.ToList<Run>()[_context.Routes.Count() - 1];
        }

        public void RemoveBus(Bus bus)
        {
            if (bus != null)
            {
                _context.Remove(bus);
                _context.SaveChanges();
            }
        }

        public void RemovePassenger(Passenger passenger)
        {
            if (passenger != null)
            {
                _context.Remove(passenger);
                _context.SaveChanges();
            }
        }

        public void RemoveRoute(Route route)
        {
            if (route != null)
            {
                _context.Remove(route);
                _context.SaveChanges();
            }
        }

        public void RemoveRun(Run run)
        {
            if (run != null)
            {
                _context.Remove(run);
                _context.SaveChanges();
            }
        }

        public void RemoveBusById(int BusId)
        {
            _context.Remove(GetBusById(BusId));
            _context.SaveChanges();
        }

        public void RemovePassengerById(int PassengerId)
        {
            _context.Remove(GetPassengerById(PassengerId));
            _context.SaveChanges();
        }

        public void RemoveRouteById(int RouteId)
        {
            _context.Remove(GetRouteById(RouteId));
            _context.SaveChanges();
        }

        public void RemoveRunById(int RunId)
        {
            _context.Remove(GetRunById(RunId));
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

     

    }
}
