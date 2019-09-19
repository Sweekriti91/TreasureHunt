using System;
namespace XamagonHunt.Common
{
    public class RetrieveAnchorResponse
    {
        public static RetrieveAnchorResponse NotFound => new RetrieveAnchorResponse();

        public bool AnchorFound { get; }

        public string AnchorId { get; }

        public RetrieveAnchorResponse(string anchorId)
        {
            if (string.IsNullOrWhiteSpace(anchorId))
            {
                throw new ArgumentException("The anchor identifier cannot be null, empty, or whitespace.", nameof(anchorId));
            }

            this.AnchorId = anchorId;
            this.AnchorFound = true;
        }

        private RetrieveAnchorResponse()
        {
        }

        public static implicit operator string(RetrieveAnchorResponse value)
        {
            return value?.AnchorId;
        }
    }
}
