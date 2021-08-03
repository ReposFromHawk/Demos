using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Core.Validation.Validators
{
    public class CareTaskValidator : AbstractValidator<CareTask>
    {
        public CareTaskValidator()
        {
            RuleFor(a => a.Title)
                .NotNull().NotEmpty()
                .MaximumLength(450);
            RuleFor(a => a.PatientName)
               .NotNull().NotEmpty()
               .MaximumLength(450);
            RuleFor(a => a.CreatedByUser)
               .NotNull().NotEmpty()
               .MaximumLength(450);
            RuleFor(a => a.ActualStartDateTime.ToString("yyyy-MM-dd HH:mm:ss"))
               .NotNull().NotEmpty().NotEqual("0001-01-01 00:00:00");
            RuleFor(a => a.TargetDateTime.ToString("yyyy-MM-dd HH:mm:ss"))
               .NotNull().NotEmpty().NotEqual("0001-01-01 00:00:00");
            RuleFor(a => a.Reason)
               .NotNull().NotEmpty()
               .MaximumLength(1000);
            RuleFor(a => a.Action)
              .NotNull().NotEmpty()
              .MaximumLength(1000);
            RuleFor(a => a.Frequency)
              .MaximumLength(1000).When(s=>!string.IsNullOrWhiteSpace(s.Frequency));
            RuleFor(a => a.EndDateTime)
               .NotNull().NotEmpty().When(x => x.Completed)
               .GreaterThanOrEqualTo(s => s.ActualStartDateTime);
            RuleFor(a => a.Outcome)
              .NotNull().NotEmpty().When(x => x.Completed).MaximumLength(1000);
        }
    }
}
