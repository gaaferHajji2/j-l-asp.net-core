using JLokaTestGraphQL.Data;
using JLokaTestGraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestGraphQL.GraphQL.Types;

public class TeacherType: ObjectType<Teacher>
{
    protected override void Configure(IObjectTypeDescriptor<Teacher> descriptor)
    {
        Console.WriteLine("Configure the TeacherType");

        //descriptor.Field(x => x.Department).Name("department").Description("Simple Description").Resolve(async context =>
        //{
        //    var department = await context.Service<AppDbContext>().Departments.FindAsync(context.Parent<Teacher>().DepartmentId);
        //    Console.WriteLine("Loading Department");
        //    return department;
        //});
        descriptor.Field(x => x.Department).Description("Simple Department Resolver").ResolveWith<TeacherResolvers>(x => x.GetDepartment(default, default));
    }
}

public class TeacherResolvers
{
    public async Task<Department> GetDepartment([Parent] Teacher teacher, [Service] IDbContextFactory<AppDbContext> dbContextFactory)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        var department = await dbContext.Departments.FindAsync(teacher.DepartmentId);
        return department;
    }
}