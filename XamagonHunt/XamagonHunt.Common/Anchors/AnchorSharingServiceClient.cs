using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamagonHunt.Common
{
    public class AnchorSharingServiceClient
    {
        private readonly string endpointUrl;

        public AnchorSharingServiceClient(string endpointUrl)
        {
            if (string.IsNullOrWhiteSpace(endpointUrl))
            {
                throw new ArgumentException("The base address cannot be null, empty, or whitespace.", nameof(endpointUrl));
            }

            this.endpointUrl = endpointUrl;
        }

        public async Task<SendAnchorResponse> SendAnchorIdAsync(string anchorId)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(anchorId);
                HttpResponseMessage response = await client.PostAsync(this.endpointUrl, content);

                response.EnsureSuccessStatusCode();

                string anchorNumber = await response.Content.ReadAsStringAsync();

                return new SendAnchorResponse(anchorNumber);
            }
        }

        public async Task<RetrieveAnchorResponse> RetrieveAnchorIdAsync(string anchorNumber)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponse = await client.GetAsync($"{this.endpointUrl}/{anchorNumber}");

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string anchorId = await httpResponse.Content.ReadAsStringAsync();

                        return new RetrieveAnchorResponse(anchorId);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return RetrieveAnchorResponse.NotFound;
        }
    }
}
