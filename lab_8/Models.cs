using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ParksTreesContext : DbContext
{
    public DbSet<Park> Parks => Set<Park>();
    public DbSet<TreeSpecies> TreeSpecies => Set<TreeSpecies>();
    public DbSet<Tree> Trees => Set<Tree>();
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Maintenance> Maintenance => Set<Maintenance>();

    public ParksTreesContext(DbContextOptions<ParksTreesContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Park>(e =>
        {
            e.ToTable("parks");
            e.HasKey(x => x.ParkId);
            e.Property(x => x.ParkId).HasColumnName("park_id");
            e.Property(x => x.Name).HasColumnName("name");
            e.Property(x => x.Location).HasColumnName("location");
        });

        b.Entity<TreeSpecies>(e =>
        {
            e.ToTable("tree_species");
            e.HasKey(x => x.SpeciesId);
            e.Property(x => x.SpeciesId).HasColumnName("species_id");
            e.Property(x => x.CommonName).HasColumnName("common_name");
            e.Property(x => x.ScientificName).HasColumnName("scientific_name");
        });

        b.Entity<Tree>(e =>
        {
            e.ToTable("trees");
            e.HasKey(x => x.TreeId);
            e.Property(x => x.TreeId).HasColumnName("tree_id");
            e.Property(x => x.ParkId).HasColumnName("park_id");
            e.Property(x => x.SpeciesId).HasColumnName("species_id");
            e.Property(x => x.PlantingDate).HasColumnName("planting_date");

            e.HasOne(t => t.Park)
             .WithMany(p => p.Trees)
             .HasForeignKey(t => t.ParkId);

            e.HasOne(t => t.Species)
             .WithMany(ts => ts.Trees)
             .HasForeignKey(t => t.SpeciesId);
        });

        b.Entity<Volunteer>(e =>
        {
            e.ToTable("volunteers");
            e.HasKey(x => x.VolunteerId);
            e.Property(x => x.VolunteerId).HasColumnName("volunteer_id");
            e.Property(x => x.FirstName).HasColumnName("first_name");
            e.Property(x => x.LastName).HasColumnName("last_name");
            e.Property(x => x.Phone).HasColumnName("phone");
        });

        b.Entity<Maintenance>(e =>
        {
            e.ToTable("maintenance");
            e.HasKey(x => x.MaintenanceId);
            e.Property(x => x.MaintenanceId).HasColumnName("maintenance_id");
            e.Property(x => x.TreeId).HasColumnName("tree_id");
            e.Property(x => x.VolunteerId).HasColumnName("volunteer_id");
            e.Property(x => x.MaintenanceDate).HasColumnName("maintenance_date");
            e.Property(x => x.Description).HasColumnName("description");

            e.HasOne(m => m.Tree)
             .WithMany(t => t.Maintenances)
             .HasForeignKey(m => m.TreeId);

            e.HasOne(m => m.Volunteer)
             .WithMany(v => v.Maintenances)
             .HasForeignKey(m => m.VolunteerId);
        });
    }
}

public class Park
{
    public int ParkId { get; set; }
    public string Name { get; set; } = "";
    public string? Location { get; set; }
    public List<Tree> Trees { get; set; } = new();
}

public class TreeSpecies
{
    public int SpeciesId { get; set; }
    public string CommonName { get; set; } = "";
    public string? ScientificName { get; set; }
    public List<Tree> Trees { get; set; } = new();
}

public class Tree
{
    public int TreeId { get; set; }
    public int ParkId { get; set; }
    public int SpeciesId { get; set; }
    public DateOnly PlantingDate { get; set; }

    public Park Park { get; set; } = null!;
    public TreeSpecies Species { get; set; } = null!;
    public List<Maintenance> Maintenances { get; set; } = new();
}

public class Volunteer
{
    public int VolunteerId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string? Phone { get; set; }
    public List<Maintenance> Maintenances { get; set; } = new();
}

public class Maintenance
{
    public int MaintenanceId { get; set; }
    public int TreeId { get; set; }
    public int VolunteerId { get; set; }
    public DateOnly MaintenanceDate { get; set; }
    public string? Description { get; set; }

