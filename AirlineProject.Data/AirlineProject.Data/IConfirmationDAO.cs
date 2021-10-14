using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject.Data
{
    public interface IConfirmationDAO
    {
        public IEnumerable<Confirmation> GetConfirmations();

        public Confirmation GetConfirmation(int id);

        public void AddConfirmation(Confirmation confirmation);

        public void DeleteConfirmation(int id);

        public void UpdateConfirmation(Confirmation confirmation);
    }
}
