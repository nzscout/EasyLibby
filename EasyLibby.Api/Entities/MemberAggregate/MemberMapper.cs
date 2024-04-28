using Riok.Mapperly.Abstractions;

namespace EasyLibby.Api.Entities.MemberAggregate
{

    [Mapper]
    public static partial class MemberMapper
    {
        public static partial CreateMemberDto AsCreateDto(this Member Member);

        public static partial Member AsEntity(this CreateMemberDto createDto);

        public static partial MemberDto AsDto(this Member Member);

        public static partial Member AsEntity(this UpdateMemberDto updateDto);

        public static partial void UpdateMemberDtoToMember(UpdateMemberDto MemberDto, Member Member);
    }



}
