namespace Api.Common
{
    public static class HelperMethods
    {
        public static int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;

            if (today.Month < dob.Month || (today.Month == dob.Month && today.Day < dob.Day))
            {
                age--;
            }
            return age;
        }

        public static decimal FormatDecimal(decimal value)
        {
            return Math.Round(value, 2);
        }
    }
}
