using DoctorAPP.Models;
using DoctorAPP.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAPP.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IActionResult> GetAllDoctor()
        {
            string token = HttpContext.Session.GetString("JWToken");
            var doctorList = await _doctorRepository.GetAllDoctor(token);
            return View(doctorList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var doctor = await _doctorRepository.GetDoctorById(id, token);
            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Doctor newDoctor)
        {
            var token = HttpContext.Session.GetString("JWToken");

            await _doctorRepository.AddDoctor(newDoctor, token);
            return RedirectToAction("GetAllDoctor");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var doctor = await _doctorRepository.GetDoctorById(id, token);
            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Doctor updatedDoctor)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (id != updatedDoctor.DoctorId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var modifiedDoctor = await _doctorRepository.UpdateDoctor(id, updatedDoctor, token);
                return RedirectToAction(nameof(GetAllDoctor), new { id = modifiedDoctor.DoctorId, token });
            }

            return View(updatedDoctor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            await _doctorRepository.DeleteDoctor(id, token);
            return RedirectToAction("GetAllDoctor");
        }
    }
}
