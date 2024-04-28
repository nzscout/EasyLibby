using Riok.Mapperly.Abstractions;

namespace EasyLibby.Api.Entities.BookAggregate
{
    [Mapper]
    public static partial class BookMapper
    {
        public static partial CreateBookDto AsCreateDto(this Book book);

        public static partial UpdateBookDto AsUpdateDto(this Book book);

        public static partial Book AsEntity(this CreateBookDto createDto);

        public static partial BookDto AsDto(this Book book);

        public static partial Book AsEntity(this UpdateBookDto updateDto);

        public static partial void UpdateBookDtoToBook(UpdateBookDto bookDto, Book book);
    }



}
