namespace TechTree.AssociateService.Models
{
    public class Association
    { 
        public int NodeId { get; set; }
        public int AssociateId { get; set; }
        public virtual Associate Associate { get; set; }
        public string AssociationType { get; set; }
    }
}