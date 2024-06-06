namespace BO.Dtos
{
    public class SystemAccountDto
    {
        public short AccountId { get; set; }

        public string? AccountName { get; set; }

        public string? AccountEmail { get; set; }

        public int AccountRole { get; set; }

        public string? AccountPassword { get; set; } = "1234";
    }
}
