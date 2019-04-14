using PeriodicTable.Model.Entity;
using PeriodicTable.Model.Support;

namespace PeriodicTable.Model.DAO
{
    public class StandardStateDAO
    {
        public StandardState GetObject(string value)
        {
            value = Common.ToTitleCase(value);

            var standardState = new StandardState()
            {
                Value = value
            };

            return standardState;
        }
    }
}
