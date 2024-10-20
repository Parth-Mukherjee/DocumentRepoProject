using DocumentRepository.Models.DTO;
using DocumentRepository.Services;
using Xunit;

namespace DocumentRepository.ServiceTest
{
    public class DocumentModelServiceTest
    {
        private readonly IDocumentService _documentservice;

        public DocumentModelServiceTest(IDocumentService documentService)
        {
            _documentservice = documentService;    
        }

     
        public void AddDocument_NullDocument()
        {
            //Arrange
            DocumentModelAddRequest documentRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(() => 
            { 
            _documentservice.AddDocument(documentRequest);
            });

            //assert
        }
    }
}
