using System.Collections.Generic;

namespace CareHomeTaskManager.Core
{
    public interface ICareTaskManager
    {
        IEnumerable<CareTask> GetAllTasks();
        CareTask GetWithId(int id);
        void Save(CareTask careTask);
    }
}