namespace TrackingManagement.DTO
{
    public class AccessTokenModel
    {
        public string AccessToken { get; set; }
        public int ScopeId { get; set; }

        public AccessTokenModel(string accessToken, int scopeId)
        {
            AccessToken = accessToken;
            ScopeId = scopeId;
        }
    }
}
