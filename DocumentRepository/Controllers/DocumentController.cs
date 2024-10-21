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
            //throw new NotImplementedException();
            DocumentViewModel documentViewModel = _service.GetDocumentListAsPerPagenation(1); //1 = Get the document data for the first page of pegination 
            return View(documentViewModel);
        }

        [HttpPost]
        public IActionResult AddDocument(DocumentViewModel document)
        {
            if (document.isUpdated == 1 && document.uploadedFile == null)
            {
                ModelState.Remove("uploadedFile");
            }
                if (ModelState.IsValid)
                {
                    ViewBag.DocumentSubmitted = _service.AddDocument(document);
                    return RedirectToAction("AddDocument");
                }
                else
                {
                    document = _service.GetDocumentListAsPerPagenation(1);
                    return View(document);
                }
                
        }

        [HttpGet]
        public IActionResult GetDocumentDataForEdit(Guid documentID)
        {
            return Json(_service.GetDocumentDetailByDocumentID(documentID));

        }
        
        [HttpPost]
        public IActionResult UpdateDocumentData([FromBody] DocumentViewModel documentViewModel)
        {
            int changesUpdated = _service.UpdateDocumentDetail(documentViewModel);
            return Json(changesUpdated);
        }

        [HttpGet]
        public IActionResult getDocumentDetailListPartial(int pageIndex)
        {
            var documentList = _service.GetDocumentListAsPerPagenation(pageIndex);
            return PartialView("_DocumentDetailsListView", documentList.documentDetailList);
        }
    

    }
}
