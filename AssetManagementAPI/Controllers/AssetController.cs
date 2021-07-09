using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using AssetManagementBusiness;
using AssetManagementData;

namespace AssetManagementAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IAssetManagementService _assetManagement;
        public AssetController(IAssetManagementService assetManagement, IWebHostEnvironment hostEnvironment)
        {
            _assetManagement = assetManagement;
            this._hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetModel>>> GetAssets()
        {
            var content = await _assetManagement.GetAllAsync();
            return Ok(content.Select(c => 
                { c.ImageSrc = String.Format($"{Request.Scheme}://{Request.Host}{Request.PathBase}/Images/{c.ImageName}"); 
                    return c; }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetModel>> GetEmployeeModel(int id)
        {
            var employeeModel = await _assetManagement.GetByIdAsync(id);

            if (employeeModel == null)
            {
                return NotFound();
            }

            return employeeModel;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeModel(int id, [FromForm] AssetModel employeeModel)
        {
            if (id != employeeModel.AssetID)
            {
                return BadRequest();
            }

            if (employeeModel.ImageFile != null)
            {
                DeleteImage(employeeModel.ImageName);
                employeeModel.ImageName = await SaveImage(employeeModel.ImageFile);
            }
            await _assetManagement.Update(employeeModel);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<AssetModel>> PostEmployeeModel([FromForm] AssetModel employeeModel)
        {
            employeeModel.ImageName = await SaveImage(employeeModel.ImageFile);
            await _assetManagement.CreateAsync(employeeModel);

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AssetModel>> DeleteEmployeeModel(int id)
        {
            var employeeModel = await _assetManagement.GetByIdAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            await _assetManagement.DeleteAsync(id);

            return employeeModel;
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
