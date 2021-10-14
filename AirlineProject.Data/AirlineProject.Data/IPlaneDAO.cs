using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public interface IPlaneDAO
    {
        public IEnumerable<Plane> GetPlanes();

        public Plane GetPlane(int id);

        public void AddPlane(Plane plane);

        public void DeletePlane(int id);

        public void UpdatePlane(Plane plane);
    }
}
