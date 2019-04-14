using PeriodicTable.Model.Entity;
using PeriodicTable.Model.Support;

namespace PeriodicTable.Model.DAO
{
    public class GroupBlockDAO
    {
        public GroupBlock GetObject(string name)
        {
            name = Common.ToTitleCase(name);

            var groupBlock = new GroupBlock()
            {
                Name = name
            };

            return groupBlock;
        }
    }
}
