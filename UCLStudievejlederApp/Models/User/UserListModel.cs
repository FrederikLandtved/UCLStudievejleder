namespace UCLStudievejlederApp.Models.User
{
    public class UserListModel
    {
        List<UserListItemModel> UserList { get; set; } = new List<UserListItemModel>();
    }

    public class UserListItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Institutions { get; set; }
        public string FieldsOfStudy { get; set; }
    }
}
