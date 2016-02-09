using DelegateDecompiler;

namespace ConsoleApplication
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }
        
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<DerivedGroup> DerivedGroups { get; set; }
    }

    public abstract class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Computed]
        public abstract int SomeAbstractBusinessRule { get; }

        [Computed]
        public virtual int SomeVirtualBusinessRule
        {
            get { return 111; }
        }
    }

    public class DerivedGroup : Group
    {
        [Computed]
        public override int SomeAbstractBusinessRule
        {
            get { return 222; }
        }

        [Computed]
        public override int SomeVirtualBusinessRule
        {
            get { return 222; }
        }
    }

}