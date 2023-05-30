using Supermarket_Managment_System.Models;
using Supermarket_Managment_System.ViewModels;

namespace Supermarket_Managment_System.Services.CasherService
{
    public interface ICasherServices
    {
        public void test();
        public  Task<IEnumerable<users>> Index();
        public  Task<users> Delete(users u);
        public  Task<users> Update(users u);
        public  Task<users> Create(RegisterVM model);

    }
}
