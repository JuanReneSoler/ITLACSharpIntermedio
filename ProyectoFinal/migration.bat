

dotnet ef dbcontext scaffold "Server=localhost\SQLEXPRESS;Database=SistemaVotaciones;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c SVContext --project .\Persistence\Persistence.csproj
