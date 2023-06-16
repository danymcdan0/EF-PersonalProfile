namespace PersonalProfileAPI.Models.Domains
{
	public class Project
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Aim { get; set; }

		//public List<string> Skills { get; set; }

		//public List<string> Tools { get; set; }

		//public Education? Dissertation { get; set; }

		public string? ImageUrl { get; set; }
	}
}
