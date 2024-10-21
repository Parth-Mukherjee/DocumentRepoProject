using DocumentRepository.Models;
using System.Data;

namespace DocumentRepository.Services
{
    public interface IDocumentService
    {
        int AddDocument(DocumentViewModel document);
        DocumentViewModel? GetDocumentDetailByDocumentID(Guid documentID);
        int UpdateDocumentDetail(DocumentViewModel documentvm);
        DocumentViewModel GetDocumentListAsPerPagenation(int pageIndex);

        
    }
}
