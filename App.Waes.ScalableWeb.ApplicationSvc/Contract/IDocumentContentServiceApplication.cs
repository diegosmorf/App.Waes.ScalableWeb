using App.Waes.ScalableWeb.Domain.Model;

namespace App.Waes.ScalableWeb.Application.Contract
{
    public interface IDocumentContentServiceApplication
    {
        void Left(int id, string content);
        void Right(int id, string content);
        DocumentComparisonResult Diff(int id);
    }
}