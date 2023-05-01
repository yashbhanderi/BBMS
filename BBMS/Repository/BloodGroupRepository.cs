using BBMS.Data;
using BBMS.Models;
using BBMS.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBMS.Repository
{
    public class BloodGroupRepository : IBloodGroupRepository
    {
        private readonly BloodBankDbContext _context;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BloodGroupRepository(BloodBankDbContext context,IUserService userService , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userService = userService;
            _userManager = userManager;
        }


        public async Task<List<BloodGroupModel>> GetAllBloodGroup()
        {
            return await _context.BloodStock.Select(x => new BloodGroupModel()
            {
                Id = x.Id,
                BloodGroup = x.BloodGroup,
                Unit = x.Units
            }).ToListAsync();
        }

        public async Task<int> AddPetientRequest(PetientRequestModel model)
        {
            
            var newReq = new PetientDetails()
            {
                UserId =_userService.GetUserId(),
                PatientName = model.PetientName,
                PatientAge = model.PetientAge,
                Disease = model.Disease,
                BloodGroupId = model.BloodGroupId,
                Unit = model.Unit,
                RequestStatus = RequestStatus.Pending,
                RequestDate = DateTime.UtcNow
            };
            await _context.AddAsync(newReq);
            await _context.SaveChangesAsync();
            return newReq.Id;
        }

        public async Task<List<PetientRequestModel>> GetPetientRequestDetails()
        {
            
            return await _context.PetientDetails.Where(e => e.UserId == _userService.GetUserId()).
                Select(req => new PetientRequestModel()
                {
                    Id=req.Id,
                    PetientName = req.PatientName,
                    PetientAge = req.PatientAge,
                    Disease = req.Disease,
                    BloodGroupId = req.BloodGroupId,
                    BloodGroup = (_context.BloodStock.Where(e=>e.Id==req.BloodGroupId).Select(e=>e.BloodGroup).FirstOrDefault()).ToString(),
                    Unit = req.Unit,
                    RequestDate = req.RequestDate,
                    RequestStatus = req.RequestStatus.ToString()
                }).ToListAsync();
        }
        public async Task<int> AddDonateRequest(DonateRequestModel model)
        {

            var newReq = new DonorDetails()
            {
                UserId = _userService.GetUserId(),
                DonorName = model.DonorName,
                DonorAge = model.DonorAge,
                Disease = model.Disease,
                BloodGroupId = model.BloodGroupId,
                Unit = model.Unit,
                RequestStatus = Status.Pending,
                RequestDate = DateTime.UtcNow
            };
            await _context.AddAsync(newReq);
            await _context.SaveChangesAsync();
            return newReq.Id;
        }
        public async Task<List<DonateRequestModel>> GetDonateRequestDetails()
        {

            return await _context.DonorDetails.Where(e => e.UserId == _userService.GetUserId()).
                Select(req => new DonateRequestModel()
                {
                    Id = req.Id,
                    DonorName = req.DonorName,
                    DonorAge = req.DonorAge,
                    Disease = req.Disease,
                    BloodGroupId = req.BloodGroupId,
                    BloodGroup = (_context.BloodStock.Where(e => e.Id == req.BloodGroupId).Select(e => e.BloodGroup).FirstOrDefault()).ToString(),
                    Unit = req.Unit,
                    RequestDate = req.RequestDate,
                    RequestStatus = req.RequestStatus.ToString()
                }).ToListAsync();
        }
        public async Task<List<DonateRequestModel>> GetDonationDetailsAdmin()
        {
            return await _context.DonorDetails.
                Select(req => new DonateRequestModel()
                {
                    Id = req.Id,
                    DonorName = req.DonorName,
                    DonorAge = req.DonorAge,
                    Disease = req.Disease,
                    BloodGroupId = req.BloodGroupId,
                    BloodGroup = (_context.BloodStock.Where(e => e.Id == req.BloodGroupId).Select(e => e.BloodGroup).FirstOrDefault()).ToString(),
                    Unit = req.Unit,
                    RequestDate = req.RequestDate,
                    RequestStatus = req.RequestStatus.ToString()
                }).ToListAsync();
        }
        public async Task<List<PetientRequestModel>> GetPetientRequestsAdmin()
        {
            return await _context.PetientDetails.
                Select(req => new PetientRequestModel()
                {
                    Id = req.Id,
                    PetientName = req.PatientName,
                    PetientAge = req.PatientAge,
                    Disease = req.Disease,
                    BloodGroupId = req.BloodGroupId,
                    BloodGroup = (_context.BloodStock.Where(e => e.Id == req.BloodGroupId).Select(e => e.BloodGroup).FirstOrDefault()).ToString(),
                    Unit = req.Unit,
                    RequestDate = req.RequestDate,
                    RequestStatus = req.RequestStatus.ToString()
                }).ToListAsync();
        }

        public bool UpdateRequestStatusForPetient(int id, string action)
        {
            var result = _context.PetientDetails.ToList().Where(x => x.Id == id).SingleOrDefault();
            var totalUnit = _context.BloodStock.ToList().Where(x => x.Id == result.BloodGroupId).FirstOrDefault();
            if (action == "reject")
            {
                _context.Entry(result).State = EntityState.Modified;
                result.RequestStatus = RequestStatus.Rejected;
                _context.SaveChanges();
                return true;
            }
            else
            {
                if (totalUnit.Units < result.Unit)
                {
                    return false;
                }
                else
                {
                    _context.Entry(totalUnit).State = EntityState.Modified;
                    totalUnit.Units = totalUnit.Units - result.Unit;
                    _context.Entry(result).State = EntityState.Modified;
                    result.RequestStatus = RequestStatus.Accepted;
                    _context.SaveChanges();
                    return true;
                }
            }
            
        }
        public bool UpdateRequestStatusForDonor(int id, string action)
        {
            var result = _context.DonorDetails.ToList().Where(x => x.Id == id).SingleOrDefault();
            var totalUnit = _context.BloodStock.ToList().Where(x => x.Id == result.BloodGroupId).FirstOrDefault();
            if (action == "reject")
            {
                _context.Entry(result).State = EntityState.Modified;
                result.RequestStatus = Status.Rejected;
                _context.SaveChanges();
                return true;
            }
            else
            {
                _context.Entry(totalUnit).State = EntityState.Modified;
                totalUnit.Units = totalUnit.Units + result.Unit;
                _context.Entry(result).State = EntityState.Modified;
                result.RequestStatus = Status.Accepted;
                _context.SaveChanges();
                return true;
            }
            
        }

        public bool UpdateBloodStock(BloodGroupModel model)
        {
            var result = _context.BloodStock.ToList().Where(x => x.Id == model.Id).FirstOrDefault();
            _context.Entry(result).State = EntityState.Modified;
            result.Units = result.Units + model.Unit;
            _context.SaveChanges();
            return true;
        }
        public List<object> GetRequestStatusCount()
        {
            var RequestList = _context.PetientDetails.ToList();//.Where(u => u.UserId.Equals(_userService.GetUserId())).Select(s => s.RequestStatus.ToString() == "Pending");
            var PendingRequest= RequestList.Where(u => u.UserId.Equals(_userService.GetUserId()) && u.RequestStatus.ToString() == "Pending").Select(s => s.RequestStatus.ToString() == "Pending").Count();
            var AcceptRequest = RequestList.Where(u => u.UserId.Equals(_userService.GetUserId()) && u.RequestStatus.ToString() == "Accepted").Select(s => s.RequestStatus.ToString() == "Accepted").Count();
            var RejectRequest = RequestList.Where(u => u.UserId.Equals(_userService.GetUserId()) && u.RequestStatus.ToString() == "Rejected").Select(s => s.RequestStatus.ToString() == "Rejected").Count();
            var TotalRequest = RequestList.Count();
            List<object> RequestListData = new List<object>();
            RequestListData.Add(TotalRequest);
            RequestListData.Add(AcceptRequest);
            RequestListData.Add(PendingRequest);
            RequestListData.Add(RejectRequest);
            return RequestListData;

        }
    }
}
