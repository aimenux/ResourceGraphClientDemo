namespace App
{
    public class Settings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string SubscriptionId { get; set; }
        public string ResourceGraphQuery { get; set; }
        public string ResourceUrl => "https://management.core.windows.net";
        public string AuthorityUrl => $"https://login.microsoftonline.com/{TenantId}";
    }
}
