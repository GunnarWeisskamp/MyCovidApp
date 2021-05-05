using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APICall.CustomeAttributes;
using APICall.Model;
using APICall.Services;
using EntityRepo.ContextInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = APICall.CustomeAttributes.AuthorizeAttribute;

namespace APICall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientDetailsController : ControllerBase
    {
        private readonly IPatientDetailsActions _patDet;
        private readonly IPatientStoredProcedureActions _patStor;
        public IResultSimpleAPI _resSimpleCls;
        public PatientDetailsController(IPatientDetailsActions patDet, IPatientStoredProcedureActions patStor,
                                        IResultSimpleAPI resSimpleCls, IPatientDetailsAPI patDetails)
        {
            _patDet = patDet; _patStor = patStor; _resSimpleCls = resSimpleCls;
        }

        [HttpGet]
        [Route("GetPatientHospitalAddressAndNextOfKinDetailsById")]
        public async Task<IActionResult> GetPatientHospitalAddressAndNextOfKinDetailsById(int id)
        {
            IActionResult ret;
            try
            {
                var patDetailsById = await _patDet.GetPatientHospitalAddressAndNextOfKinDetailsById(id);
                ret = StatusCode(StatusCodes.Status200OK, patDetailsById);
            }
            catch (Exception ex)
            {
                ret = StatusCode(StatusCodes.Status500InternalServerError, "An error has happened. Please contact admin stating the following: " + ex.Message);
            }
            return ret;
        }


        [HttpGet]
        [Route("GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            IActionResult ret;
            try
            {
                var allPatDetails = await _patDet.GetAllPatients();
                ret = StatusCode(StatusCodes.Status200OK, allPatDetails);
            }
            catch (Exception ex)
            {
                ret = StatusCode(StatusCodes.Status500InternalServerError, "An error has happened. Please contact admin stating the following: " + ex.Message);
            }
            return ret;
        }
        [HttpGet]
        [Route("GetPatientDetailsByFirstNameAndLastName")]
        public async Task<IActionResult> GetPatientDetailsByFirstNameAndLastName(string firstName, string lastName)
        {
            IActionResult ret;
            try
            {
                var patDetailsByFirstAndLastName = await _patDet.GetPatientDetailsByFirstNameAndLastName(firstName, lastName);
                ret = StatusCode(StatusCodes.Status200OK, patDetailsByFirstAndLastName);
            }
            catch (Exception ex)
            {
                ret = StatusCode(StatusCodes.Status500InternalServerError, "An error has happened. Please contact admin stating the following: " + ex.Message);
            }
            return ret;
        }

        [HttpPost]
        [Authorize("CanAccessInsertPatientPage")]
        [Route("InsertNewPatientDetailsAndAddress")]
        public async Task<IResultSimpleAPI> sp_InsertNewPatientDetailsAndAddress(InsertNewPatientAPI newPatient)
        {
            try
            {
                string result = await _patStor.sp_InsertNewPatientDetailsAndAddress(newPatient.FirstName, newPatient.LastName,
                                                                                    newPatient.Age, newPatient.Street, newPatient.Suburb,
                                                                                    newPatient.State, newPatient.HospitalName,
                                                                                    newPatient.DateOfTest, newPatient.HealthCarerName,
                                                                                    newPatient.Results, newPatient.Notes);
                if (result.Contains("SQL error has occurred"))
                {
                    // sql error has happened
                    _resSimpleCls.EndMessage = result;
                    _resSimpleCls.IsError = true;
                }
                else
                {
                    _resSimpleCls.EndMessage = result;
                    _resSimpleCls.IsError = false;
                }
            }
            catch (Exception ex)
            {
                _resSimpleCls.EndMessage = "Error has happened please contact customer support with the following error code: " + ex.Message; ;
                _resSimpleCls.IsError = true;
            }

            return _resSimpleCls;
        }

        [HttpPost]
        [Authorize("CanAccessEditPatientPage")]
        [Route("UpdatePatientAddress")]
        public async Task<IResultSimpleAPI> UpdatePatientAddress(UpdatePatientAPI updatePatient)
        {
            try
            {
                string result = await _patStor.sp_UpdatePatientAddress(updatePatient.Id, updatePatient.Street, updatePatient.Suburb, updatePatient.State);

                if (result.Contains("SQL error has occurred"))
                {
                    // sql error has happened
                    _resSimpleCls.EndMessage = result;
                    _resSimpleCls.IsError = true;
                }
                else
                {
                    _resSimpleCls.EndMessage = result;
                    _resSimpleCls.IsError = false;
                }
            }
            catch (Exception ex)
            {
                _resSimpleCls.EndMessage = "Error has happened please contact customer support with the following error code: " + ex.Message; ;
                _resSimpleCls.IsError = true;
            }

            return _resSimpleCls;
        }

        [HttpPost]
        [Route("UpdatePatientNextOfKin")]
        [Authorize("CanAccessEditPatientPage")]
        public async Task<IResultSimpleAPI> UpdatePatientNextOfKin(UpdatePatientNextOfKinAPI updatePatientNextOfKin)
        {
            try
            {
                string result = await _patStor.sp_UpdatePatientNextOfKin(updatePatientNextOfKin.Id, updatePatientNextOfKin.PhoneNumber,
                                                                         updatePatientNextOfKin.Name, updatePatientNextOfKin.Relationship);

                if (result.Contains("SQL error has occurred"))
                {
                    // sql error has happened
                    _resSimpleCls.EndMessage = result;
                    _resSimpleCls.IsError = true;
                }
                else
                {
                    _resSimpleCls.EndMessage = result;
                    _resSimpleCls.IsError = false;
                }
            }
            catch (Exception ex)
            {
                _resSimpleCls.EndMessage = "Error has happened please contact customer support with the following error code: " + ex.Message; ;
                _resSimpleCls.IsError = true;
            }

            return _resSimpleCls;
        }
    }
}
