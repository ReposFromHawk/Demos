using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Core.DataInterface
{
    public interface ICareTaskRepository
    {
        void Save(CareTask careTask);
        IEnumerable<CareTask> GetAll();
        CareTask Get(int id);
    }
}
