namespace DatabaseAccess.Institution.Models
{
    public class InstitutionModel : UCLListItem
    {
        public int InstitutionId { get; set; }
        public string Name { get; set; }
    }


    public class UCLListItem
    {
        public bool IsSelected { get; set; }
    }
}
