using DatabaseEntity.CodeFirst.Context;

namespace DatabaseEntity
{
    class Program
    {
        public static void Main(string[] args)
        {
            using(SiteContext context = new SiteContext())
            {
                context.SaveChanges();
            }
        }
    }
}