using Gerenciador.Context;
using GERENCIADOR_TESTE_TEMPLANTE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using X.PagedList;

namespace Gerenciador.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
       private readonly Contexto _context;

        public FileController(Contexto context){
                _context = context;
        }

        [HttpGet]
        [Authorize(Roles ="Administrador")]
        public async Task<IActionResult> Index()
        {
            var recebedados = new FileUploadViewModel();
            recebedados.FilesOnDatabase = await _context.FilesOnDatabase.Where(l => l.status == "Pendente").ToListAsync();
            return View(recebedados);
            //return View(await _context.FilesOnDatabase.ToListAsync()); 
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index2(int? pagina)
        {
            const int itensPorPagina = 6;
            int numeroPagina = (pagina ?? 1);
            return View(await _context.FilesOnDatabase.Where(a=>a.status == "aprovado").ToPagedListAsync(numeroPagina, itensPorPagina));
        }
        //[AllowAnonymous]
        //public async Task<IActionResult> index2()
        //{
        //    var recebedados2 = new FileUploadViewModel();
        //    recebedados2.FilesOnDatabase = await _context.FilesOnDatabase.Where(t => t.status == "aprovado").ToListAsync();
        //    return View(recebedados2);
        //}


        [Authorize(Roles = "Administrador, Aluno")]
         [HttpPost]
        public async Task<IActionResult> UploadToDatabase(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var verifica = fileName;
                //string[] check = verifica.Split(".");
                //foreach(string t in check)
                //{
                    if ( verifica.Contains("."))
                    {
                        TempData["Message1"] = "Arquivo invalido!!, use um arquivo .pdf";
                        break;
                    }
                //}
                if (extension != ".pdf") { TempData["Message1"] = "Arquivo invalido!!, use um arquivo .pdf";
                     break;  } else { 

                var fileModel = new FileOnDatabaseModel
                {
                    CreatedOn = DateTime.UtcNow,
                    FileType = file.ContentType,
                    Extension = extension,
                    Name = fileName,
                    Description = description,
                    UploadedBy = User.Identity.Name,
                    status = "Pendente"
                };
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }
                _context.FilesOnDatabase.Add(fileModel);
                _context.SaveChanges();
                TempData["Message"] = "Arquivo Enviado com Sucesso!! aguarde para ser aprovado!!";
                }
        }
            return RedirectToAction("Index2");
        }


        [Authorize(Roles = "Administrador, Aluno")]
        [HttpPost]
        public async Task<IActionResult> UploadToDatabasePrivate(List<IFormFile> files, string description, string IdUsuario)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var verifica = fileName;
                //string[] check = verifica.Split(".");
                //foreach(string t in check)
                //{
                if (verifica.Contains("."))
                {
                    TempData["Message1"] = "Arquivo invalido!!, use um arquivo .pdf";
                    break;
                }
                //}
                if (extension != ".pdf")
                {
                    TempData["Message1"] = "Arquivo invalido!!, use um arquivo .pdf";
                    break;
                }
                else
                {
                    var fileModel = new FileOnDatabaseModel
                    {
                        CreatedOn = DateTime.UtcNow,
                        FileType = file.ContentType,
                        Extension = extension,
                        Name = fileName,
                        Description = description,
                        UploadedBy = IdUsuario,
                        status = "Pendente",
                        //idUsuario = IdUsuario
                    };
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        fileModel.Data = dataStream.ToArray();
                    }
                    _context.FilesOnDatabase.Add(fileModel);
                    _context.SaveChanges();
                    TempData["Message"] = "Arquivo Enviado com Sucesso!! aguarde para ser aprovado!!";
                }
            }
            return RedirectToAction("Index2");
        }



        [Authorize(Roles = "Administrador, Aluno , Professor")]
        public async Task<IActionResult> DownloadFileFromDatabase(int id)
        {
            var file = await _context.FilesOnDatabase.FindAsync(id);
            if (file == null) return null;
            return File(file.Data, file.FileType, file.Name + file.Extension);
        }

    [Authorize(Roles = "Administrador")]
         public async Task<IActionResult> DeleteFileFromDatabase(int id)
        {

            var file = await _context.FilesOnDatabase.FindAsync(id);
            if (file == null) return null;
            _context.FilesOnDatabase.Remove(file);
            await _context.SaveChangesAsync();
            TempData["Message"] = $"Removed {file.Name + file.Extension} Arquivo Deleteado com sucesso.";
            return RedirectToAction("Index");
        }

    [Authorize(Roles = "Administrador")]
         public async Task<IActionResult> DetailsFileFromDatabase(int? id)
         {
             if (id == null){
                 return RedirectToAction("Index");
             }
             var arquivo = await _context.FilesOnDatabase.FindAsync(id);
             if(arquivo == null){
                 return RedirectToAction("Index");
             }
             return PartialView(arquivo);
         }
        
    [Authorize(Roles = "Administrador")]
         public async Task<IActionResult> Acceptcontent(int id)
        {

            var file = await _context.FilesOnDatabase.FindAsync(id);
            file.status = "aprovado";

            _context.Entry(file).State = EntityState.Modified;
           await _context.SaveChangesAsync();
            TempData["Message"] = $"Aprovado {file.Name + file.Extension} com sucesso.";
            return RedirectToAction("Index");
        }
    }
}