    public Tree Tree { get; set; } = null!;
    public Volunteer Volunteer { get; set; } = null!;
}
public class Program
{
    public static async Task Main()
    {
        // 1. Подключение к БД
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<ParksTreesContext>()
            .UseMySql(
                config.GetConnectionString("ParksTrees")!,
                new MySqlServerVersion(new Version(8, 0, 34))
            )
            .Options;

        using var db = new ParksTreesContext(options);

        Console.WriteLine("=== ADO.NET запрос с prepared statement ===");
        await ExecuteAdoNetQuery(config);

        Console.WriteLine("\n=== ORM запросы ===");

        var q1 = await db.Trees
            .Include(t => t.Species)
            .Include(t => t.Park)
            .Where(t => t.Species.CommonName == "Дуб" && t.Park.Name == "Центральный парк")
            .Select(t => new
            {
                t.Species.CommonName,
                t.Species.ScientificName,
                t.PlantingDate
            })
            .ToListAsync();

        Console.WriteLine("\nЗапрос 1: Деревья 'Дуб' в 'Центральный парк'");
        foreach (var item in q1)
        {
            Console.WriteLine($"{item.CommonName} | {item.ScientificName} | {item.PlantingDate}");
        }

        var q2 = await db.Volunteers
            .Select(v => new
            {
                v.FirstName,
                v.LastName,
                v.Phone,
                Count = v.Maintenances.Count
            })
            .ToListAsync();

        Console.WriteLine("\nЗапрос 2: Волонтеры с количеством работ");
        foreach (var item in q2)
        {
            Console.WriteLine($"{item.FirstName} {item.LastName} | Телефон: {item.Phone} | Работ: {item.Count}");
        }

        var q3 = await db.Trees
            .Include(t => t.Park)
            .Include(t => t.Species)
            .GroupBy(t => new { t.Park.Name, t.Species.CommonName })
            .Select(g => new
            {
                ParkName = g.Key.Name,
                TreeType = g.Key.CommonName,
                Count = g.Count()
            })
            .OrderBy(x => x.ParkName)
            .ThenBy(x => x.TreeType)
            .ToListAsync();

        Console.WriteLine("\nЗапрос 3: Количество деревьев по паркам и видам");
        foreach (var item in q3)
        {
            Console.WriteLine($"{item.ParkName} | {item.TreeType}: {item.Count}");
        }

        var q4 = await db.Volunteers
            .Where(v => v.Maintenances.Count > 5)
            .Select(v => new
            {
                v.FirstName,
                v.LastName,
                Count = v.Maintenances.Count
            })
            .OrderBy(v => v.LastName)
            .ThenBy(v => v.FirstName)
            .ToListAsync();

        Console.WriteLine("\nЗапрос 4: Волонтеры с более чем 5 работами");
        foreach (var item in q4)
        {
            Console.WriteLine($"{item.FirstName} {item.LastName} | Работ: {item.Count}");
        }

        var maxTrees = await db.Trees
            .GroupBy(t => t.ParkId)
            .Select(g => g.Count())
            .MaxAsync();

        var q5 = await db.Parks
            .Where(p => p.Trees.Count == maxTrees)
            .Select(p => new
            {
                p.ParkId,
                p.Name
            })
            .ToListAsync();

        Console.WriteLine("\nЗапрос 5: Парки с максимальным числом деревьев");
        foreach (var item in q5)
        {
            Console.WriteLine($"ID: {item.ParkId} | Название: {item.Name}");
        }

        var parkStats = await db.Parks
            .Select(p => new
            {
                ParkId = p.ParkId,
                WorkCount = p.Trees
                    .SelectMany(t => t.Maintenances)
                    .Where(m => m.MaintenanceDate.Year == 2024)
                    .Count()
            })
            .ToListAsync();

        var q6 = new
        {
            Min = parkStats.Min(p => p.WorkCount),
            Avg = parkStats.Average(p => p.WorkCount),
            Max = parkStats.Max(p => p.WorkCount)
        };

        Console.WriteLine("\nЗапрос 6: Статистика работ по паркам за 2024 год");
        Console.WriteLine($"Минимум: {q6.Min} | Среднее: {q6.Avg:F2} | Максимум: {q6.Max}");

        Console.WriteLine("\n=== Модификация данных через ORM ===");

        var newPark = new Park
        {
            Name = "Новый парк",
            Location = "Москва"
        };
        db.Parks.Add(newPark);
        await db.SaveChangesAsync();
        Console.WriteLine($"Добавлен парк: {newPark.Name} (ID: {newPark.ParkId})");

        var oakSpecies = await db.TreeSpecies.FirstOrDefaultAsync(ts => ts.CommonName == "Дуб");
        if (oakSpecies != null)
        {
            var newTree = new Tree
            {
                ParkId = newPark.ParkId,
                SpeciesId = oakSpecies.SpeciesId,
                PlantingDate = DateOnly.FromDateTime(DateTime.Now)
            };
            db.Trees.Add(newTree);
            await db.SaveChangesAsync();
            Console.WriteLine($"Добавлено дерево в парк '{newPark.Name}' (ID: {newTree.TreeId})");
        }

        var volunteer = await db.Volunteers.FirstOrDefaultAsync();
        if (volunteer != null)
        {
            volunteer.Phone = "+79161234567";
            await db.SaveChangesAsync();
            Console.WriteLine($"Обновлен телефон волонтера {volunteer.FirstName}");
        }
    }

    private static async Task ExecuteAdoNetQuery(IConfiguration config)
    {
        var connectionString = config.GetConnectionString("ParksTrees")!;

        using var connection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
        await connection.OpenAsync();

        Console.Write("Введите название вида дерева: ");
        var commonName = Console.ReadLine();

        Console.Write("Введите название парка: ");
        var parkName = Console.ReadLine();

        const string sql = @"
            SELECT ts.common_name, ts.scientific_name, t.planting_date
            FROM trees t
            JOIN tree_species ts ON t.species_id = ts.species_id
            JOIN parks p ON t.park_id = p.park_id
            WHERE ts.common_name = @commonName AND p.name = @parkName";

        using var command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@commonName", commonName);
        command.Parameters.AddWithValue("@parkName", parkName);

        using var reader = await command.ExecuteReaderAsync();

        Console.WriteLine("\nРезультат ADO.NET запроса:");
        while (await reader.ReadAsync())
        {
            Console.WriteLine($"{reader["common_name"]} | {reader["scientific_name"]} | {reader["planting_date"]}");
        }
    }
}