namespace SDSI.Access
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public bool IsAdult(int age)
        {
            return age >= 18;
        }
    }
}
