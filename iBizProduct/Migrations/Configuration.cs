namespace iBizProduct.Migrations
{
    using System.Data.Entity.Migrations;
    using iBizProduct.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed( ProductContext context )
        {

        }
    }
}
