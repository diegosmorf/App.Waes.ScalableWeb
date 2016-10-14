using App.Waes.ScalableWeb.Domain.Model;

namespace App.Waes.ScalableWeb.Application.Contract
{
    public interface IDocumentComparisonStrategy
    {
        DocumentComparisonResult Compare(string content1, string content2);
    }
}