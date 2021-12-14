namespace product_service.Infrastructure.Api.Dto
{
    public record AttributeDto<T>(string Key, T Value);
}
