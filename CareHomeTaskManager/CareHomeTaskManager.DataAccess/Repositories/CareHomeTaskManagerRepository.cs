using CareHomeTaskManager.Core;
using CareHomeTaskManager.Core.DataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.DataAccess.Repositories
{
    public class CareHomeTaskManagerRepository : ICareTaskRepository
    {
        private readonly CareHomeTaskManagerContext _context;
        public CareHomeTaskManagerRepository(CareHomeTaskManagerContext context)
        {
            _context = context;
        }

        public CareTask Get(int id)
        {
            return _context.CareTasks.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CareTask> GetAll()
        {
            return _context.CareTasks.ToList();
        }

        public void Save(CareTask careTask)
        {
            if (_context.CareTasks.FirstOrDefault(x => x.Id == careTask.Id) == null)
            {
                _context.CareTasks.Add(careTask);
            }
            else
            {
                _context.CareTasks.Update(careTask);
            }
            _context.SaveChanges();
        }
    }
}
