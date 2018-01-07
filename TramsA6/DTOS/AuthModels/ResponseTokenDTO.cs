using Newtonsoft.Json;

namespace TramsA6.DTOS.AuthModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ResponseTokenDTO
    {
        public ResponseTokenDTO()
        {
        }

        public string Token { get; set; }

        public int Expiration { get; set; }
    }
}