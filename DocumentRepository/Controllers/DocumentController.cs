using DocumentRepository.Models;
using DocumentRepository.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata;

namespace DocumentRepository.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService _service;
        public DocumentController(IDocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult AddDocument()
        {
            DocumentViewModel documentViewModel = new DocumentViewModel();
            var documentList = _service.GetAllDocumentDetails();
            documentViewModel.documentDetailList = documentList;
            return View(documentViewModel);
        }

        [HttpPost]
        public IActionResult AddDocument(DocumentViewModel document)
        {
            if (ModelState.IsValid)
            {
                _service.AddDocument(document);
                return RedirectToAction("AddDocument");
            }
            else
            {
                document.documentDetailList = _service.GetAllDocumentDetails();
                return View(document);
            }
                
        }

        [HttpGet]
        public IActionResult GetDocumentData(Guid documentID)
        {
            return Json(_service.GetDocumentDetailByDocumentID(documentID));

        }
        
        [HttpPost]
        public IActionResult UpdateDocument(DocumentViewModel documentVm)
        {
            int changesUpdated = _service.UpdateDocumentDetail(documentVm);
            return Json(changesUpdated);
        }


    }
}
