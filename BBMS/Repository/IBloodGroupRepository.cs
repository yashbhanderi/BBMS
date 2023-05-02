using BBMS.Data;
using BBMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBMS.Repository
{
    public interface IBloodGroupRepository
    {
        Task<int> AddPetientRequest(PatientRequestModel model);
        Task<List<BloodGroupModel>> GetAllBloodGroup();
        Task<List<PatientRequestModel>> GetPetientRequestDetails();
        List<object> GetRequestStatusCount();
        Task<int> AddDonateRequest(DonateRequestModel model);
        Task<List<DonateRequestModel>> GetDonateRequestDetails();
        Task<List<DonateRequestModel>> GetDonationDetailsAdmin();
        Task<List<PatientRequestModel>> GetPetientRequestsAdmin();
        bool UpdateRequestStatusForPetient(int id, string action);
        bool UpdateRequestStatusForDonor(int id, string action);
        bool UpdateBloodStock(BloodGroupModel model);


    }
}