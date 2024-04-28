using Riok.Mapperly.Abstractions;

namespace EasyLibby.Api.Entities.AuthorAggregate
{

    [Mapper]
    public static partial class AuthorMapper
    {
        public static partial CreateAuthorDto AsCreateDto(this Author Author);

        public static partial Author AsEntity(this CreateAuthorDto createDto);

        public static partial AuthorDto AsDto(this Author Author);

        public static partial Author AsEntity(this UpdateAuthorDto updateDto);

        public static partial void UpdateAuthorDtoToAuthor(UpdateAuthorDto AuthorDto, Author Author);
    }



}
