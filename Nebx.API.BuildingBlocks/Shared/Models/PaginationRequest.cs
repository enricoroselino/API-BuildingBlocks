namespace Nebx.API.BuildingBlocks.Shared.Models;

public record PaginationRequest(int PageIndex, int PageSize = 10);