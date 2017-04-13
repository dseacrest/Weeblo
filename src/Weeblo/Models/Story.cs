namespace Weeblo.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description  { get; set; }
        public string Status { get; set; }
        public string AssignedTo { get; set; }
    }
}