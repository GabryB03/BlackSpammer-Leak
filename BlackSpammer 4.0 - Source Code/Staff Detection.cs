   public async Task<bool> isStaff(string username)
        {
            try
            {

                var http = new HttpClient();
                var content = await Task<String>.Run(async () =>
                {


                    var authReq = new HttpRequestMessage
                    {

                        RequestUri = new Uri(API_URL + "/staff/?user=" + username),
                        Method = HttpMethod.Get

                    };
                    var e = await http.SendAsync(authReq);
                    var contentx = await e.Content.ReadAsStringAsync();
                    return contentx;
                });
                var rsp = content;
                if(rsp.Contains("200 This user is an official staff member"))
                {
                    return true;
                } else
                {
                    return false;
                }

            } catch(Exception exc)
            {
                return false;
            }
        }