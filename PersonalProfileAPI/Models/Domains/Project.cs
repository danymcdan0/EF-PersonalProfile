namespace PersonalProfileAPI.Models.Domains
{
	public class Project
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Aim { get; set; }

		public string? ImageUrl { get; set; }

        //public ICollection<string?> Skills { get; set; }

        //public ICollection<string?> Tools { get; set; }

        //public Guid? EducationId { get; set; }
    }
}
