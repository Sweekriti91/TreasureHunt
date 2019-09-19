using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<SendAnchorResponse> SendAnchorIdAsync(string anchorId, string anchorDescription)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(anchorId);
                var newendpointUrl = endpointUrl + "?anchorDescription=" + anchorDescription;
                HttpResponseMessage response = await client.PostAsync(newendpointUrl, content);

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

        public async Task<List<string>> RetrieveAllAnchors()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponse = await client.GetAsync($"{this.endpointUrl}/all");

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string anchorList = await httpResponse.Content.ReadAsStringAsync();
                        var resultList = anchorList.Split(',').ToList();
                        
                        return resultList;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return null;
        }
    }
}
