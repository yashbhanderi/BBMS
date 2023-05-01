using BBMS.Models;
using BBMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Areas.Admin.Controllers
{
    [Area("admin")]
    public class DashBoard : Controller
    {
        private readonly IBloodGroupRepository _bloodGroupRepository;
        private readonly IAccountRepository _accountRepository;

        public DashBoard(IBloodGroupRepository bloodGroupRepository, IAccountRepository accountRepository)
        {
            _bloodGroupRepository = bloodGroupRepository;
            _accountRepository = accountRepository;
        }
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var result = await _bloodGroupRepository.GetAllBloodGroup();
            return View(result);
        }
        [Route("petientdetails")]
        public IActionResult PetientDetails()
        {
            var result = _accountRepository.GetPetientUserDetails();
            return View(result);
        }
        [Route("donordetails")]
        public IActionResult DonorDetails()
        {
            var result = _accountRepository.GetDonorUserDetails();
            return View(result);
        }

        [Route("donation")]
        public async Task<IActionResult> GetDonationDetails()
        {
            var result = await _bloodGroupRepository.GetDonationDetailsAdmin();
            
            return View(result);
        }

        [Route("blood-request")]
        public async Task<IActionResult> GetBloodRequestDetails(string isSuccess= null)
        {

            ViewBag.IsSuccess = isSuccess;
            var result = await _bloodGroupRepository.GetPetientRequestsAdmin();
            return View(result);
        }
        [Route("request-history")]
        public async Task<IActionResult> BloodRequestHistory(bool isSuccess = true)
        {
            var result = await _bloodGroupRepository.GetPetientRequestsAdmin();
            return View(result);
        }
        [HttpPost("accept-petientrequest")]
        public IActionResult AcceptRequestForPetient(int id)
        {
            string action = "accept";
            var result =_bloodGroupRepository.UpdateRequestStatusForPetient(id, action);
            if (!result)
            {
                return RedirectToAction("getbloodrequestdetails", "dashboard", new { area="admin",isSuccess = "failed" });
            }
            return RedirectToAction("getbloodrequestdetails", "dashboard", new { area = "admin", isSuccess = "Accept" }); 
        }

        [HttpPost("accept-donorrequest")]
        public IActionResult AcceptRequestForDoner(int id)
        {
            string action = "accept";
            var result = _bloodGroupRepository.UpdateRequestStatusForDonor(id, action);
            return RedirectToAction("getdonationdetails", "dashboard", new { area = "admin" });
        }

        [HttpPost("reject-petientrequest")]
        public IActionResult RejectRequestForPetient(int id)
        {
            string action = "reject";
            var result = _bloodGroupRepository.UpdateRequestStatusForPetient(id,action);
            if (!result)
            {
                return RedirectToAction("getbloodrequestdetails", "dashboard", new { area = "admin", isSuccess = "failed" });
            }
            return RedirectToAction("getbloodrequestdetails", "dashboard", new { area = "admin", isSuccess = "Reject" });
        }
        [HttpPost("reject-donorrequest")]
        public IActionResult RejectRequestForDoner(int id)
        {
            string action = "reject";
            var result = _bloodGroupRepository.UpdateRequestStatusForDonor(id, action);
            return RedirectToAction("getdonationdetails", "dashboard", new { area = "admin" });
        }
        [Route("update-stock")]
        public IActionResult UpdateBloodStock(bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }
        [HttpPost("update-stock")]
        public IActionResult UpdateBloodStock(BloodGroupModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _bloodGroupRepository.UpdateBloodStock(model);
                if (result)
                {
                    return RedirectToAction("updatebloodstock", "dashboard", new { area = "admin", isSuccess = true });
                }
            }
            return View();
        }

        [HttpPost("delete-user")]
        public IActionResult DeleteUser(string email)
        {
            _accountRepository.SendEmailforUserDeletion(email);
            return RedirectToAction("index", "dashboard", new { area = "admin" });
        }
    }
}
