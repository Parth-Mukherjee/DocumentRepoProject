using DocumentRepository.Data;
using DocumentRepository.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace DocumentRepository.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ApplicationDbContext _context;
        public DocumentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddDocument(DocumentViewModel document)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploads);
            var fileName = Path.GetFileName(document.uploadedFile.FileName);
            var filePath = Path.Combine(uploads, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                document.uploadedFile.CopyTo(stream);
            }
            var documentModel = ToDocumentModel(document);
            _context.documentModels.Add(documentModel);
            _context.SaveChanges();
        }

        public DocumentModel ToDocumentModel(DocumentViewModel document)
       {
            DocumentModel documentModel = new DocumentModel()
            {
                documentName = document.documentName,
                documentCode = document.documentCode,
                documentExtension = Path.GetExtension(document.uploadedFile.FileName).ToLower(),
                documentSize = Convert.ToInt32((document.uploadedFile.Length) / 1024 * 1024),
                uploadedFileDetails = Path.GetFileNameWithoutExtension(document.uploadedFile.FileName),
                uploadedBy = "TestUser",
                uploaded_DateTime = DateTime.Now
            };  
            return documentModel;
       }

        public List<DocumentDetailListViewModel> GetAllDocumentDetails()
        {
            List<DocumentDetailListViewModel> documentsListVm = new List<DocumentDetailListViewModel>();
             List<DocumentModel> documentListFromDb = _context.documentModels.ToList();
            foreach (var item in documentListFromDb)
            {
                DocumentDetailListViewModel docVm = new DocumentDetailListViewModel();
                docVm.DocumentID = item.documentID;
                docVm.documentName = item.documentName;
                docVm.documentCode = item.documentCode;
                docVm.fileAddress = GetDocumentPathByDocumentID(item.documentID);

                documentsListVm.Add(docVm);    
            }
            return documentsListVm;
        }


        public string GetDocumentPathByDocumentID(Guid documentID)
        {
            string? fileName = _context.documentModels.Where(doc => doc.documentID == documentID)
                 .Select(x => $"{x.uploadedFileDetails}{x.documentExtension}").FirstOrDefault();
           
            string? FilePath = "/uploads/" + fileName;
            return FilePath;
        }


        public DocumentViewModel? GetDocumentDetailByDocumentID(Guid documentID)
        {
           DocumentViewModel? documentDetail =  _context.documentModels.Where(x => x.documentID == documentID)
                .Select(x => new DocumentViewModel { documentName = x.documentName, documentCode = x.documentCode }).FirstOrDefault();

            return documentDetail;
        }

        public int UpdateDocumentDetail(DocumentViewModel model)
        {
            var document = _context.documentModels.Find(model.documentID);
            int changesSaved = 0 ;
            if (document != null)
            {
                document.documentName = model.documentName;
                document.documentCode = model.documentCode;
                document.updatedBy = "TestUser";
                document.updatedOn = DateTime.Now;
                if (model.uploadedFile != null)
                {
                    document.uploadedFileDetails = Path.GetFileNameWithoutExtension(model.uploadedFile.FileName);
                    document.documentExtension = Path.GetExtension(model.uploadedFile.FileName).ToLower();
                    document.documentSize = Convert.ToInt32((model.uploadedFile.Length) / 1024 * 1024);
                }
                changesSaved = _context.SaveChanges();
                return changesSaved;
            }
            return changesSaved;
        }
    }


}
