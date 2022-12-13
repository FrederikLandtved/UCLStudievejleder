namespace UCLStudievejlederApp.Models.Formular
{
    public class MyFormularsModel
    {
        public List<FormularListItemModel> Formulars { get; set; } = new List<FormularListItemModel>();
    }

    public class FormularListItemModel
    {
        public int FormularId { get; set; }
        public string DateCreated { get; set; }
        public string Month { get; set; }
        public string TypeOfEnquiry { get; set; }
        public string WhoWereGuided { get; set; }
        public string Level { get; set; }
        public string ChoosePrimaryInstitution { get; set; }
        public string ChooseAllFieldsOfStudies { get; set; }
        public string ConversationTime { get; set; }
        public string SubjectCategory { get; set; }
    }
}
