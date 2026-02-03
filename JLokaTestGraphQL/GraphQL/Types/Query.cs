using JLokaTestGraphQL.Data;
using JLokaTestGraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace JLokaTestGraphQL.GraphQL.Types;

public class Query
{
    public async Task<List<Teacher>> GetTeachers([Service] AppDbContext context) => await context.Teachers.Include(x => x.Department).ToListAsync();
    public async Task<Teacher?> GetTeacher(Guid id, [Service] AppDbContext context) => await context.Teachers.FindAsync(id);
}
