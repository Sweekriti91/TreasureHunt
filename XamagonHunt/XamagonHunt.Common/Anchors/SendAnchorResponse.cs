using System;
namespace XamagonHunt.Common
{
    public class SendAnchorResponse
    {
        public string AnchorNumber { get; }

        public SendAnchorResponse(string anchorNumber)
        {
            if (string.IsNullOrWhiteSpace(anchorNumber))
            {
                throw new ArgumentException("The anchor number cannot be null, empty, or whitespace.", nameof(anchorNumber));
            }

            this.AnchorNumber = anchorNumber;
        }
        
        public static implicit operator string(SendAnchorResponse value)
        {
            return value?.AnchorNumber;
        }
    }
}
