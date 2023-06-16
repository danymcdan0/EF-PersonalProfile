namespace PersonalProfileAPI.Models.Domains
{
	public class Education
	{
        public Guid Id { get; set; }

        public string University { get; set; }

        public string Course { get; set; }

        public string Grade { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
