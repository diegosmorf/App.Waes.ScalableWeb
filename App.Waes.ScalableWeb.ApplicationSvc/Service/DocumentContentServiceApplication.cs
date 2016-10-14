using System;
using App.Waes.ScalableWeb.Application.Contract;
using App.Waes.ScalableWeb.Core.Repository;
using App.Waes.ScalableWeb.Domain.Model;

namespace App.Waes.ScalableWeb.Application.Service
{
    public class DocumentContentServiceApplication : IDocumentContentServiceApplication
    {
        private readonly IDocumentComparisonStrategy comparisonStrategy;
        private readonly ICryptographyHandler cryptoHandler;
        private readonly IRepository<DocumentContent> repositoryDocumentContent;

        public DocumentContentServiceApplication(
            IRepository<DocumentContent> repositoryDocumentContent,
            IDocumentComparisonStrategy comparisonStrategy,
            ICryptographyHandler cryptoHandler)
        {
            this.repositoryDocumentContent = repositoryDocumentContent;
            this.comparisonStrategy = comparisonStrategy;
            this.cryptoHandler = cryptoHandler;
        }

        public void Left(int id, string content)
        {
            if (id == 0)
                throw new ArgumentException($"Id is required field");

            if (string.IsNullOrEmpty(content))
                throw new ArgumentException($"Content is required field");

            content = cryptoHandler.Decode(content);

            var document = repositoryDocumentContent.Find(d => d.Id == id);

            if (document == null)
            {
                repositoryDocumentContent.Insert(new DocumentContent {Id = id, Left = content});
            }
            else
            {
                document.Left = content;
                repositoryDocumentContent.Update(document);
            }
        }

        public void Right(int id, string content)
        {
            if (id == 0)
                throw new ArgumentException($"Id is required field");

            if (string.IsNullOrEmpty(content))
                throw new ArgumentException($"Content is required field");

            content = cryptoHandler.Decode(content);

            var document = repositoryDocumentContent.Find(d => d.Id == id);

            if (document == null)
            {
                repositoryDocumentContent.Insert(new DocumentContent {Id = id, Right = content});
            }
            else
            {
                document.Right = content;
                repositoryDocumentContent.Update(document);
            }
        }

        public DocumentComparisonResult Diff(int id)
        {
            var document = repositoryDocumentContent.Find(d => d.Id == id);

            if (document == null)
                throw new ArgumentException($"Document id={id} does not exists on repository.");

            return comparisonStrategy.Compare(document.Left, document.Right);
        }
    }
}