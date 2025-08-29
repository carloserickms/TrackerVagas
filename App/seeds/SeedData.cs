using App.DataBase;
using App.Models;

public static class StatusTableSeed
{
    public static void Seed(AppDbContext context)
    {
        if (!context.VacancyStatus.Any())
        {
            var now = DateTime.Now;
            var statusList = new List<VacancyStatus>
            {
                new VacancyStatus("Aberto", Guid.NewGuid(), now, now),
                new VacancyStatus("Aguardando", Guid.NewGuid(), now, now),
                new VacancyStatus("Rejeitado", Guid.NewGuid(), now, now),
            };

            context.VacancyStatus.AddRange(statusList);
            context.SaveChanges();
        }
    }
}

public static class ModalityTableSeed
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Modality.Any())
        {
            var now = DateTime.Now;
            var modalityList = new List<Modality>
            {
                new Modality("Home Officer", Guid.NewGuid(), now, now),
                new Modality("Presencial", Guid.NewGuid(), now, now),
                new Modality("Hibrido", Guid.NewGuid(), now, now),
            };

            context.Modality.AddRange(modalityList);
            context.SaveChanges();
        }
    }
}

public static class InterestLevelSeed
{
    public static void Seed(AppDbContext context)
    {
        if (!context.InterestLevel.Any())
        {
            var now = DateTime.Now;
            var interestLevel = new List<InterestLevel>
            {
                new InterestLevel("Muito Interessado", Guid.NewGuid(), now, now),
                new InterestLevel("Neutro", Guid.NewGuid(), now, now),
                new InterestLevel("Pouco Interessado", Guid.NewGuid(), now, now),
            };

            context.InterestLevel.AddRange(interestLevel);
            context.SaveChanges();
        }
    }
}

public static class TypeOfContractSeed
{
    public static void Seed(AppDbContext context)
    {
        if (!context.TypeOfContract.Any())
        {
            var now = DateTime.Now;
            var typeOfContract = new List<TypeOfContract>
            {
                new TypeOfContract("Experiência", Guid.NewGuid(), now, now),
                new TypeOfContract("Aprendiz", Guid.NewGuid(), now, now),
                new TypeOfContract("Estágio", Guid.NewGuid(), now, now),
                new TypeOfContract("CLT", Guid.NewGuid(), now, now),
                new TypeOfContract("PJ", Guid.NewGuid(), now, now),
            };

            context.TypeOfContract.AddRange(typeOfContract);
            context.SaveChanges();
        }
    }
}