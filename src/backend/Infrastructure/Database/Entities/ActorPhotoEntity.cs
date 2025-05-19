namespace Infrastructure.Database.Entities;

public class ActorPhotoEntity: PhotoEntity
{
    public Guid ActorId { get; set; }
    public ActorEntity Actor { get; set; }
}