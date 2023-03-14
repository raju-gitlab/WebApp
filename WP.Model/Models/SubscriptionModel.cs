namespace WP.Model.Models
{
    public class SubscriptionModel : CommonModel
    {
        public int Id { get; set; }
        public string UserUUID { get; set; }
        public string PageUUID { get; set; }
    }
}
