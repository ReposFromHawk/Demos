using CareHomeTaskManager.Core.DataInterface;
using System;
using System.Collections.Generic;

namespace CareHomeTaskManager.Core
{
    public class CareTaskManager : ICareTaskManager
    {
        private readonly ICareTaskRepository _repo;
        public CareTaskManager(ICareTaskRepository repository)
        {
            _repo = repository;
        }

        public IEnumerable<CareTask> GetAllTasks()
        {
            return _repo.GetAll();
        }

        public void Save(CareTask careTask)
        {
            _repo.Save(careTask);
        }

        public CareTask GetWithId(int id)
        {
            return _repo.Get(id);

        }
    }
}