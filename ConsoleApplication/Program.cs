using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelegateDecompiler;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Model())
            {
                // Clear db
                context.Groups.RemoveRange(context.Groups.ToArray());
                context.SaveChanges();

                // Fill db
                context.DerivedGroups.Add(new DerivedGroup
                {
                    Name = "My name"
                });
                context.SaveChanges();
            }

            using (var context = new Model())
            {
                // Case 1 (virtual)
                var dto = context.Groups
                    .Select(g => new GroupDto
                    {
                        Id = g.Id,
                        Name = g.Name,

                        // Expected: 222
                        // Actual: 111
                        SomeVirtualBusinessRule = g.SomeVirtualBusinessRule,
                    })
                    .Decompile()
                    .FirstOrDefault();

                // Case 2 (abstract)
                dto = context.Groups
                    .Select(g => new GroupDto
                    {
                        Id = g.Id,
                        Name = g.Name,

                        // Expected: 222
                        // Actual: NullReferenceException
                        SomeAbstractBusinessRule = g.SomeAbstractBusinessRule
                    })
                    .Decompile()
                    .FirstOrDefault();
            }
        }
    }
}
