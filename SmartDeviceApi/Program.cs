using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<DeviceDb>(opt => opt.UseInMemoryDatabase("DeviceList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<DeviceDb>(options => options.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=ApiProjetFinal;Integrated Security=True"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//***********controlleur de devices***********************
app.MapGet("api/smartdevices", async (DeviceDb db) =>
    await db.Devices.ToListAsync());


app.MapGet("api/smartdevices/{id}", async (int id, DeviceDb db) =>
    await db.Devices.FindAsync(id)
        is Device device
            ? Results.Ok(device)
            : Results.NotFound());

app.MapPost("api/smartdevices", async (Device device, DeviceDb db) =>
{
    db.Devices.Add(device);
    await db.SaveChangesAsync();

    return Results.Created($"api/smartdevices/{device.Id}", device);
});

app.MapPut("api/smartdevices/{id}", async (int id, Device inputDevice, DeviceDb db) =>
{
    var device = await db.Devices.FindAsync(id);

    if (device is null) return Results.NotFound();

    device.Modele = inputDevice.Modele;
    device.Fabriquant = inputDevice.Fabriquant;
    device.Type = inputDevice.Type;
    device.Plateforme = inputDevice.Plateforme;
    device.Prix = inputDevice.Prix;
    device.ImageUrl = inputDevice.ImageUrl;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/smartdevices/{id}", async (int id, DeviceDb db) =>
{
    if (await db.Devices.FindAsync(id) is Device device)
    {
        db.Devices.Remove(device);
        await db.SaveChangesAsync();
        return Results.Ok(device);
    }

    return Results.NotFound();
});


//***********controlleur de factures***********************
app.MapGet("api/factures", async (DeviceDb db) =>
    await db.Factures.ToListAsync());

 

app.MapGet("api/factures/{id}", async (int id, DeviceDb db) =>
    await db.Factures.FindAsync(id)
        is Facture facture
            ? Results.Ok(facture)
            : Results.NotFound());

app.MapPost("api/factures", async (Facture facture, DeviceDb db) =>
{
    db.Factures.Add(facture);
    await db.SaveChangesAsync();

    return Results.Created($"api/factures/{facture.Id}", facture);
});

app.MapPut("api/factures/{id}", async (int id, Facture inputFacture, DeviceDb db) =>
{
    var device = await db.Factures.FindAsync(id);

    if (device is null) return Results.NotFound();

    device.Montant = inputFacture.Montant;
    device.Nom = inputFacture.Nom;
    device.Prenom = inputFacture.Prenom;
    device.Adresse = inputFacture.Adresse;
    device.Telephone = inputFacture.Telephone;
    device.Courriel = inputFacture.Courriel;
    device.NumBancaire = inputFacture.NumBancaire;
    device.Photo = inputFacture.Photo;


    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/factures/{id}", async (int id, DeviceDb db) =>
{
    if (await db.Factures.FindAsync(id) is Facture facture)
    {
        db.Factures.Remove(facture);
        await db.SaveChangesAsync();
        return Results.Ok(facture);
    }

    return Results.NotFound();
});

app.Run();

//Models
class Device
{


    public int Id { get; set; }
    public string? Modele { get; set; }
    public string? Fabriquant { get; set; }

    public string? Type { get; set; }
    public string? Plateforme { get; set; }
    public double? Prix { get; set; }
    public string? ImageUrl { get; set; }
}

class Facture
{
    public int Id { get; set; }
    public double Montant { get; set; }

    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    public string? Adresse { get; set; }
    public string? Telephone { get; set; }
    public string? Courriel { get; set; }
    public string? NumBancaire { get; set; }
    public string? Photo { get; set; }
    public string? NomClient { get; set; }
    public string? ProduitsStr { get; set; }

}


//DbContext
class DeviceDb : DbContext
{
    public DeviceDb(DbContextOptions<DeviceDb> options)
        : base(options) { }

    public DbSet<Device> Devices => Set<Device>();
    public DbSet<Facture> Factures => Set<Facture>();

}