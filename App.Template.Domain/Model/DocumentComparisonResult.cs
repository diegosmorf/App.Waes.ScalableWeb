using System.Collections.Generic;

namespace App.Waes.ScalableWeb.Domain.Model
{
    public class DocumentComparisonResult
    {
        public DocumentComparisonResult()
        {
            Diffs = new List<ComparisonDifference>();
        }

        public string ComparisonSizeName => ComparisonSize.ToString();
        public eDocumentComparisonSize ComparisonSize { get; set; }
        public string Content { get; set; }
        public int Size { get; set; }
        public IList<ComparisonDifference> Diffs { get; set; }
    }
}