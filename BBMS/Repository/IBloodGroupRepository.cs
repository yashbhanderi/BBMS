using BBMS.Data;
using BBMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBMS.Repository
{
    public interface IBloodGroupRepository
    {
        Task<int> AddPetientRequest(PetientRequestModel model);
        Task<List<BloodGroupModel>> GetAllBloodGroup();
        Task<List<PetientRequestModel>> GetPetientRequestDetails();
        List<object> GetRequestStatusCount();
        Task<int> AddDonateRequest(DonateRequestModel model);
        Task<List<DonateRequestModel>> GetDonateRequestDetails();
        Task<List<DonateRequestModel>> GetDonationDetailsAdmin();
        Task<List<PetientRequestModel>> GetPetientRequestsAdmin();
        bool UpdateRequestStatusForPetient(int id, string action);
        bool UpdateRequestStatusForDonor(int id, string action);
        bool UpdateBloodStock(BloodGroupModel model);


    }
}