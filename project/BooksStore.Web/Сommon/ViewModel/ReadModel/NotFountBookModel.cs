namespace BooksStore.Web.Сommon.ViewModel.ReadModel
{
    public class NotFountBookModel
    {
        public string ErrorMassage { get; set; }
        public string Value { get; set; }

        public NotFountBookModel(string errorMassage, string value)
        {
            ErrorMassage = errorMassage;
            Value = value;
        }
    }
}
