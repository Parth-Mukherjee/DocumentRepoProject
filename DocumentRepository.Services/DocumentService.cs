using DocumentRepository.Data;
using DocumentRepository.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

        public int AddDocument(DocumentViewModel document)
        {
            if (document == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrEmpty(document.documentName) || string.IsNullOrEmpty(document.documentCode) )
            {
                throw new ArgumentException();
            }

            if (document.isUpdated == 0)  //Fresh Submit Case
            {


                //For fresh Document details entered in DB
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Path.GetFileName(document.uploadedFile.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    document.uploadedFile.CopyTo(stream);
                }
                //for fresh Document SUBMIT
                var documentModel = ToDocumentModel(document);
                _context.documentModels.Add(documentModel);
                return _context.SaveChanges();
            }
            else   //Update Existing Document detail
            {
                if (document.uploadedFile != null)   //If file also sent then replace the old file with new one
                {
                    
                    var oldFileName = _context.documentModels.Where(doc => doc.documentID == document.documentID)
                    .Select(x => $"{x.uploadedFileDetails}{x.documentExtension}").FirstOrDefault();

                    var fileUpdatePath = Path.Combine("wwwroot", "uploads", oldFileName);
                    if (System.IO.File.Exists(fileUpdatePath))
                    {
                        System.IO.File.Delete(fileUpdatePath);
                    }

                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    Directory.CreateDirectory(uploads);
                    var fileName = Path.GetFileName(document.uploadedFile.FileName);
                    var filePath = Path.Combine(uploads, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        document.uploadedFile.CopyTo(stream);
                    }
                }
                //For UPDATING document details 

              return  UpdateDocumentDetail(document);
            }

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

        //public List<DocumentDetailListViewModel> GetAllDocumentDetails()
        //{
        //    List<DocumentDetailListViewModel> documentsListVm = new List<DocumentDetailListViewModel>();
        //    List<DocumentModel> documentListFromDb = _context.documentModels.ToList();
        //    foreach (var item in documentListFromDb)
        //    {
        //        DocumentDetailListViewModel docVm = new DocumentDetailListViewModel();
        //        docVm.documentID = item.documentID;
        //        docVm.documentName = item.documentName;
        //        docVm.documentCode = item.documentCode;
        //        docVm.fileAddress = GetDocumentPathByDocumentID(item.documentID);

        //        documentsListVm.Add(docVm);
        //    }
        //    return documentsListVm;
        //}


        //public string GetDocumentPathByDocumentID(Guid documentID)
        //{
        //    string? fileName = _context.documentModels.Where(doc => doc.documentID == documentID)
        //         .Select(x => $"{x.uploadedFileDetails}{x.documentExtension}").FirstOrDefault();

        //    string? FilePath = "/uploads/" + fileName;
        //    return FilePath;
        //}


        public DocumentViewModel? GetDocumentDetailByDocumentID(Guid documentID)
        {
            if(documentID == Guid.Empty )
            {
                return null;
            }
            DocumentViewModel? documentDetail = _context.documentModels.Where(x => x.documentID == documentID)
                 .Select(x => new DocumentViewModel { documentName = x.documentName, documentCode = x.documentCode }).FirstOrDefault();
            if (documentDetail == null)
            {
                return null;
            }
            return documentDetail;
        }

        public int UpdateDocumentDetail(DocumentViewModel documentVm)  //Update the existing document details
        {

            if (documentVm == null)
            {
                throw new ArgumentNullException();
            }
            if (documentVm.documentID == Guid.Empty)
            {
                throw new ArgumentException();
            }


            //used in AddDocument method above
            var document = _context.documentModels.Find(documentVm.documentID);
            int changesSaved = 0;
            if (document != null)
            {
                document.documentName = documentVm.documentName;
                document.documentCode = documentVm.documentCode;
                document.updatedBy = "TestUser";
                document.updatedOn = DateTime.Now;
                if (documentVm.uploadedFile != null)
                {
                    document.uploadedFileDetails = Path.GetFileNameWithoutExtension(documentVm.uploadedFile.FileName);
                    document.documentExtension = Path.GetExtension(documentVm.uploadedFile.FileName).ToLower();
                    document.documentSize = Convert.ToInt32((documentVm.uploadedFile.Length) / 1024 * 1024);
                }
                changesSaved = _context.SaveChanges();
                return changesSaved;  //Document Updated then savechange = 1
            }
            return changesSaved;
        }


        public DocumentViewModel GetDocumentListAsPerPagenation(int pageIndex)   //As per the page index click from pagenation
        {                                                          //it will get the next 5 document details. 
            int maxRows = 5;
            DocumentViewModel documentvm = new DocumentViewModel();
            documentvm.documentDetailList = _context.usp_getPagenatedDocumentList(pageIndex, maxRows);


            double result = (double)_context.documentModels.Count() / maxRows;
            int incrementedPageIndex = (int)Math.Ceiling(result);
            documentvm.pageCount = incrementedPageIndex;
            documentvm.currentIndex = pageIndex;
            return documentvm;
        }
    }


}
