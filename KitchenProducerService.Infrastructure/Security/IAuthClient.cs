namespace KitchenProducerService.Infrastructure.Security
{
    public interface IAuthClient
    {
        Task<bool> ValidateTokenAsync(string token);
    }
}