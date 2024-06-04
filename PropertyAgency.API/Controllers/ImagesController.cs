using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService _imagesService;
        private readonly string _uploadPath = "./uploads";

        public ImagesController(IImagesService imagesService)
        {
            _imagesService = imagesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetImages()
        {
            var token = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            var images = await _imagesService.GetImages();
            return Ok(images);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var token = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            var image = await _imagesService.GetById(id);
            if (image == null)
                return NotFound();
            return Ok(image);
        }
        
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetById(Guid id)
        // {
        //     var image = await _imagesService.GetById(id);
        //     if (image == null)
        //         return NotFound();
        //
        //     try
        //     {
        //         // Читаем файл из указанного пути
        //         var imageFileStream = System.IO.File.OpenRead(image.ImageUrl);
        //
        //         // Читаем содержимое файла в массив байтов
        //         byte[] imageData = new byte[imageFileStream.Length];
        //         await imageFileStream.ReadAsync(imageData, 0, (int)imageFileStream.Length);
        //
        //         // Определяем MIME-тип изображения (предположим, что image.MimeType содержит его)
        //         string mimeType = "image/jpeg"; // Пример MIME-типа
        //         // Также можно определить тип файла на основе расширения файла или другим способом
        //
        //         // Возвращаем файл как FileContentResult
        //         return File(imageData, mimeType);
        //     }
        //     catch (Exception ex)
        //     {
        //         // Если возникла ошибка при чтении файла, возвращаем ошибку сервера
        //         return StatusCode(500, $"Internal server error: {ex.Message}");
        //     }
        // }
        

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UploadImage(IFormFile file, [FromForm] Guid propertyId)
        {
            var uploadResult = await _imagesService.SaveImage(file, _uploadPath, propertyId);
            if (uploadResult == null)
                return BadRequest("No file uploaded.");

            return Ok(new { FilePath = uploadResult });
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            var image = await _imagesService.GetById(id);
            if (image == null)
                return NotFound();

            var deleteResult = await _imagesService.DeleteImage(id);
            if (!deleteResult)
                return StatusCode(500, "Failed to delete image from database.");

            try
            {
                // Удаляем файл из файловой системы
                if (System.IO.File.Exists(image.ImageUrl))
                {
                    System.IO.File.Delete(image.ImageUrl);
                }
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при удалении файла, возвращаем ошибку сервера
                return StatusCode(500, $"Failed to delete image file: {ex.Message}");
            }

            return Ok();
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateImage(Guid id, IFormFile file)
        {
            // Получаем данные об изображении из репозитория
            var existingImage = await _imagesService.GetById(id);
            if (existingImage == null)
            {
                return NotFound();
            }

            try
            {
                // Если пришел новый файл, удаляем старый и сохраняем новый
                if (file != null && file.Length > 0)
                {
                    // Удаляем старый файл
                    System.IO.File.Delete(existingImage.ImageUrl);

                    // Загружаем новый файл
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(_uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Обновляем путь к изображению в базе данных
                    existingImage.ImageUrl = filePath;

                    // Сохраняем обновленное изображение
                    var updateResult = await _imagesService.UpdateImage(existingImage);
                    if (!updateResult)
                    {
                        // Если возникла ошибка при обновлении изображения, возвращаем ошибку сервера
                        return StatusCode(500, "Failed to update image.");
                    }
                }

                return Ok(existingImage);
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при обновлении изображения, возвращаем ошибку сервера
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("property/{propertyId}")]
        [Authorize]
        public async Task<IActionResult> GetImagesByProperty(Guid propertyId)
        {
            var token = Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            var images = await _imagesService.GetByProperty(propertyId);
            if (images == null || images.Count == 0)
                return NotFound();

            return Ok(images);
        }
    }
}
