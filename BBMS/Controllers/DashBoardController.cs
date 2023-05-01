using BBMS.Data;
using BBMS.Models;
using BBMS.Repository;
using BBMS.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly BloodBankDbContext _context;
        private readonly IBloodGroupRepository _bloodGroupRepository;
        private readonly IUserService _userService;

        public DashBoardController(BloodBankDbContext context, IBloodGroupRepository bloodGroupRepository,IUserService userService)
        {
            _context = context;
            _bloodGroupRepository = bloodGroupRepository;
            _userService = userService;
        }
        public IActionResult Index()
        {
            if (this.User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "DashBoard", new { area = "admin" });
            }
            var RequestList = _context.PetientDetails.ToList();//.Where(u => u.UserId.Equals(_userService.GetUserId())).Select(s => s.RequestStatus.ToString() == "Pending");
            ViewBag.PendingRequest = RequestList.Where(u => u.UserId.Equals(_userService.GetUserId()) && u.RequestStatus.ToString() == "Pending").Select(s => s.RequestStatus.ToString() == "Pending").Count();
            ViewBag.AcceptRequest = RequestList.Where(u => u.UserId.Equals(_userService.GetUserId()) && u.RequestStatus.ToString() == "Accepted").Select(s => s.RequestStatus.ToString() == "Accepted").Count();
            ViewBag.RejectRequest = RequestList.Where(u => u.UserId.Equals(_userService.GetUserId()) && u.RequestStatus.ToString() == "Rejected").Select(s => s.RequestStatus.ToString() == "Rejected").Count();
            ViewBag.TotalRequest = RequestList.Where(u => u.UserId.Equals(_userService.GetUserId())).Select(s => s.RequestStatus).Count();
            //var result = _bloodGroupRepository.GetRequestStatusCount();
            return View();
        }
        [Route("makerequest")]
        public IActionResult MakeRequest(bool isSuccess=false)
        {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }

        [HttpPost("makerequest")]
        public async Task<IActionResult> MakeRequest(PetientRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _bloodGroupRepository.AddPetientRequest(model);
                return RedirectToAction(nameof(MakeRequest), new { isSuccess=true });
            }
            return View();
        }

        [Route("getallrequest")]
        public async Task<IActionResult> GetAllRequest()
        {
            var data = await _bloodGroupRepository.GetPetientRequestDetails();
            return View(data);
        }
        [Route("donate-request")]
        public IActionResult DonateRequest(bool isSuccess=false)
        {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }
        [HttpPost("donate-request")]
        public async Task<IActionResult> DonateRequest(DonateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _bloodGroupRepository.AddDonateRequest(model);
                return RedirectToAction(nameof(DonateRequest), new { isSuccess = true });
            }
            return View();
        }
        [Route("getdonate-request")]
        public async Task<IActionResult> GetAllDonateRequest()
        {
            var data = await _bloodGroupRepository.GetDonateRequestDetails();
            return View(data);
        }
    }
}
