using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using UploadImagemCapturar.DataContext;
using UploadImagemCapturar.Models;
using UploadImagemCapturar.UploadRepository;
using static System.Net.Mime.MediaTypeNames;
using File = UploadImagemCapturar.Models.FileUpload;

namespace UploadImagemCapturar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUploadRepository _uploadRepository;
        public HomeController(ILogger<HomeController> logger, IUploadRepository uploadRepository)
        {
            _logger = logger;
            _uploadRepository = uploadRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            var fileBytes = stream.ToArray();


            var dbfile = new FileUpload()
            {
                Name = file.FileName,
                Content = fileBytes

            };
            _uploadRepository.SalvarImagem(dbfile);
            //using (var context = new AppDbContext())
            //{
            //    context.Files.Add(dbFile);
            //    await context.SaveChangesAsync();
            //}
            return RedirectToAction("Index");
        }

        [HttpGet/*("dowloard/{id}")*/]
        public IActionResult Download(int id)
        {

            //var dbFile = _uploadRepository.RecuperarArquivo(id);
            //if (dbFile == null)
            //{
            //    // Trate o caso de arquivo não encontrado
            //    return NotFound();
            //}

            //var fileBytes = dbFile.Content;
            //return File(fileBytes, "application/octet-stream", dbFile.Name);
            if (ModelState.IsValid)
            {
                try
                {

                    var imagemBanco = _uploadRepository.RecuperarArquivo(id);
                    var converte = "data:application/octet-stream;base64," + Convert.ToBase64String(imagemBanco.Content);
                    return Json(new { status = "success", nome = imagemBanco.Name, arquivo = converte });
                }
                catch (Exception ex)
                {
                    return Json(new { status = "error", message = ex.Message });
                }

            }
            else
            {
                return Json(new { status = "error", message = "Imagem vazia!" });

            }

        }
        [HttpPost]
        public IActionResult SavePicture(string dataImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    byte[] image = Convert.FromBase64String(dataImage.Split(',')[1]);
                    string s = Convert.ToBase64String(image);
                    var imageBanco = new FileUpload()
                    {
                        Name = "teste",
                        Content = image
                    };

                    _uploadRepository.SalvarImagem(imageBanco);
                    return Json(new { status = "success" });
                }
                catch (Exception ex)
                {
                    return Json(new { status = "error", message = ex.Message });
                }
                
            }else
            {
                return Json(new { status = "error", message = "Imagem vazia!" });

            }

        }
        [HttpGet]
        public IActionResult RecuperarImagem(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //byte[] image = Convert.FromBase64String(dataImage.Split(',')[1]);
                    //string s = Convert.ToBase64String(image);
                    //var imageBanco = new FileUpload()
                    //{
                    //    Name = "teste",
                    //    Content = image
                    //};

                    var imagemBanco = _uploadRepository.RecuperarImagem(id);
                    var converte = "data:image/png;base64," + Convert.ToBase64String(imagemBanco.Content);
                    return Json(new { status = "success", nome =  imagemBanco.Name, imagem = converte});
                }
                catch (Exception ex)
                {
                    return Json(new { status = "error", message = ex.Message });
                }

            }
            else
            {
                return Json(new { status = "error", message = "Imagem vazia!" });

            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}