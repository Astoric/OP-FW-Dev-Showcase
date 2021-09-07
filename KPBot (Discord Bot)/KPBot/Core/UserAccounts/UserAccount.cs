namespace KPBot.Core.UserAccounts
{
    public class UserAccount
    {
        public ulong ID { get; set; }

        public int admin_points { get; set; }

        public int claim_points { get; set; }

        public int active_points { get; set; }

        public int total_points { get; set; }

        public bool blacklisted { get; set; }

        public int XP { get; set; }
    }
}