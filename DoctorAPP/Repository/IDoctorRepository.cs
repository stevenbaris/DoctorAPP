using DoctorAPP.Models;

namespace DoctorAPP.Repository
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllDoctor(string token);
        Task<Doctor> GetDoctorById(int id, string token);
        Task<Doctor> AddDoctor(Doctor newDoctor, string token);
        Task<Doctor> UpdateDoctor(int doctorId, Doctor updatedDoctor, string token);
        Task DeleteDoctor(int doctorId, string token);
    }
}
