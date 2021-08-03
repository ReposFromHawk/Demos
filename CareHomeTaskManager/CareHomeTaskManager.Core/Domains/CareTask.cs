using CareHomeTaskManager.Core.Validation;
using CareHomeTaskManager.Core.Validation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CareHomeTaskManager.Core
{
    public class CareTask
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(450)]
        public string Title { get; set; }
        [MaxLength(450)]
        public string PatientName { get; set; }
        [MaxLength(450)]
        public string CreatedByUser { get; set; }
        public DateTime ActualStartDateTime{ get; set; }
        public DateTime TargetDateTime { get; set; }
        [MaxLength(1000)]
        public string Reason { get; set; }
        [MaxLength(1000)]
        public string Action { get; set; }
        [MaxLength(1000)]
        public string Frequency { get; set; }
        public bool Completed { get; set; }
        public DateTime? EndDateTime { get; set; }
        [MaxLength(1000)]
        public string Outcome { get; set; }       
       
    }
}