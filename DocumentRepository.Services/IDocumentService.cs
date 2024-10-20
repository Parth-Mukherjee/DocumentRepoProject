using DocumentRepository.Models;
using System.Data;

namespace DocumentRepository.Services
{
    public interface IDocumentService
    {
        void AddDocument(DocumentViewModel document);
        List<DocumentDetailListViewModel> GetAllDocumentDetails();
        string GetDocumentPathByDocumentID(Guid documentID);
        DocumentViewModel? GetDocumentDetailByDocumentID(Guid documentID);
        int UpdateDocumentDetail(DocumentViewModel documentvm);
    }
}
