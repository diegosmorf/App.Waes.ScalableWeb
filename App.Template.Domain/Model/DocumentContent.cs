using App.Waes.ScalableWeb.Core.Domain;

namespace App.Waes.ScalableWeb.Domain.Model
{
    public class DocumentContent : IEntityBase
    {
        public string Left { get; set; }

        public string Right { get; set; }
        public int Id { get; set; }
    }
}