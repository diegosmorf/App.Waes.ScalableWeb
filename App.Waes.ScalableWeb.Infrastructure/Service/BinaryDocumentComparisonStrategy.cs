using System.Text;
using App.Waes.ScalableWeb.Application.Contract;
using App.Waes.ScalableWeb.Domain.Model;

namespace App.Waes.ScalableWeb.Infrastructure.Service
{
    public class BinaryDocumentComparisonStrategy : IDocumentComparisonStrategy
    {
        public DocumentComparisonResult Compare(string content1, string content2)
        {
            var result = new DocumentComparisonResult();
            var content1Bytes = Encoding.UTF8.GetBytes(content1);
            var content2Bytes = Encoding.UTF8.GetBytes(content2);

            if (content1Bytes.Length != content2Bytes.Length)
            {
                result.ComparisonSize = eDocumentComparisonSize.IsNotEqual;
                return result;
            }

            var difference = new ComparisonDifference();
            for (var i = 0; i < content1Bytes.Length; i++)
            {
                var bit1 = content1Bytes[i];
                var bit2 = content2Bytes[i];

                if (bit1 == bit2)
                {
                    if (difference.Id > 0)
                    {
                        difference.Size = i + 1 - difference.StartPosition;
                        result.Diffs.Add(difference);
                        difference = new ComparisonDifference();
                    }
                }
                else
                {
                    if (difference.Id == 0)
                        difference = new ComparisonDifference
                        {
                            Id = result.Diffs.Count + 1,
                            StartPosition = i + 1
                        };
                }
            }

            if (difference.Id > 0)
            {
                difference.Size = content1Bytes.Length + 1 - difference.StartPosition;
                result.Diffs.Add(difference);
            }

            result.Size = content1Bytes.Length;

            result.ComparisonSize = result.Diffs.Count == 0 ?
                eDocumentComparisonSize.IsEqual :
                eDocumentComparisonSize.IsNotEqual;

            if (result.ComparisonSize == eDocumentComparisonSize.IsEqual)
                result.Content = content1;

            return result;
        }
    }
}