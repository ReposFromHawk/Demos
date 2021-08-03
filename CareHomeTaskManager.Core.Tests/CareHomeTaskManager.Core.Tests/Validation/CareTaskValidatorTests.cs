using CareHomeTaskManager.Core.Validation.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Core.Tests.Validator
{
    public class CareTaskValidatorTests
    {
        public CareTaskValidatorTests()
        {
            _sut = new CareTaskValidator();
        }

        private CareTask CreateCareTask()
        {
            return new CareTask
            {
                Action = "This is an action",
                ActualStartDateTime = new DateTime(2021, 08, 03),
                Completed = false,
                CreatedByUser = "here is the user",
                EndDateTime = null,
                Frequency = null,
                Id = 1,
                Outcome = "",
                PatientName = "This is patients name",
                Reason = "Reason is ok",
                TargetDateTime = DateTime.Now.AddDays(2),
                Title = "This is the title"
            };
        }

        private CareTaskValidator _sut;
        
        [Test]
        [TestCase(null,false, "'Title' must not be empty.")]
        [TestCase("There is a title", true,null)]
        public void ShouldValidateTitleProperty(string title,bool expectedResult,string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.Title = title;
            var res= _sut.Validate(ct);
            Assert.That(res.IsValid,Is.EqualTo(expectedResult));
            if(!expectedResult)
            Assert.That(res.Errors.FirstOrDefault(x => x.PropertyName == "Title").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
        [Test]
        [TestCase(null, false, "'Patient Name' must not be empty.")]
        [TestCase("Paul Backs", true, null)]
        public void ShouldValidatePatientNameProperty(string patientName, bool expectedResult, string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.PatientName = patientName;
            var res = _sut.Validate(ct);
            Assert.That(res.IsValid, Is.EqualTo(expectedResult));
            if (!expectedResult)
                Assert.That(res.Errors.FirstOrDefault(x=>x.PropertyName=="PatientName").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
        [Test]
        [TestCase(null, false, "'Created By User' must not be empty.")]
        [TestCase("Erdem", true, null)]
        public void ShouldValidateCreatedByUSerProperty(string createdByUser, bool expectedResult, string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.CreatedByUser = createdByUser;
            var res = _sut.Validate(ct);
            Assert.That(res.IsValid, Is.EqualTo(expectedResult));
            if (!expectedResult)
                Assert.That(res.Errors.FirstOrDefault(x => x.PropertyName == "CreatedByUser").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
        [Test]
        [TestCase(null, false, "'' must not be equal to '0001-01-01 00:00:00'.")]
        [TestCase("2021-08-08 00:00:00", true, null)]
        public void ShouldValidateActualStartDateTimeProperty(string actualStartDateTime, bool expectedResult, string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.ActualStartDateTime = Convert.ToDateTime(actualStartDateTime);
            var res = _sut.Validate(ct);
            Assert.That(res.IsValid, Is.EqualTo(expectedResult));
            if (!expectedResult)
                Assert.That(res.Errors.FirstOrDefault(x => x.PropertyName == "").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
        [Test]
        [TestCase(null, false, "'' must not be equal to '0001-01-01 00:00:00'.")]
        [TestCase("2021-08-08 00:00:00", true, null)]
        public void ShouldValidateTargetDateTimeProperty(string targetDateTime, bool expectedResult, string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.TargetDateTime = Convert.ToDateTime(targetDateTime);
            var res = _sut.Validate(ct);
            Assert.That(res.IsValid, Is.EqualTo(expectedResult));
            if (!expectedResult)
                Assert.That(res.Errors.FirstOrDefault(x => x.PropertyName == "").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
        [Test]
        [TestCase(null, false, "'Reason' must not be empty.")]
        [TestCase("This is the reason", true, null)]
        public void ShouldValidateReasonProperty(string reason, bool expectedResult, string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.Reason = reason;
            var res = _sut.Validate(ct);
            Assert.That(res.IsValid, Is.EqualTo(expectedResult));
            if (!expectedResult)
                Assert.That(res.Errors.FirstOrDefault(x => x.PropertyName == "Reason").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
        [Test]
        [TestCase(null, false, "'Action' must not be empty.")]
        [TestCase("This is the action", true, null)]
        public void ShouldValidateActionProperty(string action, bool expectedResult, string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.Action = action;
            var res = _sut.Validate(ct);
            Assert.That(res.IsValid, Is.EqualTo(expectedResult));
            if (!expectedResult)
                Assert.That(res.Errors.FirstOrDefault(x => x.PropertyName == "Action").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
        [Test]
        [TestCase(null, true, null)]
        [TestCase("This is the frequency", true, null)]
        public void ShouldValidateFrequencyProperty(string frequency, bool expectedResult, string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.Frequency = frequency;
            var res = _sut.Validate(ct);
            Assert.That(res.IsValid, Is.EqualTo(expectedResult));
            if (!expectedResult)
                Assert.That(res.Errors.FirstOrDefault(x => x.PropertyName == "Action").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
        [Test]
        [TestCase(null,true,"outcome value", false, "'End Date Time' must not be empty.")]//Completed but enddatenotset
        [TestCase("2021-08-08 00:00:00",true,"outcome value", true, null)]//completed and set
        [TestCase("2021-08-03 00:00:00", true, "outcome value", true, null)]//completed and set to same date of actualstart
        [TestCase("2021-08-01 00:00:00", true, "outcome value", false, "'End Date Time' must be greater than or equal to '03/08/2021 00:00:00'.")]//completed but set before actual start
        public void ShouldValidateEndDateTimeProperty(string endDateTime,bool completed,string outcome ,bool expectedResult, string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.Completed = completed;
            ct.Outcome = outcome;
            if(endDateTime!=null)
            ct.EndDateTime = Convert.ToDateTime(endDateTime);
            var res = _sut.Validate(ct);
            Assert.That(res.IsValid, Is.EqualTo(expectedResult));
            if (!expectedResult)
                Assert.That(res.Errors.FirstOrDefault(x => x.PropertyName == "EndDateTime").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
        [Test]
        [TestCase(null,true ,false, "'Outcome' must not be empty.")]
        [TestCase(null, false, true, null)]
        [TestCase("This is the outcome", true,true, null)]
        public void ShouldValidateOutcomeProperty(string outcome, bool completed,bool expectedResult, string expectedErrorMessage)
        {
            CareTask ct = CreateCareTask();
            ct.Completed = completed;
            ct.Outcome = outcome;
            if (completed) { ct.EndDateTime = DateTime.Now.AddDays(10); }
            var res = _sut.Validate(ct);
            Assert.That(res.IsValid, Is.EqualTo(expectedResult));
            if (!expectedResult)
                Assert.That(res.Errors.FirstOrDefault(x => x.PropertyName == "Outcome").ErrorMessage, Is.EqualTo(expectedErrorMessage));
        }
    }
}
#region Care Plan validation requirements
    //Field             Type	        Mandatory	    Rules	    Example
    //Title	            Nvarchar(450)	Yes                         Feeding care plan
    //PatientName	    Nvarchar(450)	Yes                         Dorris Day
    //Created By User	Nvarchar(450)	Yes                         Alex.savage
    //ActualStartDate/Time Date/Time    Yes		                    30/07/2021
    //Target Date/Time Date/Time        Yes		                    10/08/2021
    //Reason            Nvarchar(1000)  Yes                         Ensure client’s weight is maintained
    //Action	        Nvarchar(1000)	Yes                         3 hot meals each day and encourage the client to eat at least 75% of each meal. Provide 1 glass of water with each meal.
    //Frequency	        Nvarchar(1000)	No                          3 times a day
    //CompletedPicklist (Yes, No)	    No
    //End Date/Time	    Date/Time	    No	            Cannot be before Start Date/Time. Visible and mandatory when “Completed” is set to Yes	10/09/2021
    //Outcome	        Nvarchar(1000)	No              Visible and mandatory when “Completed” is set to Yes	

#endregion